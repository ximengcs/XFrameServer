
using CommandLine;
using System.Net;

namespace XFrameServer.Core.Commands
{
    public class Options
    {
        [Option("ip", Required = false, HelpText = "host address.")]
        public string Host { get; set; }
    }
}
