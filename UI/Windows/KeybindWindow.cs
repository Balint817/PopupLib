using PopupLib.UI.Windows.Abstract;
using System;
using PopupLib.UI.Windows.Interfaces;
using UnityEngine;
using PopupLib.Records;
using LocalizeLib;
using KeybindManager;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using KeybindUtils = KeybindManager.Utils;
using LocalizeUtils = LocalizeLib.Utils;
using HarmonyLib;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using MelonLoader;


namespace PopupLib.UI.Windows
{
    public sealed class KeybindWindow : BaseMessageWindow, IResultWindow<KeybindWindow.Output?>
    {
        static KeybindWindow()
        {
            var type = AccessTools.TypeByName("KeybindManager.ModMain");
            IsKeybindManagerLoaded = type is not null;
        }
        private AnyNoMousePressInfo? _anyPressInfo;
        private ObservableCollection<KeyCode> _keys;
        private string? _keysAsString;
        private Output? _result;
        public Output? Result
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
        private KeybindListener? _listener;

        public static bool IsKeybindManagerLoaded { get; private set; }
        public override bool IsLoaded => IsKeybindManagerLoaded && PopupUtils.CurrentScene == SceneDefinitions.MainMenu || PopupUtils.CurrentScene == SceneDefinitions.InGame;
        protected override bool IsShowReadyPrivate => IsLoaded;
        public class Output
        {
            private protected Output(ReadOnlyCollection<KeyCode>? keys, string? keysAsString, bool cancelled)
            {
                Keys = keys;
                KeysAsString = keysAsString;
                Cancelled = cancelled;
            }
            public ReadOnlyCollection<KeyCode>? Keys { get; }
            public string? KeysAsString { get; }
            public bool Cancelled { get; }
        }
        private class OutputInstance : Output
        {
            public OutputInstance(ReadOnlyCollection<KeyCode>? keys, string? keysAsString, bool cancelled) : base(keys, keysAsString, cancelled) { }
        }
        private static Output CreateOutput(ReadOnlyCollection<KeyCode>? keys, string? keysAsString, bool cancelled)
        {
            return new OutputInstance(keys, keysAsString, cancelled);
        }
        static readonly LocalString UnformattedText = new()
        {
            English = "Press a key to add it:\n{0}\nConfirm to accept this keybind.",
            ChineseSimplified = null!,
            ChineseTraditional = null!,
            Japanese = null!,
            Korean = null!,
        };
        static readonly LocalString KeybindTitle = new()
        {
            English = "Keybind",
            ChineseSimplified = null!,
            ChineseTraditional = null!,
            Japanese = null!,
            Korean = null!,
        };

        public KeybindWindow(): this(null, null)
        {
        }
        public KeybindWindow(LocalString? formattableText): this(formattableText, null)
        {
        }
        public KeybindWindow(LocalString? formattableText, LocalString? title)
        {
            if (!IsKeybindManagerLoaded)
            {
                throw new InvalidOperationException($"'{nameof(KeybindWindow)}' cannot be used if '{nameof(KeybindManager)}' is missing");
            }
            // this is called seperately so that we don't get an exception by initializing a variable of an unknown type,
            // and the 'prettier' exception gets outputted instead.
            Init(formattableText, title);
        }
        [MemberNotNull(nameof(_keys))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Init(LocalString? formattableText, LocalString? title)
        {
            title ??= KeybindTitle.Copy();
            formattableText ??= UnformattedText.Copy();
            if (!LocalizeUtils.IsFormattable(formattableText, 1))
            {
                throw new ArgumentException("text must be formattable to display current keybind", nameof(formattableText));
            }
            _keys = new();
            _keys.CollectionChanged += CollectionChangedEvent;
        }
        private void CollectionChangedEvent(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateText();
        }

        ~KeybindWindow()
        {
            _listener?.Dispose();
        }
        protected override void HandleClose()
        {
            base.HandleClose();
            _listener?.Dispose();
        }
        protected override void HandleManagedShow()
        {
            UpdateText();
            _listener = new("AnyNoMouse");
            _listener.OnTick += OnTick;
            base.HandleManagedShow();
        }

        private static readonly HashSet<KeyCode> excludedKeys = new() { KeyCode.Return };
        private void OnTick(KeybindListener listener)
        {
            if (_anyPressInfo is null)
            {
                if (listener.PressInfos.Count != 1 || listener.PressInfos[0] is not AnyNoMousePressInfo anyNoMousePressInfo)
                {
                    return;
                }
                _anyPressInfo = anyNoMousePressInfo;
            }
            foreach (var pressInfo in _anyPressInfo.PressedKeys)
            {
                if (pressInfo.State == KeyState.Press)
                {
                    var key = pressInfo.Key;
                    // if we couldn't remove it (e.g. it wasn't in the list), add the key instead.
                    if (!excludedKeys.Contains(key) && !_keys.Remove(key))
                    {
                        _keys.Add(key);
                    }
                }
            }
        }
        private void UpdateText()
        {
            if (_keys is null)
                return;
            _keysAsString = KeybindUtils.GetKeybindString(false, _keys);
            if (!LocalizeUtils.TryFormatAll(UnformattedText, out Text, _keysAsString))
            {
                MelonLogger.Error($"<{nameof(KeybindWindow)}> text was changed to an unformattable text during runtime, force-closing...");
                ForceClose();
                return;
            }
            if (MessageBox is not null)
            {
                SetText();
            }
        }
        protected override void InitMessageBox()
        {
            base.InitMessageBox();
            wrapper.OnNoClicked = OnNoClicked;
            wrapper.OnYesClicked = OnYesClicked;
        }
        private void OnNoClicked()
        {
            _result = CreateOutput(null, null, true);
        }
        private void OnYesClicked()
        {
            _result = CreateOutput(_keys?.ToList().AsReadOnly(), _keysAsString, false);
        }
        protected override void OnReset()
        {
            base.OnReset();
            _result = null;
            _anyPressInfo = null;
            _listener?.Dispose();
            _keys?.Clear();
        }
        internal static BaseMessageBoxWrapper wrapper = null!;
        protected internal override BaseMessageBoxWrapper wrapperInstance => wrapper;
    }
}
