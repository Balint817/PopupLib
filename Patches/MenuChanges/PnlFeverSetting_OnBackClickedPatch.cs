using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlFeverSetting), nameof(PnlFeverSetting.OnBackClicked))]
    internal class PnlFeverSetting_OnBackClickedPatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display;
        }
    }
}
