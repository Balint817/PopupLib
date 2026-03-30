//using HarmonyLib;
//using Il2CppAssets.Scripts.GameCore.Managers;
//using System.Runtime.InteropServices;
//using System;
//using Il2CppSystem.Collections.Generic;
//using Il2CppAssets.Scripts.UI.Panels.Bulletin;
//using BulletinController = Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinController;
//using BulletinDict = Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppSystem.Collections.Generic.List<Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinDataModel>>;
//using MelonLoader;

//namespace PopupLib.Patches.RefreshBulletinFix
//{
//    [HarmonyPatch(typeof(BulletinController), nameof(BulletinController.RefreshBulletinInfo))]
//    class BulletinManagerFix
//    {
//        internal static BulletinDict? StoreOriginal { get; private set; }
//        static bool Prefix([Optional] ref Il2CppSystem.Action<BulletinDict> callback)
//        {
//            var t = callback;
//            var t2 = (BulletinDict result) =>
//            {
//                StoreOriginal = result;
//                t?.Invoke(result);
//            };
//            callback = t2;
//            MelonLogger.Msg(StoreOriginal is null);
//            return StoreOriginal is null;
//        }
//    }
//}
