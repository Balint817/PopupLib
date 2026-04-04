using HarmonyLib;
using Il2CppAssets.Scripts.UI.Panels.Bulletin;
using MelonLoader;
using PopupLib.UI;
using PopupLib.UI.Windows;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PopupLib.Patches.ForumWindowClickDetection
{
    [HarmonyPatch(typeof(Toggle), nameof(Toggle.OnPointerClick))]
    internal class Toggle_OnPointerClickPatch
    {
        private static void Postfix(PointerEventData eventData, Toggle __instance)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                Debug.DevMsg($"Toggle patch skipped (not left click)");
                return;
            }
            if (ForumWindow.wrapper is null)
            {
                return;
            }
            var msgBox = ForumWindow.wrapper.MessageBox;
            if (msgBox is null)
            {
                Debug.DevMsg($"Toggle patch skipped (not loaded)");
                return;
            }
            var bulletin = msgBox.TryCast<PnlStageBulletinController>();
            if (bulletin is null)
            {
                MelonLogger.Msg(ConsoleColor.Red, $"Toggle patch skipped (couldn't cast wrapped msgbox to bulletin)");
                return;
            }
            var idx = bulletin.m_BulletinView.toggles.IndexOf(__instance);
            //ForumWindow.wrapper.MessageBox?.Cast<PnlBulletin>().m_Tgls.
            //try
            //{
            //    var bullets = BulletinManager.instance.bulletins["English"];
            //    foreach (var kv in bullets)
            //    {
            //        MelonLogger.Msg($"UID={kv.uid}, Title={kv.title}, ImageURL={kv.imageUrl}");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MelonLogger.Msg(ex.ToString());
            //}
            if (idx == -1)
            {
                Debug.DevMsg($"Toggle patch idx skipped ({idx})");
                return;
            }

            WindowManager.ForumWindow_OnToggle((int)idx);
        }

        private static void Finalizer(Exception __exception)
        {
            if (__exception != null)
            {
                MelonLogger.Error(__exception?.ToString());
            }
        }
    }
}










//using Il2CppAssets.Scripts.UI.Panels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using HarmonyLib;
//using PopupLib.UI.Windows;
//using PopupLib.UI;
//using MelonLoader;

//namespace PopupLib.Patches.ForumWindowClickDetection
//{
//    [HarmonyPatch(typeof(Toggle), "OnSelect")]
//    internal class Toggle_OnPointerClickPatch
//    {
//        static void Postfix(Toggle __instance)
//        {
//            if (__instance is not Toggle tgl)
//            {
//                return;
//            };
//            var idx = ForumWindow.wrapper.MessageBox?.Cast<PnlBulletin>().m_Tgls.IndexOf(tgl);
//            //ForumWindow.wrapper.MessageBox?.Cast<PnlBulletin>().m_Tgls.
//            if (idx == null || idx == -1)
//            {
//                Debug.DevMsg($"Toggle patch idx skipped ({idx?.ToString() ?? "null"})");
//                return;
//            }
//            WindowManager.ForumWindow_OnToggle((int)idx);
//        }

//        static void Finalizer(Exception __exception)
//        {
//            if (__exception != null)
//            {
//                MelonLogger.Error(__exception?.ToString());
//            }
//        }
//    }
//}