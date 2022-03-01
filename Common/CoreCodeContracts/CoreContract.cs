
using System.Diagnostics;

namespace Common.CoreCodeContracts
{
    public static class CoreContracts
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        [Conditional("DEBUG")] // the call is emitted in Debug build only
        public static void Precondition(bool condition, string message = null)
        {
            if (condition) return;
            System.Diagnostics.Debug.WriteLine("Precondition failure occurred - " + (message ?? "No message"));
            if (!Debugger.IsAttached)
                Debugger.Launch(); // a similar dialog, without the message, though
            else
                // if debugger is already attached, we break here
                Debugger.Break(); // CONTINUE PRESING F10 NEXT STEP 2 TIMES To SEE WHERE ASSERTIONS FAILS  
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        [Conditional("DEBUG")] // the call is emitted in Debug build only
        public static void Postcondition(bool condition, string message = null)
        {
            if (condition) return;
            System.Diagnostics.Debug.WriteLine("Postcondition failure occurred - " + (message ?? "No message"));
            if (!Debugger.IsAttached)
                Debugger.Launch(); // a similar dialog, without the message, though
            else
                // if debugger is already attached, we break here
                Debugger.Break(); // CONTINUE PRESING F10 NEXT STEP 2 TIMES To SEE WHERE ASSERTIONS FAILS  
        }

        [Conditional("DEBUG")] // the call is emitted in Debug build only
        internal static void WriteLine(string message) => System.Diagnostics.Debug.WriteLine(message);
    }
}
