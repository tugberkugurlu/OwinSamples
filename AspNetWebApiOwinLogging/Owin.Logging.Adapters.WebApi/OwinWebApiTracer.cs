using Microsoft.Owin.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;
using TraceLevel = System.Web.Http.Tracing.TraceLevel;

namespace Owin.Logging.Adapters.WebApi
{
    public class OwinWebApiTracer : ITraceWriter
    {
        private static readonly TraceEventType[] TraceLevelToTraceEventType = new TraceEventType[]
        {
            // TraceLevel.Off
            (TraceEventType)0,

            // TraceLevel.Debug
            TraceEventType.Verbose,

            // TraceLevel.Info
            TraceEventType.Information,

            // TraceLevel.Warn
            TraceEventType.Warning,

            // TraceLevel.Error
            TraceEventType.Error,

            // TraceLevel.Fatal
            TraceEventType.Critical
        };

        private readonly ILogger _logger;

        public OwinWebApiTracer(ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _logger = logger;
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            if (traceAction == null)
            {
                throw new ArgumentNullException("traceAction");
            }

            if (level < TraceLevel.Off || level > TraceLevel.Fatal)
            {
                throw new ArgumentOutOfRangeException("level");
            }

            TraceRecord traceRecord = new TraceRecord(request, category, level);
            traceAction(traceRecord);
            string message = Format(traceRecord);
            if (!String.IsNullOrEmpty(message))
            {
                _logger.WriteCore(eventType: TraceLevelToTraceEventType[(int)traceRecord.Level], eventId: 0, state: null, exception: traceRecord.Exception, formatter: (_state, _ex) => 
                {
                    return Format(traceRecord);
                });
            }
        }

        // privates

        private static string Format(TraceRecord record)
        {
            StringBuilder message = new StringBuilder();

            if (record.Request != null)
            {
                message.Append(record.RequestId);

                if (record.Request.Method != null)
                    message.Append(" ").Append(record.Request.Method);

                if (record.Request.RequestUri != null)
                    message.Append(" ").Append(record.Request.RequestUri.AbsoluteUri);
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
            {
                message.Append(" ").Append(record.Category);
            }

            if (!string.IsNullOrWhiteSpace(record.Operator))
            {
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);
            }

            if (!string.IsNullOrWhiteSpace(record.Message))
            {
                message.Append(" ").Append(record.Message);
            }

            if (record.Exception != null && !string.IsNullOrWhiteSpace(record.Exception.GetBaseException().Message))
            {
                message.Append(" ").Append(record.Exception.GetBaseException().Message);
            }

            return message.ToString();
        }
    }
}