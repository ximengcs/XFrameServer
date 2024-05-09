using Google.Protobuf.Reflection;
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
using XFrameShare.Network;
using XFrameShare.Test;

namespace XFrameServer.Core.Procedures
{
    public class MainProcedure : ProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            XRoot serverRoot = Entry.GetModule<IEntityModule>().Create<XRoot>();
            Entry.GetModule<NetworkModule>().Create(serverRoot, NetMode.Server, 9999);
            serverRoot.AddCom<World>();
        }
    }
}
