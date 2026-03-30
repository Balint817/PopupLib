using HarmonyLib;
using Il2CppAssets.Scripts.GameCore.Managers;
using System.Runtime.InteropServices;
using System;
using Il2Cpp;
using UnityEngine;
using PopupLib.UI;
using PopupLib.UI.Windows;
using MelonLoader;
using Il2CppAssets.Scripts.PeroTools.Commons;
using UnityEngine.UI;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Il2CppAssets.Scripts.UI.Panels.Bulletin;
using PopupLib.UI.Components;

namespace PopupLib.Patches.RefreshBulletinFix
{
    [HarmonyPatch(typeof(ImageLoader), nameof(ImageLoader.LoadTexture2DFromLocal))]
    class BulletinLoadTexturePatch
    {
        public static readonly string PopupLibURLPrefix = "PopupLib://";
        internal static bool Prefix(ref string url, ref Il2CppSystem.Action<Texture2D> callback, ref bool __result)
        {
            if (WindowManager.FirstOrDefault() is not ForumWindow window)
            {
                return true;
            }
            if (url.StartsWith(PopupLibURLPrefix, StringComparison.Ordinal))
            {
                Debug.DevMsg(ConsoleColor.Cyan, $"Setting custom image \"{url}\"...");
                Texture2D? texture = null;

                while (true)
                {
                    url = url[PopupLibURLPrefix.Length..];
                    if (!uint.TryParse(url, out uint idx))
                    {
                        MelonLogger.Msg(ConsoleColor.DarkRed, $"Failed to parse '{url}'");
                        break;
                    }
                    var forumObjects = window.ForumObjects;
                    if (idx >= forumObjects.Count || idx > int.MaxValue)
                    {
                        MelonLogger.Msg(ConsoleColor.DarkRed, $"Invalid index '{idx}' (expected max {forumObjects.Count})");
                        break;
                    }
                    var newTexture = forumObjects[(int)idx].Texture;
                    if (newTexture != null)
                    {
                        texture = newTexture;
                    }
                    break;
                }
                texture ??= Utils.CreateDefaultTexture();
                if (texture != null)
                {
                    //Utils.ApplyBulletinAlpha(texture);
                    __result = true;
                    callback?.Invoke(texture);
                    return false;
                }
                else
                {
                    MelonLogger.Msg(ConsoleColor.DarkRed, $"Texture was unloaded!!! Letting the game call failCallback...");
                    url = "";
                }
            }
            else
            {
                Debug.DevMsg(ConsoleColor.DarkMagenta, $"Loading image from URL \"{url}\"...");
            }
            return true;
        }
    }
    //[HarmonyPatch]
    //class ImageLoader_LoadTexturePatch
    //{
    //    static IEnumerable<MethodBase> TargetMethods()
    //    {
    //        return AccessTools.GetDeclaredMethods(typeof(ImageLoader)).Where(x => x.Name == nameof(ImageLoader.ImageLoad));
    //    }
    //    static bool Prefix(string url, Il2CppSystem.Action<Texture2D> callback)
    //    {
    //        var b = false;
    //        return ImageLoader_LoadLocalTexturePatch.Prefix(url, callback, ref b);
    //    }
    //}
}
