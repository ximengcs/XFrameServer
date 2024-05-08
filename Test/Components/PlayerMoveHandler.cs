using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrameShare.Core.Network;

namespace XFrameServer.Test.Components
{
    public class PlayerMoveHandler : Entity, IMessageHandler
    {
        private Player m_Player;

        public Type Type => typeof(TransformMessage);

        protected override void OnInit()
        {
            base.OnInit();
            m_Player = Parent as Player;
        }

        public void OnReceive(TransData data)
        {
            TransformMessage message = data.Message as TransformMessage;
        }
    }
}
