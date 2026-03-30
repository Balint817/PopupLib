using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(PnlAnchorMode), "OnEnablePnl")]
    //class PnlAnchorEnablePatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Streamer;
    //    }
    //}
    [HarmonyPatch(typeof(PnlAnchorMode), nameof(PnlAnchorMode.OnDisablePnl))]
    class PnlAnchorMode_OnDisablePnlPatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
