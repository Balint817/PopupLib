using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlBackgroundSetting), nameof(PnlBackgroundSetting.Awake))]
    internal class PnlBackgroundSetting_AwakePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display_Brightness;
        }
    }
}
