using Retromono.Tweens;
using System.Net;
using System.Net.Sockets;
using XFrame.Core;
using XFrame.Core.Threads;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrame.Modules.Procedure;
using XFrame.Tasks;
using XFrameServer.Core.Animations;
using XFrameServer.Test;
using XFrameShare.Network;

namespace XFrameServer.Core.Procedures
{
    public class MainProcedure : ProcedureBase
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            GameConst.Initialize();
            InnerCreateServer();
            //InnerTestClient(20);
        }

        private void InnerTestClient(int count)
        {
            if (count <= 0)
                return;
            XTask.Delay(1).OnCompleted(() =>
            {
                InnerCreateClient();
                InnerTestClient(count - 1);
            }).Coroutine();
        }

        #region Server
        private void InnerCreateServer()
        {
            Fiber serverFiber = Global.Fiber.GetOrNew(GameConst.SERVER_STR, GameConst.FIBER_ID);
            serverFiber.StartThread(10);
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
            Fiber clientFiber = Global.Fiber.GetOrNew(GameConst.CLIENT_STR, GameConst.FIBER_ID);
            clientFiber.StartThread(10);
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
