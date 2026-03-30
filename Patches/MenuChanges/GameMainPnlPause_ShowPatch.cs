using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(GameMainPnlPause), nameof(GameMainPnlPause.Show), new Type[] { })]
    class GameMainPnlPause_ShowPatch
    {
        static void Prefix()
        {
            PopupUtils.IsGamePaused = true;
        }
    }
}
