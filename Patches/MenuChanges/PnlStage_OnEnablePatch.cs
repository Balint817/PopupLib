using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppAssets.Scripts.UI.Specials;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{

    [HarmonyPatch(typeof(PnlStage), nameof(PnlStage.OnEnable))]
    class PnlStage_OnEnablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.LevelSelect;
        }
    }
}
