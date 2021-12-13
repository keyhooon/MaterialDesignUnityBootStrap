using System;
using Microsoft.Extensions.Logging;

namespace MaterialDesignUnityBootStrap.Services.Logging
{
    public class LogEventMessage
    {
        public LogEventMessage(LogLevel logLevel, EventId eventId, string message)
        {
            LogLevel = logLevel;
            EventId = eventId;
            Message = message;
        }

        public LogLevel LogLevel { get; }
        public EventId EventId { get; }
        public String Message { get; }
    }
}
