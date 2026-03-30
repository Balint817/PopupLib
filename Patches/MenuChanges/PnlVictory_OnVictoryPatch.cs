using HarmonyLib;
using Il2Cpp;
using PopupLib.UI;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch]
    class PnlVictory_OnVictoryPatch
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            return typeof(PnlVictory).GetMethods().Where(x => x.Name == nameof(PnlVictory.OnVictory));
        }
        static void Prefix()
        {
            PopupUtils.ActiveMenu = MenuType.Victory;
        }
    }
}
