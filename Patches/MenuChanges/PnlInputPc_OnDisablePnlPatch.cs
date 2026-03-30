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
    class PnlInputPc_OnDisablePnlPatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
