using HarmonyLib;
using Il2CppAssets.Scripts.Database;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PopupLib.Patches.TerminalFix
{
    /// <summary>
    /// For removing the error message from the terminal
    /// </summary>
    [HarmonyPatch]
    internal class DBConfigUILocalization_GetLocalizationPatch
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(DBConfigUILocalization)
                .GetMethods()
                .Where(method => method.Name == "GetLocalization")
                .Cast<MethodBase>();
        }
        public static bool Prefix(ref string __result, string key)
        {
            switch (key)
            {
                case "terminal_network_error":
                case "terminal_not_login":
                    __result = null!;
                    return false;
                default:
                    return true;
            }
        }
    }
}
