
using NLog;
using System.Text;

namespace XFrameServer.Core.Logs
{
    public class NLogLogger : XFrame.Modules.Diagnotics.ILogger
    {
        private Logger m_Logger;

        public NLogLogger()
        {
            LogManager.Setup().LoadConfiguration((builder) =>
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                builder.ForLogger().FilterLevel(LogLevel.Debug).WriteToFile($"logs/nlog-debug-{time}.txt");
                builder.ForLogger().FilterLevel(LogLevel.Warn).WriteToFile($"logs/nlog-warning-{time}.txt");
                builder.ForLogger().FilterLevel(LogLevel.Error).WriteToFile($"logs/nlog-error-{time}.txt");
                builder.ForLogger().FilterLevel(LogLevel.Fatal).WriteToFile($"logs/nlog-fatal-{time}.txt");
            });
            m_Logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(params object[] content)
        {
            m_Logger.Debug(InnerFormat(content));
        }

        public void Error(params object[] content)
        {
            m_Logger.Error(InnerFormat(content));
        }

        public void Exception(Exception e)
        {
            m_Logger.Fatal(e.ToString());
        }

        public void Fatal(params object[] content)
        {
            m_Logger.Fatal(InnerFormat(content));
        }

        public void Warning(params object[] content)
        {
            m_Logger.Warn(InnerFormat(content));
        }

        private string InnerFormat(object[] content)
        {
            if (content.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append('[');
                sb.Append(content[0]);
                sb.Append(']');
                for (int i = 1; i < content.Length; i++)
                    sb.Append(content[i]);
                return sb.ToString();
            }
            else
            {
                return string.Concat(content);
            }
        }
    }
}
