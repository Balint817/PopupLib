using Il2CppDiscord;
using Il2CppSystem.Runtime.Remoting.Messaging;
using LocalizeLib;
using PopupLib.Records;
using PopupLib.UI.Windows.Abstract;
using PopupLib.UI.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupLib.UI.Windows
{
    public class InputWindow: BaseTitleWindow, IResultWindow<string?>
    {
        public override bool IsLoaded => PopupUtils.CurrentScene == SceneDefinitions.MainMenu;
        protected override bool IsShowReadyPrivate => IsLoaded;
        /// <param name="title">
        /// The message to be displayed the user with <code>PopupUtils.ShowInfo</code>
        /// </param>
        public InputWindow(LocalString? title = null)
        {
            Title = title;
        }

        internal static BaseMessageBoxWrapper wrapper = null!;
        protected internal override BaseMessageBoxWrapper wrapperInstance => wrapper;

        private string? _result;
        /// <summary>
        /// The result of the input.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when Completed is false.
        /// </exception>
        public string? Result
        {
            get
            {
                if (!Completed)
                {
                    throw new InvalidOperationException("attempted to get result before completion");
                };
                return _result;
            }
            private set
            {
                _result = value;
            }
        }
        protected override void HandleClose()
        {
            //PopupUtils.GetTerminal().m_InputField.placeholder.GetComponent<Text>().text = defaultPlaceholder;
            PopupUtils.GetTerminal().m_InputField.text = "";
        }

        private void OnNoClicked()
        {
            _result = null;
            this.OnClose();
        }
        private void OnYesClicked()
        {
            _result = PopupUtils.GetTerminal().m_InputField.text ?? "";
            this.OnClose();
        }

        protected override void InitMessageBox()
        {
            base.InitMessageBox();
            wrapper.OnNoClicked = OnNoClicked;
            wrapper.OnYesClicked = OnYesClicked;
        }
        protected override void OnShow()
        {
            var msg = Title?.Current();
            if (msg != null)
            {
                PopupUtils.ShowInfo(msg);
            }
        }
        protected override void OnReset()
        {
            Result = default;
        }
    }
}
