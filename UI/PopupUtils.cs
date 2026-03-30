using MelonLoader;
using LocalizeLib;
using Il2CppAssets.Scripts.UI.Controls;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppAssets.Scripts.UI.Tips;
using System;

namespace PopupLib.UI
{
    public static class SceneDefinitions
    {
        public const string MainMenu = "UISystem_PC";
        public const string InGame = "GameMain";
        public const string Loading = "Loading";
        public const string WelcomeScreen = "Welcome";
        public const string GameNotYetLoaded = "Driver";
    }
    public static class Events
    {
        public class MenuEventArgs : EventArgs
        {
            public MenuType From;
            public MenuType To;
            internal MenuEventArgs(MenuType from, MenuType to)
            {
                From = from;
                To = to;
            }
        }
        public class SceneEventArgs : EventArgs
        {
            public string From;
            public string To;
            internal SceneEventArgs(string from, string to)
            {
                From = from;
                To = to;
            }
        }
        public static event Action<SceneEventArgs>? SceneLoaded;
        /// <summary>
        /// Event to handle menu updates.
        /// <para>Does not get invoked when the <c>ActiveMenu</c> is set to what it's already set to.</para>
        /// <para>(such as going from Driver scene to Welcome, which switches from Other to Other)</para>
        /// </summary>
        public static event Action<MenuEventArgs>? MenuChanged;
        /// <summary>
        /// Event that fires when the search window is closed or opened.
        /// </summary>
        public static event Action<bool>? SearchActiveChanged;
        /// <summary>
        /// Event that fires when the pause menu is closed or opened.
        /// <para>(Does not wait for the countdown, fires immediately)</para>
        /// </summary>
        public static event Action<bool>? PauseActiveChanged;
        internal static void InvokeOnMenuChanged(MenuEventArgs e)
        {
            MenuChanged?.GenericEventSafeInvokeCheckless(nameof(MenuChanged),e);
        }
        internal static void InvokeOnSearchActiveChanged()
        {
            SearchActiveChanged?.GenericEventSafeInvokeCheckless(nameof(SearchActiveChanged), PopupUtils.IsSearchOpen);
        }
        internal static void InvokeOnPauseActiveChanged()
        {
            PauseActiveChanged?.GenericEventSafeInvokeCheckless(nameof(PauseActiveChanged), PopupUtils.IsGamePaused);
        }
        internal static void InvokeOnSceneLoaded(SceneEventArgs e)
        {
            SceneLoaded?.GenericEventSafeInvokeCheckless(nameof(SceneLoaded), e);
        }

    }


    public static class PopupUtils
    {
        static string? _currentScene;
        public static string? CurrentScene
        {
            get
            {
                return _currentScene;
            }
            internal set
            {
                if (_currentScene != value)
                {
                    if (_currentScene is not null)
                    {
                        Events.InvokeOnSceneLoaded(new(_currentScene, value!));
                    }
                    _currentScene = value;
                }
            }
        }
        internal static PnlTerminal GetTerminal()
        {
            return PnlTipsManager.instance.GetMessageBox<PnlTerminal>("PnlTerminal");
        }
        /// <summary>
        /// Displays a message in a long strip that disappears shortly.
        /// </summary>
        /// <param name="obj">
        /// The <c>object.ToString()</c> to be displayed to the user.
        /// If <c>object</c> is <c>null</c>, an empty string is substituted.
        /// </param>
        public static void ShowInfo(object? obj)
        {
            try
            {
                ShowText.ShowInfo(Utils.ObjectToString(obj));
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        public static void ShowInfoAndLog(object? obj)
        {
            var text = Utils.ObjectToString(obj);
            MelonLogger.Msg(text);
            try
            {
                ShowText.ShowInfo(text);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        /// <param name="textColor">
        /// The color of the message (affects console only).
        /// </param>
        public static void ShowInfoAndLog(object? obj, ConsoleColor textColor)
        {
            var text = Utils.ObjectToString(obj);
            MelonLogger.Msg(textColor, text);
            try
            {
                ShowText.ShowInfo(text);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        /// <param name="forceEnglishLog">
        /// Force log message to be in English.
        /// </param>
        public static void ShowInfoAndLog(LocalString text, bool forceEnglishLog)
        {
            MelonLogger.Msg(forceEnglishLog ? text?.English : text?.Current());
            try
            {
                ShowText.ShowInfo(text?.Current());
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        public static void ShowInfoAndLog(LocalString text)
        {
            ShowInfoAndLog(text, true);
        }

        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        public static void ShowInfo(LocalString text)
        {
            ShowInfo(text?.Current());
        }

        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        /// <param name="textColor">
        /// The color of the message (affects console only).
        /// </param>
        /// <param name="forceEnglishLog">
        /// Force log message to be in English.
        /// </param>
        public static void ShowInfoAndLog(LocalString text, ConsoleColor textColor, bool forceEnglishLog)
        {
            MelonLogger.Msg(textColor, forceEnglishLog ? text?.English : text?.Current());
            try
            {
                ShowText.ShowInfo(text?.Current());
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Displays a message in a long strip that disappears shortly, and logs it to the console.
        /// </summary>
        /// <param name="text">
        /// The message to be displayed to the user.
        /// </param>
        /// <param name="textColor">
        /// The color of the message (affects console only).
        /// </param>
        /// <param name="forceEnglishLog">
        /// Force log message to be in English.
        /// </param>
        public static void ShowInfoAndLog(LocalString text, bool forceEnglishLog, ConsoleColor textColor)
        {
            ShowInfoAndLog(text, textColor, forceEnglishLog);
        }

        /// <summary>
        /// Returns whether the mod currently has any windows in queue.
        /// </summary>
        public static bool AnyManagedWindow() => WindowManager.Any();

        private static bool _isSearchOpen;

        /// <summary>
        /// Returns whether the search window is currently open
        /// </summary>
        public static bool IsSearchOpen
        {
            get
            {
                return _isSearchOpen;
            }
            internal set
            {
                if (value == _isSearchOpen)
                {
                    return;
                }
                _isSearchOpen = value;
                Events.InvokeOnSearchActiveChanged();
            }
        }

        private static bool _isPaused;

        /// <summary>
        /// Returns whether the game is currently paused
        /// </summary>
        public static bool IsGamePaused
        {
            get
            {
                return _isPaused;
            }
            internal set
            {
                if (value == _isPaused)
                {
                    return;
                }
                _isPaused = value;
                Events.InvokeOnPauseActiveChanged();
            }
        }

        private static MenuType _activeMenu = MenuType.None;

        /// <summary>
        /// The currently active menu (within UISystem_PC)
        /// Does not differentiate between tabs (e.g. settings, character/elfin selection, etc.)
        /// "Other" means in-game or loading (you can check with your OnSceneWasLoaded event or MuseDashMirror)
        /// "None" means no scene has been initialized yet.
        /// </summary>
        public static MenuType ActiveMenu
        {
            get
            {
                return _activeMenu;
            }
            set
            {
                if (_activeMenu == value)
                {
                    return;
                }
                IsSearchOpen = false;
                Events.InvokeOnMenuChanged(new Events.MenuEventArgs(_activeMenu, value));
                _activeMenu = value;
            }
        }
    }
    public enum MenuType
    {
        None = -100,
        Welcome = -1,
        Unknown = 0,
        MainMenu = 1,
        LevelSelect = 2,
        Shop = 3,

        Settings = 100,
        Characters = 101,
        Elfins = 102,
        Trove = 103,
        Achievements = 104,

        Loading = 1000,
        InGame = 1001,
        Victory = 1002,
        FailScreen = 1003,

        Settings_Offset = 10000,
        Settings_Controls = 10001,
        Settings_Display = 10002,
        Settings_Display_FeverBG = 10003,
        Settings_Display_Brightness = 10004,
        Settings_Audio = 10005,
        Settings_Streamer = 10006,
        Settings_Credits = 10007,
        Settings_GoodsStore = 10008,
        Settings_QA = 10009,
    }
}
