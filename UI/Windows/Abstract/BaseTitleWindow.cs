using LocalizeLib;

namespace PopupLib.UI.Windows.Abstract
{
    public abstract class BaseTitleWindow : BaseWindow
    {

        /// <summary>
        /// The title of the window
        /// </summary>
        public LocalString? Title;
        protected virtual void SetText()
        {
            MessageBox.MessageBoxTitle = Title?.Current() ?? "";
        }

        protected override void OnShow()
        {
            SetText();
        }
    }
}
