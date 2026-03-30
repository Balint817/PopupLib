using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(MenuSelect), nameof(MenuSelect.SetOn))]
    class MenuSelect_SetOnPatch
    {
        static void Prefix(Il2CppAssets.Scripts.UI.MenuType type)
        {
            PopupUtils.IsSearchOpen = false;
            switch (type)
            {
                case Il2CppAssets.Scripts.UI.MenuType.Option:
                    PopupUtils.ActiveMenu = MenuType.Settings;
                    break;
                case Il2CppAssets.Scripts.UI.MenuType.Elfin:
                    PopupUtils.ActiveMenu = MenuType.Elfins;
                    break;
                case Il2CppAssets.Scripts.UI.MenuType.Role:
                    PopupUtils.ActiveMenu = MenuType.Characters;
                    break;
                case Il2CppAssets.Scripts.UI.MenuType.Trove:
                    PopupUtils.ActiveMenu = MenuType.Trove;
                    break;
                case Il2CppAssets.Scripts.UI.MenuType.Achv:
                    PopupUtils.ActiveMenu = MenuType.Achievements;
                    break;
                default:
                    break;
            }
        }
    }
}
