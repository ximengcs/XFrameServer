using Google.Protobuf;
using System.Numerics;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrameShare.Network;

namespace XFrameServer.Test.Components
{
    public class ClientMoveHandler : Entity, IMessageHandler, IFactoryMessage
    {
        private Client m_Client;
        private ClientPropertyComponent m_Prop;

        Type IMessageHandler.Type => typeof(TransformRequestMessage);

        Type IFactoryMessage.Type => typeof(TransformExcuteMessage);

        public IMessage Message => new TransformExcuteMessage()
        {
            X = m_Prop.Pos.X,
            Y = m_Prop.Pos.Y,
            Z = m_Prop.Pos.Z,
        };

        protected override void OnInit()
        {
            base.OnInit();
            m_Client = Parent as Client;
            m_Prop = m_Client.GetCom<ClientPropertyComponent>();
        }

        public void OnReceive(TransitionData data)
        {
            Log.Debug(NetConst.Net, $"transform request {data.Message}");
            TransformRequestMessage message = data.Message as TransformRequestMessage;
            m_Prop.Pos += new Vector3(message.X, message.Y, message.Z) * 0.5f;
            m_Client.Send(this);
        }
    }
}
