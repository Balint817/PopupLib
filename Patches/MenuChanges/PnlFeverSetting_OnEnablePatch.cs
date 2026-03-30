using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    //[HarmonyPatch(typeof(PnlBackgroundSetting), "OnBackClicked")]
    //class PnlBackgroundBackPatch
    //{
    //    static void Prefix()
    //    {
    //        PopupUtils.ActiveMenu = MenuType.Settings_Display;
    //    }
    //}


    [HarmonyPatch(typeof(PnlFeverSetting), nameof(PnlFeverSetting.OnEnable))]
    class PnlFeverSetting_OnEnablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display_FeverBG;
        }
    }
}
