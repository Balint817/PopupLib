using Il2CppAssets.Scripts.UI.Tips;
using LocalizeLib;
using PopupLib.Records;
using PopupLib.UI.Windows.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupLib.UI.Windows
{
    /// <summary>
    /// Displays a simple message window.
    /// </summary>
    public class MessageWindow : BaseMessageWindow
    {
        public override bool IsLoaded => PopupUtils.CurrentScene == SceneDefinitions.MainMenu;
        protected override bool IsShowReadyPrivate => IsLoaded;

        internal static BaseMessageBoxWrapper wrapper = null!;
        protected internal override BaseMessageBoxWrapper wrapperInstance => wrapper;

        /// <param name="message">
        /// The message to be displayed to the user.
        /// </param>
        public MessageWindow(LocalString message)
        {
            Text = message;
        }
        /// <param name="message">
        /// The message to be displayed to the user.
        /// </param>
        /// <param name="title">
        /// The title of the window
        /// </param>
        public MessageWindow(LocalString message, LocalString title)
        {
            Title = title;
            Text = message;
        }
    }
}
