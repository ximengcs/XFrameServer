using Google.Protobuf.Reflection;
using NLog;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.Loader;
using XFrame.Core;
using XFrame.Core.Threads;
using XFrame.Modules.Config;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrame.Modules.ID;
using XFrame.Modules.Procedure;
using XFrame.Modules.Reflection;
using XFrame.Modules.Threads;
using XFrame.Tasks;
using XFrameServer.Test;
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

            GameConst.Initialize();
            InnerCreateServer();
        }

        #region Server
        private void InnerCreateServer()
        {
            Fiber serverFiber = Global.Fiber.GetOrNew(GameConst.FIBER_ID);
            serverFiber.StartThread();
            IScene serverScene = Global.Scene.Create(serverFiber);
            serverScene.Fiber.Post(InnerCreateServerScene, serverScene);
        }

        private void InnerCreateServerScene(object state)
        {
            IScene serverScene = state as IScene;
            if (string.IsNullOrEmpty(Init.Options.Host))
                Global.Net.Create(serverScene, NetMode.Server, 9999, XProtoType.Tcp);
            else
            {
                if (IPAddress.TryParse(Init.Options.Host, out IPAddress ipAddress))
                    Global.Net.Create(serverScene, NetMode.Server, ipAddress, 9999, XProtoType.Tcp);
            }
        }
        #endregion

        #region Client
        private void InnerCreateClient()
        {
            Fiber clientFiber = Global.Fiber.GetOrNew(GameConst.FIBER_ID);
            clientFiber.StartThread();
            IScene clientScene = Global.Scene.Create(clientFiber);
            clientScene.Fiber.Post(InnerCreateClientScene, clientScene);
        }

        private void InnerCreateClientScene(object state)
        {
            IScene clientScene = state as IScene;
            if (string.IsNullOrEmpty(Init.Options.Host))
                Global.Net.Create(clientScene, NetMode.Client, 9999, XProtoType.Tcp);
            else
            {
                if (IPAddress.TryParse(Init.Options.Host, out IPAddress ipAddress))
                    Global.Net.Create(clientScene, NetMode.Client, ipAddress, 9999, XProtoType.Tcp);
            }
        }
        #endregion
    }
}
