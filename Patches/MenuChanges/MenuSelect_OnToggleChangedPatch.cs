using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(MenuSelect), nameof(MenuSelect.OnToggleChanged), new Type[] { typeof(int), typeof(int), typeof(bool) })]
    internal class MenuSelect_OnToggleChangedPatch
    {
        private static void Prefix(int listIndex, int index, bool isOn)
        {
            if (!isOn)
            {
                return;
            }
            switch ((Il2CppAssets.Scripts.UI.MenuType)index)
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
