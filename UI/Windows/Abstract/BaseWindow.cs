using Il2CppAssets.Scripts.UI.Tips;
using PopupLib.Records;

namespace PopupLib.UI.Windows.Abstract
{
    /// <summary>
    /// The base class for all windows
    /// </summary>
    public abstract class BaseWindow
    {
        public bool AutoReset { get; set; }
        public abstract bool IsLoaded { get; }
        protected abstract bool IsShowReadyPrivate { get; }
        public bool IsShowReady => IsLoaded && IsShowReadyPrivate;
        public delegate void OnInternalShowHandler(BaseWindow window);
        public delegate void OnEarlyInternalShowHandler(BaseWindow window);
        public delegate void OnInternalExceptionHandler(BaseWindow window);
        public delegate void OnCompletionHandler(BaseWindow window);
        /// <summary>
        /// Gets called if an exception occurs in the internal window manager
        /// </summary>
        public event OnInternalExceptionHandler? OnInternalException;

        protected internal void InvokeOnInternalException()
        {
            OnInternalException?.GenericEventSafeInvokeCheckless(nameof(OnInternalException), this);
        }

        /// <summary>
        /// Gets called when the window actually appears
        /// </summary>
        public event OnInternalShowHandler? OnInternalShow;
        /// <summary>
        /// Gets called when the window first gets the chance to appear
        /// </summary>
        public event OnEarlyInternalShowHandler? OnEarlyInternalShow;
        /// <summary>
        /// Gets called when the window closes
        /// </summary>
        public event OnCompletionHandler? OnCompletion;

        /// <summary>
        /// Whether the window has been queued (after Show is called)
        /// </summary>
        public bool IsQueued { get; internal set; } = false;

        /// <summary>
        /// Whether the window has been activated (currently displayed)
        /// </summary>
        public bool Activated { get; internal set; } = false;

        /// <summary>
        /// Whether the window has been closed
        /// </summary>
        public bool Completed { get; internal set; } = false;
        protected internal abstract BaseMessageBoxWrapper wrapperInstance { get; }
        protected internal AbstractMessageBox MessageBox => wrapperInstance?.MessageBox!;
        protected virtual void InitMessageBox()
        {

            wrapperInstance.OnClose = OnClose;
            wrapperInstance.OnShow = OnShow;
        }

        /// <summary>
        /// Resets the window.
        /// <para>Succeeds if <c>Completed</c> is <c>true</c>, returns <c>false</c> otherwise.</para>
        /// </summary>
        public bool Reset()
        {
            if (!Completed)
            {
                return false;
            }
            OnEarlyReset();
            return InternalReset(false);
        }

        protected internal bool InternalReset(bool suppressUpdate)
        {
            if (!suppressUpdate)
            {
                WindowManager.Update();
            }
            OnReset();
            Completed = false;
            Activated = false;
            IsQueued = false;
            return true;
        }
        protected virtual void OnEarlyReset()
        {

        }
        protected virtual void OnReset()
        {

        }

        /// <summary>
        /// Queues the window to be displayed.
        /// Sets "IsQueued" if successful.
        /// <para>Returns <c>true</c> if successful.</para>
        /// </summary>
        public virtual bool Show()
        {
            if (Activated || !IsLoaded || !WindowManager.Add(this))
            {
                Debug.Error($"window type <{GetType()}> returned false in show");
                return false;
            }
            Debug.DevMsg($"queued type <{GetType()}>");
            return IsQueued = true;
        }

        internal void Show_Managed()
        {
            OnEarlyInternalShow?.GenericEventSafeInvokeCheckless(nameof(OnEarlyInternalShow), this);
            HandleManagedShowEarly();
            InitMessageBox();
            MessageBox.Show();
            Activated = true;
            OnInternalShow?.GenericEventSafeInvokeCheckless(nameof(OnInternalShow), this);
            HandleManagedShow();
        }
        /// <summary>
        /// Make sure that this doesn't throw or else shit will break
        /// </summary>
        protected virtual void HandleManagedShowEarly()
        {

        }

        protected virtual void HandleManagedShow()
        {

        }

        protected virtual void HandleClose()
        {

        }
        protected void OnClose()
        {
            //MessageBox.Close();
            wrapperInstance.ResetFunctions();
            try
            {
                MessageBox?.Close();
            }
            finally
            {
                Completed = true;
                HandleClose();
                OnCompletion?.GenericEventSafeInvokeCheckless(nameof(OnCompletion), this);
            }

        }
        public void ForceClose()
        {
            Debug.DevMsg($"Force-closing <{GetType().FullName}>");
            if (Completed)
            {
                return;
            }

            if (Activated)
            {
                OnClose();
            }
            else if (IsQueued)
            {
                Activated = true;
                OnInternalShow?.GenericEventSafeInvokeCheckless(nameof(OnInternalShow), this);
                OnClose();
            }
            WindowManager.Remove(this);
            Completed = true;
            WindowManager.Update();
            if (AutoReset)
            {
                InternalReset(true);
            }
        }
        protected virtual void OnShow()
        {

        }
    }
}
