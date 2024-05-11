﻿using Google.Protobuf.Reflection;
using NLog;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;
using XFrame.Core;
using XFrame.Modules.Config;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrame.Modules.ID;
using XFrame.Modules.Procedure;
using XFrame.Modules.Reflection;
using XFrame.Tasks;
using XFrameServer.Test.Entities;
using XFrameShare.Network;
using XFrameShare.Test;

namespace XFrameServer.Core.Procedures
{
    public class MainProcedure : ProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            Game serverRoot = Entry.GetModule<IEntityModule>().Create<Game>();
            Entry.GetModule<NetworkModule>().Create(serverRoot, NetMode.Server, 9999);
        }
    }
}
