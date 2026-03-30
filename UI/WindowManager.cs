using MelonLoader;
using PopupLib.UI.Windows;
using PopupLib.UI.Windows.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupLib.UI
{
    internal static class WindowManager
    {
        private static readonly List<BaseWindow> _windows = new List<BaseWindow>();
        internal static bool Add(BaseWindow window)
        {
            if (_windows.Contains(window))
            {
                return false;
            };
            _windows.Add(window);
            return true;
        }
        internal static bool Remove(BaseWindow window)
        {
            return _windows.Remove(window);
        }

        internal static bool ForumWindow_OnToggle(int idx)
        {
            if (!Any() || _windows[0] is not ForumWindow window)
            {
                return false;
            }
            Debug.DevMsg($"Changed Toggle item: {idx}");
            window.HandleSelection(idx);
            return true;
        }

        internal static bool Any()
        {
            return _windows.Any();
        }

        internal static BaseWindow? FirstOrDefault()
        {
            return _windows.FirstOrDefault();
        }

        static bool waitingForUnmanagedWindow = false;
        static bool waitingForShowReady = false;
        internal static void Update()
        {
            if (!Any())
            {
                return;
            }
            var window = _windows[0];
            if (!window.IsLoaded)
            {
                Debug.DevMsg($"force-removing window <{window.GetType()}> due it being unloaded (did you load a new scene?)");
                waitingForUnmanagedWindow = false;
                waitingForShowReady = false;
                try
                {
                    window.ForceClose();
                }
                catch (NullReferenceException)
                {
                    // ignore
                }
                if (window.AutoReset)
                {
                    try
                    {
                        window.InternalReset(true);
                    }
                    catch (Exception ex)
                    {
                        MelonLogger.Error(ex.ToString());
                    }
                }
                return;
            }
            if (!window.Activated)
            {
                if (!window.IsShowReady)
                {
                    if (!waitingForShowReady)
                    {
                        Debug.DevMsg($"waiting for window to be ready <{window.GetType()}>");
                        waitingForShowReady = true;
                        waitingForUnmanagedWindow = false;
                    }
                    return;
                }
                waitingForShowReady = false;
                if (window.MessageBox?.isActiveAndEnabled is true)
                {
                    if (!waitingForUnmanagedWindow)
                    {
                        Debug.DevMsg($"waiting for unmanaged window to close before showing <{window.GetType()}>");
                        waitingForUnmanagedWindow = true;
                    }
                    return;
                }
                waitingForUnmanagedWindow = false;
                try
                {
                    Debug.DevMsg($"managed show of <{window.GetType()}>");
                    window.Show_Managed();
                }
                catch (Exception ex)
                {
                    MelonLogger.Error(ex.ToString());
                    try
                    {
                        window.wrapperInstance.ResetFunctions();
                    }
                    catch (Exception ex2)
                    {
                        MelonLogger.Msg("Unexpected exception in window manager, some base game functions may be broken now");
                        MelonLogger.Error(ex2.ToString());
                    }
                    _windows.RemoveAt(0);
                    try
                    {
                        window.InvokeOnInternalException();
                    }
                    catch (Exception ex2)
                    {
                        MelonLogger.Error(ex2.ToString());
                    }
                    if (window.AutoReset)
                    {
                        try
                        {
                            window.InternalReset(true);
                        }
                        catch (Exception ex2)
                        {
                            MelonLogger.Error(ex2.ToString());
                        }
                    }
                }
                return;
            }
            if (!window.Completed)
            {
                return;
            }
            Debug.DevMsg($"removed <{window.GetType()}>");
            _windows.RemoveAt(0);
            if (window.AutoReset)
            {
                window.InternalReset(true);
            }
        }
    }
}
