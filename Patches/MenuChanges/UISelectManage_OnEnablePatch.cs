using HarmonyLib;
using Il2Cpp;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppAssets.Scripts.UI.Specials;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{

    [HarmonyPatch(typeof(UISelectManage), nameof(UISelectManage.OnEnable))]
    class UISelectManage_OnEnablePatch
    {
        static void Prefix(UISelectManage __instance)
        {
            if (__instance.gameObject.name == "PnlHome")
            {
                PopupUtils.ActiveMenu = MenuType.MainMenu;
            }
        }
    }
}
