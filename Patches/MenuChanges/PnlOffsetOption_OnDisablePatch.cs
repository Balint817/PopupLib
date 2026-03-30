using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(PnlOffsetOption), "OnEnable")]
    //class PnlOffsetEnablePatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Offset;
    //    }
    //}
    [HarmonyPatch(typeof(PnlOffsetOption), nameof(PnlOffsetOption.OnDisable))]
    class PnlOffsetOption_OnDisablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
