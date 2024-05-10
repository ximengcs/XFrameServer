
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrameServer.Test.Entities;
using XFrameShare.Network;
using XFrameShare.Test;

namespace XFrameServer.Test.Components
{
    [NetEntityComponent(typeof(Game))]
    public class GameComponent : Entity, INetEntityComponent
    {
        private Game m_Game;

        protected override void OnInit()
        {
            base.OnInit();
            Log.Debug(Parent == null);
            m_Game = Parent as Game;
            m_Game.AddCom<World>();
        }
    }
}
