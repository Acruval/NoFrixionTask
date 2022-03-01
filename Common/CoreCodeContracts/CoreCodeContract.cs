


//This class thas the responsability get a better behaviour for Debug.Assert in .NET. Contrats are not availables in net core yet

#if !NETFRAMEWORK // this class is active for non-Framework platforms only
using System.Diagnostics;
using SystemDebug = System.Diagnostics.Debug;

namespace Common //MyRootNamespace // so in your project this will hide the original Debug class
{
    internal static class Debug
    {
        [Conditional("DEBUG")] // the call is emitted in Debug build only
        internal static void Assert(bool condition, string message = null)
        {
            if (condition) return;
            SystemDebug.WriteLine("Debug failure occurred - " + (message ?? "No message"));
            if (!Debugger.IsAttached)
                Debugger.Launch(); // a similar dialog, without the message, though
            else
                // if debugger is already attached, we break here
                Debugger.Break(); // CONTINUE PRESING F10 NEXT STEP 2 TIMES To SEE WHERE ASSERTIONS FAILS  
        }

        [Conditional("DEBUG")] // the call is emitted in Debug build only
        internal static void WriteLine(string message) => SystemDebug.WriteLine(message);

        // and if you use other methods from Debug, define also them here...
    }
}
#endif
