using XFrame.Core;
using XFrame.Modules.Config;
using XFrame.Modules.Diagnotics;
using XFrame.Tasks;
using XFrameServer.Core.Procedures;
using XFrameServer.Core.Logs;
using XFrameServer.Core.Network;
using XFrame.Modules.Archives;
using XFrame.Modules.Threads;

namespace XFrameServer.Core
{
    public static class Init
    {
        public static bool Quit { get; set; }

        public static void Main(string[] args)
        {
            Initialize();
            Start();
            while (!Quit)
            {
                Update();
                Thread.Sleep(1);
            }
            Destroy();
        }

        private static void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += InnerExpceptionHandler;
            XTaskHelper.ExceptionHandler += Log.Exception;
            InnerConfigType();
            XConfig.Entrance = typeof(MainProcedure).FullName;
            XConfig.DefaultLogger = typeof(Logger).FullName;
            XConfig.ArchivePath = "./Data/";
            XConfig.DefaultDownloadHelper = typeof(DownloadHelper).FullName;
            XConfig.TypeChecker = new TypeChecker();

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

        private static void Update()
        {
            Entry.Trigger<IUpdater>(1f);
            Entry.Trigger<ISaveable>();
        }

        private static void Destroy()
        {
            Entry.ShutDown();
        }

        private static void InnerConfigType()
        {
            TypeChecker.IncludeModule("XFrameServer");
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
