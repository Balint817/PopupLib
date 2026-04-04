//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
//using HarmonyLib;
//using Il2CppAssets.Scripts.PeroTools.Managers;
//using Il2CppAssets.Scripts.UI.Specials;
//using Il2CppInterop.Common;
//using Il2CppInterop.Runtime;
//using MelonLoader.NativeUtils;
//using PopupLib;
//using PopupLib.UI;
//namespace PopupLib.Patches
//{
//    internal unsafe class AutoPushPopPanel_OnPnlHomeActiveChanged_HookPatch
//    {
//        private static readonly NativeHook<PatchDelegate> Hook = new();
//        internal static unsafe void AttachHook()
//        {
//            var method = AccessTools.Method(typeof(AutoPushPopPanel),
//                nameof(AutoPushPopPanel.OnPnlHomeActiveChanged), new[] { typeof(UnityEngine.GameObject), typeof(bool) });

//            if (method is null)
//            {
//                Debug.Error("FATAL ERROR: Patch failed.");
//                Thread.Sleep(1000);
//                Environment.Exit(1);
//            }

//            var methodPointer = *(IntPtr*)(IntPtr)Il2CppInteropUtils
//                .GetIl2CppMethodInfoPointerFieldForGeneratedMethod(method).GetValue(null)!;

//            // Create a pointer for our new method to be called instead
//            // This is Cdecl because this is going to be called in an unmanaged context
//            delegate* unmanaged[Cdecl]<IntPtr, IntPtr, IntPtr, IntPtr> detourPointer = &PatchMethod;

//            // Set the hook so that PatchMethod runs instead of the original
//            Hook.Detour = (IntPtr)detourPointer;
//            Hook.Target = methodPointer;
//            Hook.Attach();
//        }

//        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
//        private static IntPtr PatchMethod(IntPtr instance, IntPtr gameObjectPointer, IntPtr activePointer)
//        {
//            Debug.DevMsg("hook called");

//            var pointer = (object*)(int*)activePointer;
//            Debug.DevMsg("got pointer");
//            var obj = *pointer;
//            Console.WriteLine("got object");
//            Console.WriteLine($"object is null => {obj is null}");
//            if (obj is not null)
//            {
//                Console.WriteLine(obj.GetType().Name);
//            }

//            return Hook.Trampoline(instance, gameObjectPointer, activePointer);

//            return IntPtr.Zero;
//            var active = IL2CPP.PointerToValueGeneric<Il2CppSystem.Boolean>(activePointer, true, false);
//            if (active.m_value)
//            {
//                PopupUtils.IsSearchOpen = false;
//                PopupUtils.ActiveMenu = MenuType.MainMenu;
//            }

//            return IntPtr.Zero;
//        }

//        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
//        private delegate IntPtr PatchDelegate(IntPtr go, IntPtr active, IntPtr nativeMethodInfo);
//    }
//}