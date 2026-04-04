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
    internal class PnlOffsetOption_OnDisablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
