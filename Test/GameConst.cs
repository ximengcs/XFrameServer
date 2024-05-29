﻿
using XFrame.Core.Threads;

namespace XFrameServer.Test
{
    public static class GameConst
    {
        private const int NET_FIBER = 1;

        private static Fiber s_NetFiber;
        public static Fiber NetFiber => s_NetFiber;

        public static void Initialize()
        {
            s_NetFiber = Global.Fiber.GetOrNew(NET_FIBER);
            s_NetFiber.StartThread(1);
            Global.Net.SetFiber(s_NetFiber);
        }
    }
}
