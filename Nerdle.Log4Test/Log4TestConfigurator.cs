using log4net.Config;

namespace Nerdle.Log4Test
{
    public class Log4TestConfigurator
    {
        public static void Configure()
        {
            BasicConfigurator.Configure(new Log4TestAppender { Name = typeof(Log4TestAppender).FullName });
        }
    }
}