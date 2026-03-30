using MelonLoader;
using System;

namespace PopupLib
{
    static class Debug
    {
        internal static bool IsDebug
        {
            get
            {
                return Entry.Value;
            }
        }

        internal static MelonPreferences_Entry<bool> Entry = null!;

        internal static void DevMsg(object txt)
        {
            if (!IsDebug)
            {
                return;
            }
            Msg(txt);
        }
        internal static void DevMsg(ConsoleColor color, object txt)
        {
            if (!IsDebug)
            {
                return;
            }
            Msg(color, txt);
        }
        internal static void Msg(ConsoleColor color, object txt)
        {
            MelonLogger.Msg(color, Utils.ObjectToString(txt));
        }
        internal static void Msg(object txt)
        {
            MelonLogger.Msg(Utils.ObjectToString(txt));
        }
        internal static void Error(object txt)
        {
            Msg(ConsoleColor.Red, Utils.ObjectToString(txt));
        }
        internal static void DevError(object txt)
        {
            if (!IsDebug)
            {
                return;
            }
            Error(txt);
        }
    }
}