using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels.PnlDLC;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlDlc), nameof(PnlDlc.OnDisable), new Type[] { })]
    internal class PnlDlc_OnDisablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.LevelSelect;
        }
    }
}
