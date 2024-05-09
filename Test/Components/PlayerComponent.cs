
using XFrame.Modules.Entities;
using XFrame.Modules.Pools;
using XFrameServer.Test.Components;
using XFrameShare.Network;

namespace XFrameServer.Test
{
    [NetEntityComponent(typeof(Player))]
    public class PlayerComponent : Entity, INetEntityComponent
    {
        private Player m_Player;

        protected override void OnInit()
        {
            base.OnInit();
            m_Player = Parent as Player;
            m_Player.AddCom<SyncDataMessageHandler>();
            m_Player.AddCom<PlayerMoveHandler>();
        }
    }
}
