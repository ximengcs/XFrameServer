
using XFrame.Modules.Entities;
using XFrame.Modules.Pools;
using XFrameServer.Test.Components;
using XFrameShare.Network;

namespace XFrameServer.Test
{
    [NetEntityComponent(typeof(Player))]
    public class PlayerComponent : PoolObjectBase, INetEntityComponent
    {
        private Player m_Player;

        public void OnInit(IEntity entity)
        {
            m_Player = entity as Player;
            m_Player.AddCom<PlayerMoveHandler>();
        }
    }
}
