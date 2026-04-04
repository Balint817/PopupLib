using LocalizeLib;
using PopupLib.Records;
using PopupLib.UI.Windows.Abstract;
using PopupLib.UI.Windows.Interfaces;
using System;

namespace PopupLib.UI.Windows
{

    public class PromptWindow : BaseMessageWindow, IResultWindow<bool?>
    {
        public override bool IsLoaded => PopupUtils.CurrentScene == SceneDefinitions.MainMenu || PopupUtils.CurrentScene == SceneDefinitions.InGame;
        protected override bool IsShowReadyPrivate => IsLoaded;

        internal static BaseMessageBoxWrapper wrapper = null!;
        protected internal override BaseMessageBoxWrapper wrapperInstance => wrapper;

        private bool? _result;
        /// <summary>
        /// The result of the prompt.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when Completed is false.
        /// </exception>

        public bool? Result
        {
            get
            {
                if (!Completed)
                {
                    throw new InvalidOperationException("attempted to get result before completion");
                }
                ;
                return _result;
            }
            private set
            {
                _result = value;
            }
        }
        protected override void InitMessageBox()
        {
            base.InitMessageBox();
            wrapper.OnNoClicked = OnNoClicked;
            wrapper.OnYesClicked = OnYesClicked;
        }

        /// <param name="message">
        /// The message to be displayed to the user.
        /// </param>
        public PromptWindow(LocalString message)
        {
            Text = message;
        }
        /// <param name="message">
        /// The message to be displayed to the user.
        /// </param>
        /// <param name="title">
        /// The title of the window
        /// </param>
        public PromptWindow(LocalString message, LocalString title)
        {
            Title = title;
            Text = message;
        }

        internal void OnNoClicked()
        {
            Result = false;
            OnClose();
        }
        internal void OnYesClicked()
        {
            Result = true;
            OnClose();
        }
        protected override void OnReset()
        {
            Result = default;
        }
    }
}
