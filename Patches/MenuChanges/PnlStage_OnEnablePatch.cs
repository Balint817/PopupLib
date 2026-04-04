using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{

    [HarmonyPatch(typeof(PnlStage), nameof(PnlStage.OnEnable))]
    internal class PnlStage_OnEnablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.LevelSelect;
        }
    }
}
