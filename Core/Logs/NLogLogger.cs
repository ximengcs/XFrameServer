
using System.Text;
using XFrame.Modules.Diagnotics;

namespace XFrameServer.Core.Logs
{
    public class NLogLogger : ILogger
    {
        private NLog.Logger m_Logger;

        public NLogLogger()
        {
            m_Logger = NLog.LogManager.GetCurrentClassLogger();
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
                for (int i = 0; i < content.Length; i++)
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
