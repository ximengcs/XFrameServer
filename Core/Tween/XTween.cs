
using System.Numerics;
using XFrame.Core;
using XFrame.Core.Threads;

namespace XFrameServer.Core.Animations
{
    public static class XTween
    {
        public static void DoVec3(this Fiber fiber, Vector3 src, Vector3 tar, float duration, Action<Vector3> updateCallback, Action completeCallback = null)
        {
            Entry.GetModule<TweenModule>().ToVec3(fiber, new TimeSpan((long)(duration * TimeSpan.TicksPerSecond)), src, tar, updateCallback, completeCallback);
        }
    }
}
