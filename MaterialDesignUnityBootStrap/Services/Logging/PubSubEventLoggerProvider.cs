using System.Collections.Concurrent;
using MaterialDesignUnityBootStrap.Config;
using Microsoft.Extensions.Logging;
using Prism.Events;

namespace MaterialDesignUnityBootStrap.Services.Logging
{
    public sealed class PubSubEventLoggerProvider : ILoggerProvider
    {
        private readonly PubSubEventLoggerOptions _option;
        private readonly IEventAggregator _eventAggregator;

        private readonly ConcurrentDictionary<string, PubSubEventLogger> _loggers = new();

        public PubSubEventLoggerProvider(PubSubEventLoggerOptions option, IEventAggregator eventAggregator)
        {
            _option = option;
            _eventAggregator = eventAggregator;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new PubSubEventLogger(name, _option, _eventAggregator));

        public void Dispose() => _loggers.Clear();
    }
}