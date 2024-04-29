
using XFrame.Modules.Diagnotics;

namespace XFrameServer.Core.Logs
{
    public class Logger : ILogger
    {
        public void Debug(params object[] content)
        {
            InnerFormat(out string result, content);
            Console.WriteLine(result);
        }

        public void Error(params object[] content)
        {
            InnerFormat(out string result, content);
            Console.WriteLine(result);
        }

        public void Exception(Exception e)
        {
            Console.WriteLine(e);
        }

        public void Fatal(params object[] content)
        {
            InnerFormat(out string result, content);
            Console.WriteLine(result);
        }

        public void Warning(params object[] content)
        {
            InnerFormat(out string result, content);
            Console.WriteLine(result);
        }

        private bool InnerFormat(out string result, params object[] content)
        {
            if (content.Length > 1)
            {
                if (content.Length > 2)
                {
                    object[] contentList = new object[content.Length - 2];
                    for (int i = 0; i < contentList.Length; i++)
                        contentList[i] = content[i + 2];
                    result = string.Format((string)content[1], contentList);
                }
                else
                {
                    result = $"[{content[0]}] {content[1]}";
                }
                return true;
            }
            else
            {
                result = string.Concat(content);
                return true;
            }
        }

    }
}
