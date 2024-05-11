using CommandLine;

namespace XFrameServer.Core.Commands
{
    public class Options
    {
        [Option("ip", Required = false, HelpText = "host address.")]
        public string Host { get; set; }

        [Option("log", Required = false, HelpText = "set logger.")]
        public LogType Logger { get; set; } = LogType.console;
    }

    public enum LogType
    {
        console,
        nlog
    }
}
