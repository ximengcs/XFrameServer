
using XFrame.Modules.Diagnotics;
using XFrameShare.Network;

namespace XFrameServer.Core.Logs
{
    public partial class ConsoleLogger
    {
        private class Formater
        {
            private ConsoleColor OriginColor;
            private Dictionary<string, ConsoleColor> m_Colors = new Dictionary<string, ConsoleColor>()
                {
                    { Log.XFrame, ConsoleColor.Magenta },
                    { Log.Procedure, ConsoleColor.DarkYellow },
                    { Log.Condition, ConsoleColor.Blue },
                    { Log.CSV, ConsoleColor.DarkCyan },
                    { NetMode.Client.ToString(), ConsoleColor.Blue },
                    { NetMode.Server.ToString(), ConsoleColor.Yellow },
                    { NetConst.Net, ConsoleColor.Blue }
                };

            public void Register(string name, ConsoleColor color)
            {
                m_Colors.Add(name, color);
            }

            public void Begin(string name)
            {
                OriginColor = Console.ForegroundColor;
                if (!string.IsNullOrEmpty(name) && m_Colors.TryGetValue(name, out ConsoleColor color))
                {
                    Console.ForegroundColor = color;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }

            public void End()
            {
                Console.ForegroundColor = OriginColor;
            }

            public void Head()
            {
                OriginColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            public void Error()
            {
                OriginColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
            }

            public void Fatal()
            {
                OriginColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }

            public void Warning()
            {
                OriginColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
    }
}
