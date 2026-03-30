using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlBackgroundSetting), nameof(PnlBackgroundSetting.OnDisable))]
    class PnlBackgroundSetting_OnDisablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display;
        }
    }
}
