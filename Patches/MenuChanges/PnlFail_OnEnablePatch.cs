using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlFail), nameof(PnlFail.OnEnable))]
    internal class PnlFail_OnEnablePatch
    {
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.FailScreen;
        }
    }
}
