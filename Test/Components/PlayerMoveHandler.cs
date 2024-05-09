using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrameShare.Network;

namespace XFrameServer.Test.Components
{
    public class PlayerMoveHandler : Entity, IMessageHandler, IFactoryMessage
    {
        private Player m_Player;

        public Type Type => typeof(TransformRequestMessage);

        public IMessage Message => new TransformExcuteMessage()
        {
            X = m_Player.Pos.X,
            Y = m_Player.Pos.Y,
            Z = m_Player.Pos.Z,
        };

        protected override void OnInit()
        {
            base.OnInit();
            m_Player = Parent as Player;
        }

        public void OnReceive(TransData data)
        {
            Log.Debug(NetConst.Net, $"transform request {data.Message}");
            TransformRequestMessage message = data.Message as TransformRequestMessage;
            m_Player.Pos = new Vector3(message.X, message.Y, message.Z) * 0.5f;
            m_Player.Send(this);
        }
    }
}
