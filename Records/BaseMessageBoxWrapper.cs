using Il2CppAssets.Scripts.UI.Tips;
using System;
using System.Runtime.CompilerServices;

namespace PopupLib.Records
{
    public abstract class BaseMessageBoxWrapper
    {
        private readonly Il2CppSystem.Action onCancelClicked_original;
        private readonly Il2CppSystem.Action onHelpClicked_original;
        private readonly Il2CppSystem.Action onClose_original;
        private readonly Il2CppSystem.Action onNoClicked_original;
        private readonly Il2CppSystem.Action onShow_original;
        private readonly Il2CppSystem.Action onShutClicked_original;
        private readonly Il2CppSystem.Action onYesClicked_original;


        public Action OnCancelClicked
        {
            set
            {
                MessageBox.onCancelClicked = value;
            }
        }
        public Action OnHelpClicked
        {
            set
            {
                MessageBox.onHelpClicked = value;
            }
        }
        public Action OnClose
        {
            set
            {
                MessageBox.onClose = value;
            }
        }
        public Action OnCloseAct
        {
            set
            {
                MessageBox.onCloseAct = value;
            }
        }
        public Action OnNoClicked
        {
            set
            {
                MessageBox.onNoClicked = value;
            }
        }
        public Action OnNoAct
        {
            set
            {
                MessageBox.onNoAct = value;
            }
        }
        public Action OnShow
        {
            set
            {
                MessageBox.onShow = value;
            }
        }
        public Action OnShutClicked
        {
            set
            {
                MessageBox.onShutClicked = value;
            }
        }
        public Action OnYesClicked
        {
            set
            {
                MessageBox.onYesClicked = value;
            }
        }
        public Action OnYesAct
        {
            set
            {
                MessageBox.onYesAct = value;
            }
        }

        public abstract AbstractMessageBox MessageBox { get; }
        protected BaseMessageBoxWrapper(params object?[]? args)
        {
            var messageBox = Init(args);
            ArgumentNullException.ThrowIfNull(messageBox, nameof(messageBox));
            onCancelClicked_original = messageBox.onCancelClicked;
            onHelpClicked_original = messageBox.onHelpClicked;
            onClose_original = messageBox.onClose;
            onNoClicked_original = messageBox.onNoClicked;
            onShow_original = messageBox.onShow;
            onShutClicked_original = messageBox.onShutClicked;
            onYesClicked_original = messageBox.onYesClicked;
        }
        protected abstract AbstractMessageBox? Init(object?[]? args);
        internal void ResetFunctions()
        {
            MessageBox.onCancelClicked = onCancelClicked_original;
            MessageBox.onHelpClicked = onHelpClicked_original;
            MessageBox.onClose = onClose_original;
            MessageBox.onNoClicked = onNoClicked_original;
            MessageBox.onShow = onShow_original;
            MessageBox.onShutClicked = onShutClicked_original;
            MessageBox.onYesClicked = onYesClicked_original;
        }
    }
    public class MessageBoxKeyWrapper : BaseMessageBoxWrapper
    {
        private string _msgBoxName;
        public override AbstractMessageBox MessageBox => GetMessageBox(_msgBoxName);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AbstractMessageBox GetMessageBox(string msgBoxName) => PnlTipsManager.instance.GetMessageBox(msgBoxName);
#pragma warning disable CS8618
        public MessageBoxKeyWrapper(string messageBoxName) : base(messageBoxName)
        {
        }
#pragma warning restore CS8618
        protected override AbstractMessageBox? Init(object?[]? args)
        {
            _msgBoxName = (string)args![0]! ?? throw new ArgumentNullException($"{nameof(args)}[{0}]");
            return MessageBox;
        }
    }
    public class ManagedMessageBoxWrapper : BaseMessageBoxWrapper
    {
        public delegate AbstractMessageBox MessageBoxInit();

        private AbstractMessageBox _instance;
        public override AbstractMessageBox MessageBox => _instance;
#pragma warning disable CS8618
        public ManagedMessageBoxWrapper(MessageBoxInit initMethod) : base(initMethod)
        {

        }
#pragma warning restore CS8618
        protected override AbstractMessageBox? Init(object?[]? args)
        {
            if (args![0] is not MessageBoxInit del)
            {
                throw new ArgumentNullException($"{nameof(args)}[{0}]");
            }
            _instance = del();
            //Console.WriteLine(_instance is null);
            //Console.WriteLine(_instance == null);
            //Console.WriteLine(_instance?.Equals(null).ToString() ?? "<null>");
            UnityEngine.Object.DontDestroyOnLoad(_instance);
            return _instance;
        }
    }
}
