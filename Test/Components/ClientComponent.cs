
using XFrame.Modules.Entities;
using XFrame.Modules.Pools;
using XFrameServer.Test.Components;
using XFrameShare.Network;

namespace XFrameServer.Test
{
    [NetEntityComponent(typeof(Client))]
    public class ClientComponent : Entity, INetEntityComponent
    {
        private Client m_Player;

        protected override void OnInit()
        {
            base.OnInit();
            m_Player = Parent as Client;
            m_Player.AddCom<ClientPropertyComponent>();
            m_Player.AddCom<ClientMoveComponent>();
            m_Player.AddHandler<SyncDataConsumer>();
        }
    }
}
