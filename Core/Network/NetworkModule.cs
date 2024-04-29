using System.Net;
using XFrame.Core;
using System.Net.Sockets;

namespace XFrameServer.Core.Network
{
    [CommonModule]
    public partial class NetworkModule : ModuleBase, IUpdater
    {
        private IPHostEntry m_IPHost;
        private IPAddress m_IPAddress;
        private IPEndPoint m_IPEndPoint;
        private Socket m_Listener;

        private Connection m_Connect;

        protected override void OnInit(object data)
        {
            base.OnInit(data);
            m_IPHost = Dns.GetHostEntry(Dns.GetHostName());
            m_IPAddress = m_IPHost.AddressList[0];
            m_IPEndPoint = new IPEndPoint(m_IPAddress, 9999);
            m_Listener = new Socket(m_IPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            m_Listener.Bind(m_IPEndPoint);
            m_Listener.Listen(1000);
        }

        void IUpdater.OnUpdate(float escapeTime)
        {
            if (m_Connect == null)
                m_Connect = new Connection(m_Listener);
            if (m_Connect != null)
                m_Connect.Update();
        }
    }
}
