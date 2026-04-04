using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch]
    internal class PnlVictory_OnVictoryPatch
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(PnlVictory).GetMethods().Where(x => x.Name == nameof(PnlVictory.OnVictory));
        }
        private static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Victory;
        }
    }
}
