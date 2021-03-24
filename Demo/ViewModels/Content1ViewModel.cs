using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignUnityBootStrap.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.ViewModels
{
    public class Content1ViewModel : BindableBase
    {
        public Content1ViewModel(ILoggerFactory loggerFactory, IOptions<MainWindowOptions> mainWindOptions)
        {
            var logger = loggerFactory.CreateLogger(mainWindOptions.Value.MainLoggerCategoryName);
            logger.Log(LogLevel.Error,"aaa");
        }
    }
}
