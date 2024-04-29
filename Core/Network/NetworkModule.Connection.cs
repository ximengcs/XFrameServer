using System.Text;
using System.Net.Sockets;
using XFrame.Modules.Diagnotics;
using XFrame.Tasks;

namespace XFrameServer.Core.Network
{
    public partial class NetworkModule
    {
        private class Client
        {
            private Socket m_Socket;
            private XTask m_CheckTask;
            private XTask m_BeatTask;
            private bool m_IsConnected;

            private byte[] m_Cache;
            private byte[] m_BeatCache;
            private bool m_BeatFinish;

            public bool Connect => m_IsConnected && m_Socket.Connected;

            public Client(Socket socket)
            {
                Log.Debug($"[{GetHashCode()}]client connect");
                m_Socket = socket;
                m_Cache = new byte[1024];
                m_IsConnected = true;
                m_BeatCache = BitConverter.GetBytes(98259);
            }

            private async XTask InnerReceiveData()
            {
                int byteCount = await m_Socket.ReceiveAsync(m_Cache);
                if (byteCount == 4)
                {
                    int code = BitConverter.ToInt32(m_Cache, 0);
                    Log.Debug($"[{GetHashCode()}]beat return {code}");
                    if (code == 98259)
                        m_BeatFinish = true;
                }
                else
                {
                    string content = Encoding.UTF8.GetString(m_Cache, 0, byteCount);
                    Log.Debug($"[{GetHashCode()}]receive data {content}");
                    ArraySegment<byte> data = new ArraySegment<byte>(Encoding.UTF8.GetBytes("server return data"));
                    await m_Socket.SendAsync(data);
                }
                m_CheckTask = null;
            }

            private async XTask InnerCheckBeat()
            {
                try
                {
                    m_BeatFinish = false;
                    int byteCount = await m_Socket.SendAsync(m_BeatCache);
                    await XTask.Delay(1);
                    if (!m_BeatFinish)
                        m_IsConnected = false;
                }
                catch
                {
                    m_IsConnected = false;
                }
                m_BeatTask = null;
            }

            public void Check()
            {
                if (!Connect)
                    return;

                if (m_CheckTask == null)
                {
                    m_CheckTask = InnerReceiveData();
                    m_CheckTask.Coroutine();
                }

                if (m_BeatTask == null)
                {
                    m_BeatTask = InnerCheckBeat();
                    m_BeatTask.Coroutine();
                }
            }

            public void Dispose()
            {
                Log.Debug($"[{GetHashCode()}]client disconnect {GetHashCode()}");
                if (m_CheckTask != null)
                {
                    m_CheckTask.Cancel(false);
                    m_CheckTask = null;
                }
                if (m_Socket != null)
                {
                    m_Socket.Close();
                    m_Socket = null;
                }
            }
        }

        private class Connection
        {
            private Socket m_Server;
            private List<Client> m_Clients;
            private XTask<Client> m_CheckTask;

            public Connection(Socket server)
            {
                m_Clients = new List<Client>();
                m_Server = server;
            }

            public void Update()
            {
                InnerCheckClient();
                for (int i = m_Clients.Count - 1; i >= 0; i--)
                {
                    Client client = m_Clients[i];
                    //Log.Debug($"update {client.GetHashCode()} {client.Connect}");
                    if (!client.Connect)
                    {
                        client.Dispose();
                        m_Clients.RemoveAt(i);
                    }
                    else
                    {
                        client.Check();
                    }
                }
            }

            private void InnerCheckClient()
            {
                if (m_CheckTask == null)
                {
                    m_CheckTask = Start();
                    m_CheckTask.Coroutine();
                }
            }

            public async XTask<Client> Start()
            {
                Log.Debug("start accept");
                Socket socket = await m_Server.AcceptAsync();
                m_CheckTask = null;
                Log.Debug("accept client");
                Client client = new Client(socket);
                m_Clients.Add(client);
                return client;
            }
        }
    }
}
