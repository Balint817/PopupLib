using System;
using LocalizeLib;

namespace PopupLib.UI.Windows.Abstract
{
    public abstract class BaseMessageWindow : BaseTitleWindow
    {
        /// <summary>
        /// The title of the window
        /// </summary>
        public LocalString? Text;
        protected override void SetText()
        {
            base.SetText();
            MessageBox.MessageBoxText = Text?.Current() ?? "";
        }
    }
}
