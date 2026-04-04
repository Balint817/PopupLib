using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(PnlInputPc), "OnEnablePnl")]
    //class PnlInputEnablePatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Controls;
    //    }
    //}
    [HarmonyPatch(typeof(PnlInputPc), nameof(PnlInputPc.OnDisablePnl))]
    internal class PnlInputPc_OnDisablePnlPatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
