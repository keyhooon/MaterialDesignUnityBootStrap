using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prism.Events;

namespace MaterialDesignUnityBootStrap.Services.Logging
{
    public class LogPubSubEvent : PubSubEvent<LogEventMessage> 
    {

    }
}
