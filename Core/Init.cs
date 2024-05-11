using XFrame.Core;
using XFrame.Modules.Config;
using XFrame.Modules.Diagnotics;
using XFrame.Tasks;
using XFrameServer.Core.Procedures;
using XFrameServer.Core.Logs;
using XFrame.Modules.Archives;
using System.Diagnostics;
using XFrameServer.Core.Download;
using XFrameShare.Network;
using NLog;

namespace XFrameServer.Core
{
    public static class Init
    {
        public static bool Quit { get; set; }

        public static void Main(string[] args)
        {
            DllPerchClass();
            Initialize();
            Start();
            Stopwatch sw = new Stopwatch();
            float time = 0;
            while (!Quit)
            {
                sw.Restart();
                Update(time / 1000);
                Thread.Sleep(1);
                sw.Stop();
                time = sw.ElapsedMilliseconds;
            }
            Destroy();
        }

        private static void DllPerchClass()
        {
            new XFrameSharePerch();
        }

        private static void InitializeNLog()
        {
            LogManager.Setup().LoadConfiguration((builder) =>
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                builder.ForLogger().FilterLevel(NLog.LogLevel.Debug).WriteToFile($"logs/nlog-debug-{time}.txt");
                builder.ForLogger().FilterLevel(NLog.LogLevel.Warn).WriteToFile($"logs/nlog-warning-{time}.txt");
                builder.ForLogger().FilterLevel(NLog.LogLevel.Error).WriteToFile($"logs/nlog-error-{time}.txt");
                builder.ForLogger().FilterLevel(NLog.LogLevel.Fatal).WriteToFile($"logs/nlog-fatal-{time}.txt");
            });
        }

        private static void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += InnerExpceptionHandler;
            XTaskHelper.ExceptionHandler += Log.Exception;
            InnerConfigType();
            InitializeNLog();
            XConfig.Entrance = typeof(MainProcedure).FullName;
            XConfig.DefaultLogger = typeof(NLogLogger).FullName;
            XConfig.ArchivePath = "Data";
            XConfig.DefaultDownloadHelper = typeof(DownloadHelper).FullName;
            XConfig.TypeChecker = new TypeChecker();
            XConfig.DefaultIDHelper = typeof(NetEntityIDHelper).FullName;

            Entry.Init();
        }

        private static void InnerExpceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Exception((Exception)e.ExceptionObject);
        }

        private static void Start()
        {
            Log.ConsumeWaitQueue();
            Log.ToQueue = false;
            Entry.Start();
        }

        private static void Update(float time)
        {
            Entry.Trigger<IUpdater>(time);
            Entry.Trigger<ISaveable>();
        }

        private static void Destroy()
        {
            Entry.ShutDown();
        }

        private static void InnerConfigType()
        {
            TypeChecker.IncludeModule("XFrameServer");
            TypeChecker.IncludeModule("XFrameShare");
            TypeChecker.IncludeModule("UnityXFrame");
            TypeChecker.IncludeModule("UnityXFrame.Lib");
            TypeChecker.ExcludeNameSpace("CommandLine");
            TypeChecker.ExcludeNameSpace("CommandLine.Core");
            TypeChecker.ExcludeNameSpace("CommandLine.Infrastructure");
            TypeChecker.ExcludeNameSpace("CommandLine.Text");
            TypeChecker.ExcludeNameSpace("CSharpx");
            TypeChecker.ExcludeNameSpace("CsvHelper");
            TypeChecker.ExcludeNameSpace("CsvHelper.Configuration");
            TypeChecker.ExcludeNameSpace("CsvHelper.Configuration.Attributes");
            TypeChecker.ExcludeNameSpace("CsvHelper.Delegates");
            TypeChecker.ExcludeNameSpace("CsvHelper.Expressions");
            TypeChecker.ExcludeNameSpace("CsvHelper.TypeConversion");
            TypeChecker.ExcludeNameSpace("RailwaySharp.ErrorHandling");
        }
    }
}
