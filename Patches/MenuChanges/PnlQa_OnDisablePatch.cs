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
    class PnlQa_OnDisablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
