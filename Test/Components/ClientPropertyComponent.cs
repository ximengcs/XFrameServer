
using System.Numerics;
using XFrame.Modules.Entities;

namespace XFrameServer.Test.Components
{
    public class ClientPropertyComponent : Entity
    {
        private Vector3 m_Pos;

        public Vector3 Pos
        {
            get { return m_Pos; }
            set { m_Pos = value; }
        }
    }
}
