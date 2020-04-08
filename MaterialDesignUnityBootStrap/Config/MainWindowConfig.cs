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
        }
        public static string SectionName = "MainWindow";
        public string Name { get; set; }

    }
}
