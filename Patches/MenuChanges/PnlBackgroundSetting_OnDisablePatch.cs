using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlBackgroundSetting), nameof(PnlBackgroundSetting.OnDisable))]
    internal class PnlBackgroundSetting_OnDisablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display;
        }
    }
}
