using System.Collections.ObjectModel;
using System.Windows;
using MaterialDesignUnityBootStrap.Services.Logging;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Mvvm;

namespace MaterialDesignUnityBootStrap.ViewModels
{
    public class LogViewModel : BindableBase
    {
        public ObservableCollection<LogEventMessage> LogEventMessagesList { get; }
        public LogViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<LogPubSubEvent>().Subscribe( o => Application.Current.Dispatcher.Invoke(()=>LogEventRaised(o)));
            LogEventMessagesList = new ObservableCollection<LogEventMessage>();
        }

        private void LogEventRaised(LogEventMessage message)
        {
            if (message.LogLevel > LogLevel.Error)
                LogEventMessagesList.Add(message);
        }
    }
}
