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

        public static IEnumerable<LoggingEvent> Contents(this ILog log)
        {
            return GetAppender().GetEvents().Where(e => e.LoggerName == log.Logger.Name);
        }

        public static void Clear(this ILog log)
        {
            GetAppender().Clear(log);
        }
    }
}