using System;
using System.Collections.Generic;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class PaletteSelectorViewModel : BindableBase, IDialogAware
    {
        private readonly PaletteHelper _paletteHelper;
        private readonly Theme _theme;

        private DelegateCommand<bool> _toggleBaseCommand;
        private DelegateCommand<Swatch> _applyPrimaryCommand;
        private DelegateCommand<Swatch> _applyAccentCommand;
        private DelegateCommand _okCommand;

        public PaletteSelectorViewModel()
        {
            _paletteHelper = new PaletteHelper();
            _theme = (Theme)_paletteHelper.GetTheme();
            Swatches = new SwatchesProvider().Swatches;
        }

        public bool IsDark
        {
            get => _theme.Background == Theme.Dark.MaterialDesignBackground;
            set { _theme.SetBaseTheme(value ? Theme.Dark : Theme.Light); ThemeSettings.Default.IsDark = value; ThemeSettings.Default.Save(); _paletteHelper.SetTheme(_theme); RaisePropertyChanged(); }
        }

        public IEnumerable<Swatch> Swatches { get; }

        public DelegateCommand<bool> ToggleBaseCommand => _toggleBaseCommand ??= new DelegateCommand<bool>(
            (o) => { _theme.SetBaseTheme(o ? Theme.Dark : Theme.Light); ThemeSettings.Default.IsDark = o; ThemeSettings.Default.Save(); _paletteHelper.SetTheme(_theme); });

        public DelegateCommand<Swatch> ApplyPrimaryCommand => _applyPrimaryCommand ??= new DelegateCommand<Swatch>(
            swatch => { _theme.SetPrimaryColor(swatch.ExemplarHue.Color); ThemeSettings.Default.PrimaryColor = swatch.ExemplarHue.Color; ThemeSettings.Default.Save(); _paletteHelper.SetTheme(_theme); });


        public DelegateCommand<Swatch> ApplyAccentCommand => _applyAccentCommand ??= new DelegateCommand<Swatch>(
            swatch => { _theme.SetSecondaryColor(swatch.AccentExemplarHue.Color); ThemeSettings.Default.SecondaryColor = swatch.AccentExemplarHue.Color; ThemeSettings.Default.Save(); _paletteHelper.SetTheme(_theme); });

        public DelegateCommand OkCommand => _okCommand ??= new DelegateCommand(() => { 
            RequestClose(new DialogResult(ButtonResult.OK)); 
        });



        public string Title => "Themes";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
