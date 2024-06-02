
using Retromono.Tweens;
using System.Numerics;
using XFrame.Core.Threads;

namespace XFrameServer.Core.Animations
{
    public class TweenAnimateVec3 : TweenAnimateBase
    {
        private readonly Vector3 _from;

        private readonly Vector3 _to;

        private readonly Action<Vector3> _updateCallback;
        private readonly Action _finishCallback;

        private Fiber _fiber;

        public TweenAnimateVec3(Fiber fiber, TimeSpan duration, Vector3 from, Vector3 to, Action<Vector3> updateCallback, Func<double, double> easingFunction = null, Action finishedCallback = null)
            : base(duration, easingFunction, null)
        {
            _from = from;
            _to = to;
            _fiber = fiber;
            _finishCallback = finishedCallback;
            _updateCallback = updateCallback ?? throw new ArgumentNullException("updateCallback", "Update callback cannot be null");
        }

        protected override void OnAdvance()
        {
            _fiber.Post((state) =>
            {
                var tuple = (ValueTuple<Action<Vector3>, Vector3>)state;
                tuple.Item1(tuple.Item2);
            }, ValueTuple.Create(_updateCallback, _from + (_to - _from) * (float)TimeFactor));

            if (IsFinished)
            {
                _fiber.Post((state) =>
                {
                    Action callback = (Action)state;
                    callback();
                }, _finishCallback);
            }
        }
    }
}
