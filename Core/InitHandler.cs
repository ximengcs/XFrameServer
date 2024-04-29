using XFrame.Core;
using XFrame.Tasks;

namespace XFrameServer.Core
{
    public class InitHandler : IInitHandler
    {
        public async XTask AfterHandle()
        {
            await new XTaskCompleted();
        }

        public async XTask BeforeHandle()
        {
            await new XTaskCompleted();
        }

        public void EnterHandle()
        {

        }
    }
}
