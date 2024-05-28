
using XFrame.Core.Threads;

namespace XFrameServer.Test
{
    public static class GameConst
    {
        private static Fiber s_NetFiber;
        public static Fiber NetFiber => s_NetFiber;

        public static void Initialize()
        {
            s_NetFiber = Global.Fiber.GetOrNew(1);
            s_NetFiber.StartThread(1);
            Global.Net.SetFiber(s_NetFiber);
        }
    }
}
