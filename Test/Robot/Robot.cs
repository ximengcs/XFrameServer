using Google.Protobuf.WellKnownTypes;
using System.Numerics;
using TestGame.Share.Clients;
using TestGame.Share.Servers;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Entities;
using XFrame.Tasks;
using XFrameServer.Core.Animations;
using XFrameShare.Network;

namespace XFrameServer.Test.Robot
{
    [NetEntityComponent(typeof(Client), NetMode.Client)]
    public class Robot : Entity, INetEntityComponent
    {
        private Client m_Client;
        private PlayerMoveComponent m_Movement;

        protected override void OnInit()
        {
            base.OnInit();
            m_Client = Parent as Client;
        }

        public void OnReady()
        {
            m_Movement = m_Client.GetHandlerInstance<PlayerMoveComponent>();

            if (m_Client.IsSelf())
            {
                XTask.Beat(1, () =>
                {
                    m_Movement.Move(new Vector3(InnerNext(), InnerNext(), 0));
                    return false;
                }).Coroutine();
            }
        }

        private float InnerNext()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return (rand.Next() % 2 == 0 ? 1 : -1) * rand.NextSingle() * 10000 % 10;
        }

        private void InnerMove()
        {
            Vector3 start = Vector3.Zero;
            Vector3 target = new Vector3(InnerNext(), InnerNext(), InnerNext());
            Scene.Fiber.DoVec3(start, target, 2, (value) =>
            {
                m_Movement.Move(value - start);
                start = value;
            }, InnerMove);
        }
    }
}
