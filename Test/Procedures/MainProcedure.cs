using System.Net;
using XFrame.Core.Threads;
using XFrame.Modules.Entities;
using XFrame.Modules.Procedure;
using XFrame.Tasks;
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
            XTask.Delay(5).OnCompleted(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    InnerCreateClient();
                }
            }).Coroutine();
        }

        #region Server
        private void InnerCreateServer()
        {
            Fiber serverFiber = Global.Fiber.GetOrNew(GameConst.FIBER_ID);
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
            Fiber clientFiber = Global.Fiber.GetOrNew(GameConst.FIBER_ID);
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
