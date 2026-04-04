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
    [HarmonyPatch(typeof(PnlQaSelect), nameof(PnlQaSelect.OnDisablePnl))]
    internal class PnlQa_OnDisablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
