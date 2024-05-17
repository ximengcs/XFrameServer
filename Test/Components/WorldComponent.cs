using XFrame.Modules.Entities;
using XFrameShare.Network;
using XFrameShare.Test;

namespace XFrameServer.Test.Components
{
    [NetEntityComponent(typeof(World))]
    public class WorldComponent : Entity, INetEntityComponent
    {
        private World m_World;

        protected override void OnInit()
        {
            base.OnInit();
            m_World = Parent as World;
            m_World.AddCom<MailBoxCom>();
        }
    }
}
