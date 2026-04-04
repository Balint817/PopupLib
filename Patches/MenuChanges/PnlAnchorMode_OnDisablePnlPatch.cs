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
    internal class PnlAnchorMode_OnDisablePnlPatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
