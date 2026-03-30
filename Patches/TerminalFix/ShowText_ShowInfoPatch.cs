using HarmonyLib;
using Il2CppAssets.Scripts.UI.Controls;

namespace PopupLib.Patches.TerminalFix
{
    /// <summary>
    /// Don't display the message that DBConfigUILocalization_GetLocalizationPatch sets to null
    /// </summary>
    [HarmonyPatch(typeof(ShowText), nameof(ShowText.ShowInfo))]
    class ShowText_ShowInfoPatch
    {
        public static bool Prefix(string info)
        {
            if (info == null)
            {
                return false;
            }
            return true;
        }
    }
}
