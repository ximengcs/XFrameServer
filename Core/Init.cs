﻿using XFrame.Core;
using XFrame.Modules.Config;
using XFrame.Modules.Diagnotics;
using XFrame.Tasks;
using XFrameServer.Core.Procedures;
using XFrameServer.Core.Logs;
using XFrame.Modules.Archives;
using XFrameServer.Core.Download;
using XFrameShare.Network;
using CommandLine;
using XFrameServer.Core.Commands;
using XFrame.Core.Threads;
using XFrameServer.Core.Platform;

namespace XFrameServer.Core
{
    public static class Init
    {
        public static bool Quit { get; set; }

        public static Options Options { get; private set; }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run);
        }

        private static void Run(Options option)
        {
            Options = option;
            DllPerchClass();
            Initialize();
            Start();
            long time = DateTime.Now.Ticks;
            long storeTime = 0;
            double second = 0;

#if WINDOWS
            FiberExtension.SleepBefore = WindowsExtension.TimeBeginPeriod;
            FiberExtension.SleepAfter = WindowsExtension.TimeEndPeriod;
#endif

            while (!Quit)
            {
                Update(second);
                AfterUpdate(second);
                FiberExtension.Sleep(10);
                long now = DateTime.Now.Ticks;
                //Log.Debug($"TIME2 {now} {time} {now - time} {storeTime}");
                time = now - time + storeTime;
                //Log.Debug($"TIME3 {time}");
                long escapeTime = time / TimeSpan.TicksPerMillisecond;
                second = escapeTime / 1000d;
                storeTime = time % TimeSpan.TicksPerMillisecond;
                time = now;
            }
            Destroy();
        }

        private static void DllPerchClass()
        {
            new XFrameSharePerch();
        }

        private static void InitializeLog()
        {
            switch (Options.Logger)
            {
                case LogType.console:
                    XConfig.DefaultLogger = typeof(ConsoleLogger).FullName;
                    break;

                case LogType.nlog:
                    XConfig.DefaultLogger = typeof(NLogLogger).FullName;
                    break;
            }
        }

        private static void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += InnerExpceptionHandler;
            XTaskHelper.ExceptionHandler += Log.Exception;
            InnerConfigType();
            InitializeLog();
            XConfig.Entrance = typeof(MainProcedure).FullName;
            XConfig.ArchivePath = "Data";
            XConfig.DefaultDownloadHelper = typeof(DownloadHelper).FullName;
            XConfig.TypeChecker = new TypeChecker();
            XConfig.DefaultIDHelper = typeof(NetEntityIDHelper).FullName;

            Entry.Init();
            Global.Fiber.MainFiber.Use();
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

        private static void Update(double time)
        {
            Entry.Trigger<IUpdater>(time);
            Entry.Trigger<ISaveable>();
        }

        private static void AfterUpdate(double time)
        {
            Entry.Trigger<IFinishUpdater>(time);
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
