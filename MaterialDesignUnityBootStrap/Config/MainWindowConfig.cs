using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialDesignUnityBootStrap.Config
{
    public class MainWindowConfig
    {
        public MainWindowConfig()
        {
            Name = "Application";
            PaletteSelectorVisibility = true;
            NavigationButtonVisibility = true;
        }
        public static string SectionName = "MainWindow";
        public string Name { get; set; }


        public bool PaletteSelectorVisibility { get; set; }
        public bool NavigationButtonVisibility { get; set; }

        public string PopupToolBarRegionName { get; set; }

    }
}
