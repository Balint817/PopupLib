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
    class PnlGraphicSetting_OnDisablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings;
        }
    }
}
