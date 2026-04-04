using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(PnlCredits), "OnEnable")]
    //class PnlCreditsEnablePatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Credits;
    //    }
    //}
    [HarmonyPatch(typeof(PnlCredits), nameof(PnlCredits.OnDisable))]
    internal class PnlCredits_OnDisablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
