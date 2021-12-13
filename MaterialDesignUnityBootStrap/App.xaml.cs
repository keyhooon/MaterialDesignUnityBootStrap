    using System;
    using Prism.Ioc;
using MaterialDesignUnityBootStrap.Views;
using System.Windows;
using System.Windows.Controls;

using Prism.Regions;
using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
using Prism.Modularity;
using CompositeContentNavigator.Infrastructure;
using MaterialDesignThemes.Wpf;
using MaterialDesignUnityBootStrap.ViewModels;
    using CompositeContentNavigator;
    using MaterialDesignUnityBootStrap.Config;
    using MaterialDesignUnityBootStrap.Services.Authentication;
    using MaterialDesignUnityBootStrap.Services.Logging;
    using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
    using Prism.Events;
    using Prism.Services.Dialogs;
    using Prism.Unity;
    using Unity;
    using DispatcherUnhandledExceptionEventArgs = System.Windows.Threading.DispatcherUnhandledExceptionEventArgs;

    namespace MaterialDesignUnityBootStrap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // {435DB169-7D20-4896-9B61-AB32538469E3}
        private static readonly Guid AppGuid = new( 0x435db169, 0x7d20, 0x4896, new byte[] { 0x9b, 0x61, 0xab, 0x32, 0x53, 0x84, 0x69, 0xe3 } );
        AutoResetEvent eventWaitHandle;
        IDialogService dialogService;
        IProgress<string> splashMessage;
        private IEventAggregator _logEventAggregator;
        private Mutex _mutex;

        private IEventAggregator LogEventAggregator
        {
            get
            {
                if (_logEventAggregator == null && Container.IsRegistered<IEventAggregator>())
                    _logEventAggregator = Container.Resolve<IEventAggregator>();
                return _logEventAggregator;
            }
        }

        private void App_DispatcherUnhandledException(
            object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            LogEventAggregator?.GetEvent<LogPubSubEvent>()?.Publish(new LogEventMessage(LogLevel.Critical, 0, e.Exception.ToString()));
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            try
            {
                LogEventAggregator?.GetEvent<LogPubSubEvent>()?.Publish(new LogEventMessage(LogLevel.Critical, 0, e.ToString()));
            }
            catch
            {
                // ignored
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            PrismContainerExtension.Init();

            eventWaitHandle = new AutoResetEvent(false);
            splashMessage = new Progress<string>();
            base.OnStartup(e);
            Task.Run(() =>
            {
                DispatcherUnhandledException += App_DispatcherUnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                // check mutex for one instance of application exist in same time
                _mutex = new Mutex(false, AppGuid.ToString());
                if (!_mutex.WaitOne(0, false))
                    Current.Shutdown();
            });
        }


        protected override async void InitializeModules()
        {
            await Task.Run(() =>
            {
                eventWaitHandle.WaitOne();
                Container.Resolve<IModuleManager>().Run();
            });
            splashMessage.Report(string.Empty);
            await Task.Delay(200);
            if (Container.IsRegistered<UserManager>())
                dialogService.Show(typeof(LoginView).FullName, new DialogParameters(), result =>
                {
                    if (result.Result == ButtonResult.Cancel)
                        Application.Current.Shutdown();
                });
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            dialogService = Container.Resolve<IDialogService>();
            dialogService.Show(typeof(SplashScreenView).FullName, new DialogParameters { { "report", splashMessage } },
                (o) =>
                {
                    

                });
            Container.Resolve<IModuleManager>().LoadModuleCompleted += (_, args) =>
            {
                splashMessage.Report($"{args.ModuleInfo.ModuleName} is Loaded.\r\n");
            };

            eventWaitHandle.Set();
        }

        protected override IContainerExtension CreateContainerExtension()
        {
            lock (this)
            {
                return PrismContainerExtension.Current;
            }

        }
        /// <summary>
        /// Registers the <see cref="Type"/>s of the Exceptions that are not considered 
        /// root exceptions by the <see cref="ExceptionExtensions"/>.
        /// </summary>
        protected override void RegisterFrameworkExceptionTypes()
        {
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ResolutionFailedException));
        }
        protected override Window CreateShell()
        {
            LoadTheme();
            return Dispatcher.InvokeAsync(() =>
            {
                var mainWindow = Container.Resolve<MainWindow>();

                Container.Resolve<IRegionManager>().RegisterViewWithRegion("PopupToolBarRegion", typeof(LogView));
                return mainWindow;
            }).Result;
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings?.RegisterMapping(typeof(ToolBarTray), Container.Resolve<ToolBarTrayRegionAdapter>());
        }
        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {


            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppConfig.json", optional: true, true).Build();
            var pubSubEventLoggerProvider = Container.Resolve<PubSubEventLoggerProvider>();
            this.DispatcherUnhandledException += (sender, args) =>
            {
                pubSubEventLoggerProvider.CreateLogger("Main").LogError(args.Exception.Message);
                args.Handled = true;
            };
            
            containerRegistry
                .Register<MainWindow>()
                .RegisterInstance(configurationRoot)
                .RegisterServices(s =>
                {
                    s.AddOptions<ContentNavigatorOptions>()
                        .Bind(configurationRoot.GetSection(nameof(ContentNavigatorOptions))).ValidateDataAnnotations();
                    ; s.AddOptions<MainWindowOptions>()
                        .Bind(configurationRoot.GetSection(nameof(MainWindowOptions))).ValidateDataAnnotations();
                    ;
                  
                    s.AddLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddSimpleConsole(options =>
                        {
                            options.IncludeScopes = true;
                            options.SingleLine = true;
                            options.TimestampFormat = "hh:mm:ss ";
                        });
                        logging.AddProvider(pubSubEventLoggerProvider);
                        logging.AddConfiguration(configurationRoot);
                    });
                });
            containerRegistry.RegisterDialog<PaletteSelector, PaletteSelectorViewModel>(typeof(PaletteSelector).FullName);
            containerRegistry.RegisterDialog<SplashScreenView, SplashScreenViewModel>(typeof(SplashScreenView).FullName);
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>(typeof(LoginView).FullName);
            containerRegistry.RegisterDialog<AboutView, AboutViewModel>(typeof(AboutView).FullName);
            containerRegistry.RegisterDialogWindow<DialogWindow>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ContentNavigatorModule>();

        }
        private void LoadTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = (Theme)paletteHelper.GetTheme();
            var themeSettings = ThemeSettings.Default;
            theme.SetBaseTheme(themeSettings.IsDark ? Theme.Dark : Theme.Light);
            if (themeSettings.PrimaryColor.HasValue)
                theme.SetPrimaryColor(themeSettings.PrimaryColor.Value);
            if (themeSettings.SecondaryColor.HasValue)
                theme.SetSecondaryColor(themeSettings.SecondaryColor.Value);
            paletteHelper.SetTheme(theme);

        }
        protected override void OnExit(ExitEventArgs e)
        {
            ThemeSettings.Default.Save();
            base.OnExit(e);
        }
        
    }
}
