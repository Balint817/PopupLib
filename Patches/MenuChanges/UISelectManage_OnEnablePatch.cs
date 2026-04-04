using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{

    [HarmonyPatch(typeof(UISelectManage), nameof(UISelectManage.OnEnable))]
    internal class UISelectManage_OnEnablePatch
    {
        private static void Prefix(UISelectManage __instance)
        {
            if (__instance.gameObject.name == "PnlHome")
            {
                PopupUtils.ActiveMenu = MenuType.MainMenu;
            }
        }
    }
}
