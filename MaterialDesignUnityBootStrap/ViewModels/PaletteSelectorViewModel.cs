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

namespace Health_Monitoring.ViewModels
{
    public class PaletteSelectorViewModel : BindableBase
    {
        private static readonly PaletteHelper PaletteHelper = new PaletteHelper();
        private DelegateCommand<bool> _toggleBaseCommand;
        private DelegateCommand<Swatch> _applyPrimaryCommand;
        private DelegateCommand<Swatch> _applyAccentCommand;

        public PaletteSelectorViewModel()
        {

            Swatches = new SwatchesProvider().Swatches;

        }
        private bool isDark = true;
        public bool IsDark
        {
            get { return isDark; }
            set => SetProperty(ref isDark, value, () => ModifyTheme(theme => theme.SetBaseTheme(isDark ? Theme.Dark : Theme.Light)));
        }

        public IEnumerable<Swatch> Swatches { get; }

        public DelegateCommand<bool> ToggleBaseCommand => _toggleBaseCommand ??= new DelegateCommand<bool>(
            (o) => {
                ModifyTheme(theme => theme.SetBaseTheme(o ? Theme.Dark : Theme.Light));
            });

        public DelegateCommand<Swatch> ApplyPrimaryCommand => _applyPrimaryCommand ??= new DelegateCommand<Swatch>(
            swatch => {
                ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));
            });

        public DelegateCommand<Swatch> ApplyAccentCommand => _applyAccentCommand ??= new DelegateCommand<Swatch>(swatch =>
        {
            ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
        });

       
        protected static void ModifyTheme(Action<ITheme> modificationAction)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}
