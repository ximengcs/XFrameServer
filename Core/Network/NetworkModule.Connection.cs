
using System.Text;
using System.Net.Sockets;
using XFrame.Modules.Diagnotics;
using XFrame.Tasks;

namespace XFrameServer.Core.Network
{
    public partial class NetworkModule
    {
        private class Connection
        {
            private Socket m_Server;
            private Socket m_Client;

            private byte[] m_Cache;
            private byte[] m_BeatData;

            public bool Connecting { get; private set; }

            public Connection(Socket server)
            {
                m_Server = server;
                m_Cache = new byte[1024];
                m_BeatData = new byte[8];
                Start();
            }

            public void Update()
            {
                if (Connecting)
                {
                    InnerReceiveData().Coroutine();
                    InnerBeat();
                }
            }

            private async XTask InnerReceiveData()
            {
                try
                {
                    int byteCount = await m_Client.ReceiveAsync(m_Cache);
                    string content = Encoding.UTF8.GetString(m_Cache, 0, byteCount);
                    Log.Debug(content);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
                finally
                {
                    Connecting = false;
                }
            }

            private void InnerBeat()
            {
                try
                {
                    m_Client.Send(m_BeatData);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
                finally
                {
                    Connecting = false;
                }
            }

            public async void Start()
            {
                m_Client = await m_Server.AcceptAsync();
                Connecting = true;
            }
        }
    }
}
