using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels.PnlMusicTag;
using PopupLib.UI;

namespace PopupLib.Patches.IsSearchOpenFix
{
    [HarmonyPatch(typeof(PnlMusicTag), "OnEnablePnl")]
    class PnlMusicTag_OnEnablePnlPatch
    {
        static void Prefix()
        {
            PopupUtils.IsSearchOpen = true;
        }
    }
}
