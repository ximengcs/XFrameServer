using Retromono.Tweens;
using System.Numerics;
using XFrame.Core;
using XFrame.Core.Threads;

namespace XFrameServer.Core.Animations
{
    [CommonModule]
    public class TweenModule : ModuleBase, IUpdater
    {
        private List<TweenAnimateBase> m_Tweens;
        private List<TweenAnimateBase> m_Cache;

        protected override void OnInit(object data)
        {
            base.OnInit(data);
            m_Tweens = new List<TweenAnimateBase>();
            m_Cache = new List<TweenAnimateBase>();
        }

        void IUpdater.OnUpdate(double escapeTime)
        {
            m_Cache.Clear();
            m_Cache.AddRange(m_Tweens);
            foreach (var tween in m_Cache)
            {
                if (!tween.IsFinished)
                {
                    tween.Advance(new TimeSpan((long)(escapeTime * TimeSpan.TicksPerSecond)));
                }
                else
                {
                    m_Tweens.Remove(tween);
                }
            }
        }

        public TweenAnimateBase To(TimeSpan duration, double from, double to, Action<double> updateCallback)
        {
            TweenAnimateBase tween;
            lock (m_Tweens)
            {
                tween = new TweenAnimateDouble(duration, from, to, updateCallback);
                m_Tweens.Add(tween);
            }

            return tween;
        }

        public TweenAnimateBase ToVec3(Fiber fiber, TimeSpan duration, Vector3 from, Vector3 to, Action<Vector3> updateCallback, Action completeCallback)
        {
            TweenAnimateBase tween;
            lock (m_Tweens)
            {
                tween = new TweenAnimateVec3(fiber, duration, from, to, updateCallback, null, completeCallback);
                m_Tweens.Add(tween);
            }

            return tween;
        }
    }
}
