using System.Runtime.InteropServices;
using System.Security;

namespace XFrameServer.Core.Platform
{
#if WINDOWS
    public static class WindowsExtension
    {
        /// <summary>TimeBeginPeriod(). See the Windows API documentation for details.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        public static void TimeBeginPeriod()
        {
            TimeBeginPeriod(1);
        }

        /// <summary>TimeEndPeriod(). See the Windows API documentation for details.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        public static void TimeEndPeriod()
        {
            TimeEndPeriod(1);
        }

    }
#endif
}
