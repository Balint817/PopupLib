using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(GameMainPnlPause), nameof(GameMainPnlPause.Show), new Type[] { })]
    internal class GameMainPnlPause_ShowPatch
    {
        private static void Prefix()
        {
            PopupUtils.IsGamePaused = true;
        }
    }
}
