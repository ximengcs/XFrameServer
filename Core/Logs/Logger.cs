
using XFrame.Modules.Diagnotics;

namespace XFrameServer.Core.Logs
{
    public class Logger : ILogger
    {
        public void Debug(params object[] content)
        {
            Console.WriteLine(content.Length);
        }

        public void Error(params object[] content)
        {
            Console.WriteLine(content.Length);
        }

        public void Exception(Exception e)
        {
            Console.WriteLine(e);
        }

        public void Fatal(params object[] content)
        {
            Console.WriteLine(content.Length);
        }

        public void Warning(params object[] content)
        {
            Console.WriteLine(content.Length);
        }
    }
}
