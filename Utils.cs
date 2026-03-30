using MelonLoader;
using System.Reflection;
using System;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopupLib
{
    public static class Utils
    {
        public static T WaitTask<T>(Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
        public static byte[]? GetResource(Assembly assembly, string fileName)
        {
            return WaitTask(GetByteResourcePrivate(assembly, fileName, null));
        }
        public static string? GetStringResource(Assembly assembly, string fileName)
        {
            return WaitTask(GetStringResourcePrivate(assembly, fileName, null));
        }
        public static async Task<byte[]?> GetResourceAsync(Assembly assembly, string fileName)
        {
            return await GetByteResourcePrivate(assembly, fileName, null);
        }
        public static async Task<string?> GetStringResourceAsync(Assembly assembly, string fileName)
        {
            return await GetStringResourcePrivate(assembly, fileName, null);
        }
        private static async Task<string?> GetStringResourcePrivate(Assembly assembly, string fileName, Exception? innerException)
        {
            var resourceNames = assembly.GetManifestResourceNames().Where(x => x.EndsWith(fileName)).ToArray();
            string resourceName;
            switch (resourceNames.Length)
            {
                case 0:
                    throw new KeyNotFoundException($"the resource \"{fileName}\" couldn't be found", innerException);
                case 1:
                    resourceName = resourceNames[0];
                    break;
                default:
                    var ex = new AmbiguousMatchException($"the resource \"{fileName}\" got multiple results", innerException);
                    return await GetStringResourcePrivate(assembly, "." + fileName, ex);
            }

            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
            {
                return null;
            }
            using (var streamReader = new StreamReader(stream))
            {
                return await streamReader.ReadToEndAsync();
            };
        }
        private static async Task<byte[]?> GetByteResourcePrivate(Assembly assembly, string fileName, Exception? innerException)
        {
            var resourceNames = assembly.GetManifestResourceNames().Where(x => x.EndsWith(fileName)).ToArray();
            string resourceName;
            switch (resourceNames.Length)
            {
                case 0:
                    throw new KeyNotFoundException($"the resource \"{fileName}\" couldn't be found", innerException);
                case 1:
                    resourceName = resourceNames[0];
                    break;
                default:
                    var ex = new AmbiguousMatchException($"the resource \"{fileName}\" got multiple results", innerException);
                    return await GetByteResourcePrivate(assembly, "." + fileName, ex);
            }
            var checkNull = assembly.GetManifestResourceStream(resourceName);
            if (checkNull is null)
            {
                return null;
            }
            using (var stream = checkNull)
            {
                var result = new byte[stream.Length];
                var remaining = stream.Length;
                var index = 0;
                while (remaining > int.MaxValue)
                {
                    await stream.ReadAsync(result.AsMemory(index, int.MaxValue));
                    index += int.MaxValue;
                    remaining -= int.MaxValue;
                }
                var remainingInt = (int)remaining;
                if (remainingInt != 0)
                {
                    await stream.ReadAsync(result.AsMemory(index, remainingInt));
                    index += remainingInt;
                    remaining = 0;
                }
                return result;
            };
        }
        /// <summary>
        /// Load image from file path
        /// </summary>
        public static Texture2D LoadImage(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));
            return LoadImage(File.ReadAllBytes(filename));
        }
        /// <summary>
        /// Load image from bytes
        /// </summary>
        public static Texture2D LoadImage(byte[] bytes)
        {
            ArgumentNullException.ThrowIfNull(bytes, nameof(bytes));
            var tex = new Texture2D(1,1);
            if (ImageConversion.LoadImage(tex, bytes))
            {
                return tex;
            }
            throw new FormatException("failed to load image");
        }
        public static Texture2D CreateSingleColorPixel(UnityEngine.Color color)
        {
            var tex = new Texture2D(1,1);
            tex.SetPixel(0,0,color);
            tex.Apply();
            return tex;
        }
        //static byte[]? textureBytes;
        public static Texture2D CreateDefaultTexture()
        {
            return CreateSingleColorPixel(new(84 / 256f, 45 / 256f, 134 / 256f, 1));
            //var resource = textureBytes ?? GetResource(typeof(ModMain).Assembly, "singlePixel.png");
            //var texture = LoadImage(textureBytes = resource!);
            //return texture;
        }
        ///// <summary>
        ///// Applies the color of the bulletin according to the alpha channel,
        ///// <br/>
        ///// as the new bulletin doesn't support transparency.
        ///// </summary>
        //public static void ApplyBulletinAlpha(Texture2D texture)
        //{

        //}
        private static void CheckParams(MethodInfo method, object[] args)
        {
            var parameters = method.GetParameters();
            if (args is null)
            {
                if (parameters.Length != 0)
                {
                    throw new ArgumentException(nameof(args) + " doesn't match event parameter types", nameof(args));
                }
                return;
            }
            if (parameters.Length != args.Length)
            {
                throw new ArgumentException(nameof(args) + " doesn't match event parameter types", nameof(args));
            }
            for (int i = 0; i < parameters.Length; i++)
            {
                var currentType = args[i].GetType();
                var expectedType = parameters[i].ParameterType;
                if (expectedType == currentType || currentType.IsSubclassOf(expectedType))
                {
                    continue;
                }
                throw new ArgumentException(nameof(args) + " doesn't match event parameter types", nameof(args));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Event"></param>
        /// <param name="args">
        /// Throws if the types of the contents don't match
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public static void GenericEventSafeInvoke(this Delegate? Event, string name, params object[] args)
        {
            if (Event is null) return;
            var method = Event.GetType().GetMethod("Invoke")!;
            CheckParams(method, args);
            foreach (Delegate function in Event.GetInvocationList())
            {
                try
                {
                    if (function is not null)
                    {
                        method.Invoke(function, args);
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error($"An exception was thrown while invoking '{name}' event:\n" + ex.ToString());
                }
            }
        }
        public static void GenericEventSafeInvokeCheckless(this Delegate? Event, string name, params object[] args)
        {
            if (Event is null) return;
            var method = Event.GetType().GetMethod("Invoke")!;
            foreach (Delegate function in Event.GetInvocationList())
            {
                try
                {
                    if (function is not null)
                    {
                        method.Invoke(function, args);
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error($"An exception was thrown while invoking '{name}' event:\n"+ex.ToString());
                }
            }
        }
        internal static string ObjectToString(object? obj)
        {
            return obj?.ToString() ?? string.Empty;
        }
        public static class Color
        {
            public static string ToHex(byte r, byte g, byte b, byte a) => $"{r:X}{g:X}{b:X}{a:X}";
            public static string ToHex(byte r, byte g, byte b) => $"{r:X}{g:X}{b:X}";
            public static string ToHex(byte a) => $"{a:X}";
            public static string ToHex(float a) => $"{(byte)(a * byte.MaxValue):X}";
            public static string ToHex(float r, float g, float b, float a) => $"{(byte)(r * byte.MaxValue):X}{(byte)(g * byte.MaxValue):X}{(byte)(b * byte.MaxValue):X}{(byte)(a * byte.MaxValue):X}";
            public static string ToHex(float r, float g, float b) => $"{(byte)(r * byte.MaxValue):X}{(byte)(g * byte.MaxValue):X}{(byte)(b * byte.MaxValue):X}";
            public static string ToHex(UnityEngine.Color color) => $"{(byte)(color.r * byte.MaxValue):X}{(byte)(color.g * byte.MaxValue):X}{(byte)(color.b * byte.MaxValue):X}{(byte)(color.a * byte.MaxValue):X}";
            public static string AddAlpha(string hex)
            {
                switch (hex.Length)
                {
                    case 6:
                    case 7:
                        return hex + "00";
                    case 8:
                    case 9:
                        return hex;
                    default:
                        throw new ArgumentException();
                }
            }
            public static class Named
            {
                public const string Black = "black";
                public const string Blue = "blue";
                public const string Green = "green";
                public const string Orange = "orange";
                public const string Purple = "purple";
                public const string Red = "red";
                public const string White = "white";
                public const string Yellow = "yellow";
            }

            public const string Black = "000000";
            public const string LightGray = "D3D3D3";
            public const string Gray = "A9A9A9";
            public const string DarkGray = "808080";
            public const string White = "FFFFFF";

            public const string LightYellow = "FFFFE0";
            public const string Yellow = "FFFF00";
            public const string DarkYellow = "8B8000";
            public const string Orange = "FFA500";
            public const string DarkOrange = "FF8C00";

            public const string LightRed = "F08080";
            public const string Red = "FF0000";
            public const string DarkRed = "8B0000";

            public const string Pink = "FFC0CB";
            public const string Magenta = "FF00FF";
            public const string Purple = "A020F0";
            public const string DarkMagenta = "8B008B";

            public const string LightBlue = "ADD8E6";
            public const string Cyan = "00FFFF";
            public const string DarkCyan = "008B8B";
            public const string Blue = "0000FF";
            public const string DarkBlue = "00008B";

            public const string LightGreen = "90EE90";
            public const string Lime = "BFFF00";
            public const string Green = "00FF00";
            public const string DarkGreen = "008000";

            public const string LightBrown = "C4A484";
            public const string Brown = "964B00";
            public const string DarkBrown = "5C4033";
        }
    }
}
