using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts.UI.Panels.PnlDLC;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlDlc), nameof(PnlDlc.OnEnable), new Type[] { })]
    class PnlDlc_OnEnablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Shop;
        }
    }
}
