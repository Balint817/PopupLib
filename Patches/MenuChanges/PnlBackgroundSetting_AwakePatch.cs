using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlBackgroundSetting), nameof(PnlBackgroundSetting.Awake))]
    class PnlBackgroundSetting_AwakePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Settings_Display_Brightness;
        }
    }
}
