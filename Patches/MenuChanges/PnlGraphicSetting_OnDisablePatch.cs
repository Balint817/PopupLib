using HarmonyLib;
using Il2CppUI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(PnlGraphicSetting), "Awake")]
    //class PnlGraphicEnablePatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Display;
    //    }
    //}
    [HarmonyPatch(typeof(PnlGraphicSetting), nameof(PnlGraphicSetting.OnDisable))]
    internal class PnlGraphicSetting_OnDisablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
