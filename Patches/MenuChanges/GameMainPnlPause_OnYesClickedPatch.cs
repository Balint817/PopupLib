using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;
using System;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(GameMainPnlPause), nameof(GameMainPnlPause.OnYesClicked), new Type[] { })]
    class GameMainPnlPause_OnYesClickedPatch
    {
        static void Prefix()
        {
            PopupUtils.IsGamePaused = false;
        }
    }
}
