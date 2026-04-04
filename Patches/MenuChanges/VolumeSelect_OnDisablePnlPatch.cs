using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(VolumeSelect), "OnEnablePnl")]
    //class PnlVolumeEnablePatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Audio;
    //    }
    //}
    [HarmonyPatch(typeof(VolumeSelect), nameof(VolumeSelect.OnDisablePnl))]
    internal class VolumeSelect_OnDisablePnlPatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
