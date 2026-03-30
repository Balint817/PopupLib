using Il2CppAssets.Scripts.GameCore.Managers;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppInterop.Common.Attributes;
using PopupLib.Patches.RefreshBulletinFix;
using PopupLib.Records;
using PopupLib.UI.Components;
using PopupLib.UI.Windows.Abstract;
using PopupLib.UI.Windows.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Bulletin = Il2CppAssets.Scripts.GameCore.Managers.BulletinManager.Bulletin;
using BulletinDataModel = Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinDataModel;
using BulletinController = Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinController;
using static PopupLib.UI.Windows.Interfaces.IListWindow;
using Il2CppAssets.Scripts.UI.Panels.Bulletin;
using BulletinList = Il2CppSystem.Collections.Generic.List<Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinDataModel>;
using BulletinDict = Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppSystem.Collections.Generic.List<Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinDataModel>>;
using Il2Cpp;
using UnityEngine.Events;
using static Il2CppAssets.Scripts.GameCore.Managers.BulletinManager;
using Il2CppAssets.Scripts.PeroTools.Commons;
using MelonLoader;

namespace PopupLib.UI.Windows
{
    /// <summary>
    /// Displays a forum window which can contain seperate tabs of contents. (notifications tab in settings)
    /// </summary>
    public class ForumWindow : BaseWindow, IListWindow
    {
        public override bool Show()
        {
            //if (BulletinManagerFix.StoreOriginal == null)
            //{
            //    MessageBox.Cast<BulletinController>().RefreshBulletinInfo();
            //}
            return base.Show();
        }
        public override bool IsLoaded => PopupUtils.CurrentScene == SceneDefinitions.MainMenu;
        protected override bool IsShowReadyPrivate => true;

        internal static BaseMessageBoxWrapper wrapper = null!;
        protected internal override BaseMessageBoxWrapper wrapperInstance => wrapper;
        protected internal void HandleSelection(int idx)
        {
            OnSelectionChanged?.GenericEventSafeInvokeCheckless(nameof(OnSelectionChanged), this, idx);
        }
        public event SelectionChangedHandler? OnSelectionChanged;
        protected override void InitMessageBox()
        {
            base.InitMessageBox();
            MessageBox.Cast<BulletinController>().m_BulletinView.languageChangDirty = true;
        }
        protected override void HandleManagedShowEarly()
        {
            //if (ForumObjects.Count == 0)
            //{
            //    this.ForceClose();
            //    return;
            //}
            var bulletin = MessageBox.Cast<BulletinController>();

            var bulletDict = bulletin.m_BulletinDataModels = new BulletinDict();
            for (int i = 0; i < ForumObjects.Count; i++)
            {
                var current = ForumObjects[i];
                foreach (var item in current.GetBulletins(i))
                {
                    BulletinList l;
                    if (!bulletDict.ContainsKey(item.Item1))
                    {
                        l = bulletDict[item.Item1] = new BulletinList();
                    }
                    else
                    {
                        l = bulletDict[item.Item1];
                    }
                    l.Add(item.Item2);
                }
            }
            var bulletinSelect = MessageBox.gameObject.GetComponent<PnlBulletinSelect>();
            var scrollView = bulletinSelect.scrollViewObj; // TODO: figure out why this is here lol
            MessageBox.Cast<BulletinController>().m_BulletinView.languageChangDirty = true;
            bulletin.RefreshUI();
            base.HandleManagedShowEarly();
        }

        protected override void HandleClose()
        {
            var bulletin = MessageBox.Cast<BulletinController>();
            var del = OnLinkClick;
            var textContent = bulletin.m_BulletinView.m_CommonProperty.textContent;
            textContent.onLinkClick -= del;
            if (originalWrapMode is { } wrapMode)
            {
                textContent.horizontalOverflow = wrapMode;
            }
        }
        HorizontalWrapMode? originalWrapMode;
        protected override void HandleManagedShow()
        {
            var bulletin = MessageBox.Cast<BulletinController>();
            var linkClickDel = OnLinkClick;
            var textContent = bulletin.m_BulletinView.m_CommonProperty.textContent;
            textContent.onLinkClick += linkClickDel;
            originalWrapMode = textContent.horizontalOverflow;
            textContent.horizontalOverflow = HorizontalWrapMode.Wrap;
        }
        protected void OnLinkClick(string arg)
        {
            OnLinkClicked?.GenericEventSafeInvokeCheckless(nameof(OnLinkClicked), this, arg);
        }

        public event Action<ForumWindow, string>? OnLinkClicked;

        private List<ForumObject> _forumObjects;

        public List<ForumObject> ForumObjects
        {
            get
            {
                return _forumObjects;
            }
            set
            {
                _forumObjects = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
#pragma warning disable CS8618
        public ForumWindow(params ForumObject[] forumObjects)
        {
            this.ForumObjects = forumObjects.ToList();
        }
        public ForumWindow(IEnumerable<ForumObject> forumObjects)
        {
            this.ForumObjects = forumObjects.ToList();
        }
#pragma warning restore CS8618
    }
}
