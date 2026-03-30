using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(PnlFail), nameof(PnlFail.OnEnable))]
    class PnlFail_OnEnablePatch
    {
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.FailScreen;
        }
    }
}
