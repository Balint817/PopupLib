using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlFeverSetting), nameof(PnlFeverSetting.OnBackClicked))]
    class PnlFeverSetting_OnBackClickedPatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display;
        }
    }
}
