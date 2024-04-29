
using XFrame.Modules.Diagnotics;

namespace XFrameServer.Core.Logs
{
    public partial class ConsoleLogger : ILogger
    {
        private Formater m_Formater;

        public ConsoleLogger()
        {
            m_Formater = new Formater();
        }

        public void Debug(params object[] content)
        {
            lock (m_Formater)
            {
                InnerFormat(out string head, out string result, content);

                if (!string.IsNullOrEmpty(head))
                {
                    m_Formater.Head();
                    Console.Write(head);
                    m_Formater.End();
                }

                m_Formater.Begin(result);
                Console.WriteLine(result);
                m_Formater.End();
            }
        }

        public void Error(params object[] content)
        {
            lock (m_Formater)
            {
                m_Formater.Error();
                InnerFormat(out string head, out string result, content);
                Console.WriteLine(result);
                m_Formater.End();
            }
        }

        public void Exception(Exception e)
        {
            lock (m_Formater)
            {
                m_Formater.Fatal();
                Console.WriteLine(e);
                m_Formater.End();
            }
        }

        public void Fatal(params object[] content)
        {
            lock (m_Formater)
            {
                m_Formater.Fatal();
                InnerFormat(out string head, out string result, content);
                Console.WriteLine(result);
                m_Formater.End();
            }
        }

        public void Warning(params object[] content)
        {
            lock (m_Formater)
            {
                m_Formater.Warning();
                InnerFormat(out string head, out string result, content);
                Console.WriteLine(result);
                m_Formater.End();
            }
        }

        private bool InnerFormat(out string head, out string result, params object[] content)
        {
            if (content.Length > 1)
            {
                head = $"[{content[0]}]".PadRight(10, ' ');
                if (content.Length > 2)
                {
                    object[] contentList = new object[content.Length - 2];
                    for (int i = 0; i < contentList.Length; i++)
                        contentList[i] = content[i + 2];
                    result = string.Format((string)content[1], contentList);
                }
                else
                {
                    result = $"{content[1]}";
                }
                return true;
            }
            else
            {
                result = string.Concat(content);
                head = null;
                return true;
            }
        }

    }
}
