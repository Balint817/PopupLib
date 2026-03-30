//using UnityEngine;
//using Il2CppAssets.Scripts.UI;
//using System;
//using Il2CppAssets.Scripts.PeroTools.Nice.Interface;
//using Il2CppInterop.Runtime.Injection;
//using UnityEngine.UI;

//namespace PopupLib
//{
//    class GameInputField : PeroInputField
//    {
//        bool Finished;
//        public GameInputField(IntPtr ptr): base(ptr)
//        {
//        }

//        public void ShowField()
//        {
//            ActivateInputField();
//            var del = OnValueChangedEvent;
//            this.onValueChanged.AddListener(del);
//            Finished = false;
//        }

//        public static void RegisterType()
//        {
//            ClassInjector.RegisterTypeInIl2Cpp<GameInputField>();
//            GameObject gameObject = new GameObject("PopupLibInputField");
//            UnityEngine.Object.DontDestroyOnLoad(gameObject);
//            var inputField = gameObject.AddComponent<GameInputField>();
//            inputField.lineType = LineType.MultiLineNewline;
//            inputField.Disable();

//        }
//        void OnValueChangedEvent(string content)
//        {
//            if (Finished)
//            {
//                return;
//            }
//            if (content is { } value && value.Contains('\n'))
//            {
//                SendAndDisable(value);
//            }
//        }

//        void SendAndDisable(string s)
//        {
//            try
//            {
//                SendResult(s);
//            }
//            finally
//            {
//                Disable();
//            }
//        }

//        void Disable()
//        {
//            Finished = true;
//            this.onValueChanged.RemoveAllListeners();
//            SetResultCallback = null;
//            this.SetTextWithoutNotify(null);
//            this.enabled = false;
//        }
//        void SendAndDisable()
//        {
//            SendAndDisable(this.text);
//        }

//        void OnDisable()
//        {
//            if (Finished)
//            {
//                return;
//            }
//            SendAndDisable();
//        }
//        void OnDestroy()
//        {
//            this.onValueChanged.RemoveAllListeners();
//            SetResultCallback = null;
//        }

//        void OnSubmit()
//        {
//            if (Finished)
//            {
//                return;
//            }
//            SendAndDisable();
//        }
//        void SendResult(string s)
//        {
//            Finished = true;
//            SetResultCallback?.Invoke(s);
//        }

//        public event Action<string>? SetResultCallback;
//    }
//}