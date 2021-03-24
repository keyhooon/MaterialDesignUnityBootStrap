using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MaterialDesignUnityBootStrap.Services.Logging;
using Prism.Events;

namespace Demo.ViewModels
{
    public class ToolViewModel : BindableBase
    {
        public ObservableCollection<LogEventMessage> LogEventMessagesList { get; }
        public ToolViewModel(IEventAggregator eventAggregator)
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
