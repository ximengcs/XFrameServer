using Google.Protobuf;
using System.Numerics;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrameShare.Network;

namespace XFrameServer.Test.Components
{
    public class PlayerMoveHandler : Entity, IMessageHandler, IFactoryMessage
    {
        private Client m_Client;

        Type IMessageHandler.Type => typeof(TransformRequestMessage);

        Type IFactoryMessage.Type => typeof(TransformExcuteMessage);

        public IMessage Message => new TransformExcuteMessage()
        {
            X = m_Client.Pos.X,
            Y = m_Client.Pos.Y,
            Z = m_Client.Pos.Z,
        };

        protected override void OnInit()
        {
            base.OnInit();
            m_Client = Parent as Client;
        }

        public void OnReceive(TransitionData data)
        {
            Log.Debug(NetConst.Net, $"transform request {data.Message}");
            TransformRequestMessage message = data.Message as TransformRequestMessage;
            m_Client.Pos = new Vector3(message.X, message.Y, message.Z) * 0.5f;
            m_Client.Send(this);
        }
    }
}
