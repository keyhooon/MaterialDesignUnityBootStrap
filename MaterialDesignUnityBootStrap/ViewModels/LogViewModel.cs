using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignUnityBootStrap.Config;
using MaterialDesignUnityBootStrap.Services.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prism.Events;
using Prism.Mvvm;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class LogViewModel : BindableBase
    {
        public ObservableCollection<LogEventMessage> LogEventMessagesList { get; }
        public LogViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<LogPubSubEvent>().Subscribe(LogEventRaised);
            LogEventMessagesList = new ObservableCollection<LogEventMessage>();
        }

        private void LogEventRaised(LogEventMessage message)
        {
            LogEventMessagesList.Add(message);
        }
    }
}
