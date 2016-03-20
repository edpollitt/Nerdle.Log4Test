using log4net;
using log4net.Appender;
using log4net.Core;

namespace Nerdle.Log4Test
{
    class Log4TestAppender : MemoryAppender
    {
        public void Clear(ILog log)
        {
            lock (m_eventsList.SyncRoot)
            {
                for (var i = m_eventsList.Count - 1; i >= 0; i--)
                {
                    if (((LoggingEvent)m_eventsList[i]).LoggerName == log.Logger.Name)
                    {
                        m_eventsList.RemoveAt(i);
                    }
                }
            }
        }
    }
}