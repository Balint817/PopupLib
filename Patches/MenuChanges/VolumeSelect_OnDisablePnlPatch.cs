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
    class VolumeSelect_OnDisablePnlPatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
