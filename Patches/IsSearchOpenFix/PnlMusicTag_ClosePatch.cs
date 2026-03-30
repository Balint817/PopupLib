using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels.PnlMusicTag;
using PopupLib.UI;
using System.Reflection;
using System.Collections.Generic;

namespace PopupLib.Patches.IsSearchOpenFix
{
    [HarmonyPatch]
    class PnlMusicTag_ClosePatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            //yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.OnBgShutClicked));
            //yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.OnCellClicked));
            //yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.OnOkClicked));
            //yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.Close));
            //yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.Destroy));
            //yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.OnDisablePnl));
            yield return AccessTools.Method(typeof(PnlMusicTag), nameof(PnlMusicTag.OnDisable));
        }
        static void Prefix()
        {
            PopupUtils.IsSearchOpen = false;
        }
    }
}
