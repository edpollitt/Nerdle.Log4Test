using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace Nerdle.Log4Test
{
    public static class Extensions
    {
        static Log4TestAppender GetAppender()
        {
            var repository = (Hierarchy)LogManager.GetRepository();

            var appender = repository.Root.GetAppender(typeof(Log4TestAppender).FullName)
                as Log4TestAppender;

            if (appender == null)
                throw new InvalidOperationException("The Log4TestAppender is not configured. A call to Log4TestConfigurator.Configure() is required before accessing the logging system.");

            return appender;
        }

        public static IEnumerable<LoggingEvent> Events(this ILog log)
        {
            return GetAppender().GetEvents().Where(e => e.LoggerName == log.Logger.Name);
        }

        public static IEnumerable<LoggingEvent> At(this IEnumerable<LoggingEvent> events, Level logLevel)
        {
            return events.Where(e => e.Level == logLevel);
        }
        public static IEnumerable<LoggingEvent> AtOrAbove(this IEnumerable<LoggingEvent> events, Level logLevel)
        {
            return events.Where(e => e.Level >= logLevel);
        }

        public static IEnumerable<LoggingEvent> Above(this IEnumerable<LoggingEvent> events, Level logLevel)
        {
            return events.Where(e => e.Level > logLevel);
        }

        public static IEnumerable<LoggingEvent> AtOrBelow(this IEnumerable<LoggingEvent> events, Level logLevel)
        {
            return events.Where(e => e.Level <= logLevel);
        }

        public static IEnumerable<LoggingEvent> Below(this IEnumerable<LoggingEvent> events, Level logLevel)
        {
            return events.Where(e => e.Level < logLevel);
        }

        public static void Clear(this ILog log)
        {
            GetAppender().Clear(log);
        }
    }
}