using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class PaletteSelectorViewModel : BindableBase, IDialogAware
    {
        readonly PaletteHelper paletteHelper;
        readonly Theme theme;

        private DelegateCommand<bool> _toggleBaseCommand;
        private DelegateCommand<Swatch> _applyPrimaryCommand;
        private DelegateCommand<Swatch> _applyAccentCommand;
        private DelegateCommand _okCommand;

        public PaletteSelectorViewModel()
        {
            paletteHelper = new PaletteHelper();
            theme = (Theme)paletteHelper.GetTheme();
            Swatches = new SwatchesProvider().Swatches;
        }

        public bool IsDark
        {
            get => theme.Background == Theme.Dark.MaterialDesignBackground;
            set { theme.SetBaseTheme(value ? Theme.Dark : Theme.Light); ThemeSettings.Default.IsDark = value; ThemeSettings.Default.Save(); paletteHelper.SetTheme(theme); RaisePropertyChanged(); }
        }

        public IEnumerable<Swatch> Swatches { get; }

        public DelegateCommand<bool> ToggleBaseCommand => _toggleBaseCommand ??= new DelegateCommand<bool>(
            (o) => { theme.SetBaseTheme(o ? Theme.Dark : Theme.Light); ThemeSettings.Default.IsDark = o; ThemeSettings.Default.Save(); paletteHelper.SetTheme(theme); });

        public DelegateCommand<Swatch> ApplyPrimaryCommand => _applyPrimaryCommand ??= new DelegateCommand<Swatch>(
            swatch => { theme.SetPrimaryColor(swatch.ExemplarHue.Color); ThemeSettings.Default.PrimaryColor = swatch.ExemplarHue.Color; ThemeSettings.Default.Save(); paletteHelper.SetTheme(theme); });


        public DelegateCommand<Swatch> ApplyAccentCommand => _applyAccentCommand ??= new DelegateCommand<Swatch>(
            swatch => { theme.SetSecondaryColor(swatch.AccentExemplarHue.Color); ThemeSettings.Default.SecondaryColor = swatch.AccentExemplarHue.Color; ThemeSettings.Default.Save(); paletteHelper.SetTheme(theme); });

        public DelegateCommand OkCommand => _okCommand ??= new DelegateCommand(() => { 
            RequestClose(new DialogResult(ButtonResult.OK)); 
        });



        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
        }
    }
}
