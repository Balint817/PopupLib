using Il2Cpp;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppAssets.Scripts.UI.Panels.Bulletin;
using Il2CppAssets.Scripts.UI.Tips;
using MelonLoader;
using PopupLib.Records;
using PopupLib.UI;
using PopupLib.UI.Windows;
using System;
using System.IO;
using UnityEngine;
using MenuType = PopupLib.UI.MenuType;

namespace PopupLib
{
    //internal class Asd: Il2CppAssets.Scripts.PeroTools.Nice.Variables.Variable
    //{
    //    public Asd(IntPtr intPtr) : base(intPtr)
    //    {

    //    }
    //}
    internal class SomeClass : AbstractMessageBox
    {
        public SomeClass(IntPtr intPtr) : base(intPtr)
        {

        }
        public SomeClass()
        {

        }

    }
    public class ModMain : MelonMod
    {
        public static bool IsKeybindsLoaded => Utils.IsAssemblyLoaded("KeybindManager");
        /// <summary>
        /// Whether the mod has finished the first load.
        /// The game first loads windows on the main UISystem_PC scene.
        /// </summary>
        public static bool IsLoaded { get; private set; } = false;
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {

            PopupUtils.IsSearchOpen = false;
            PopupUtils.IsGamePaused = false;
            PopupUtils.CurrentScene = sceneName;
            Debug.DevMsg($"Loaded scene \"{sceneName}\"");
            switch (sceneName)
            {
                case SceneDefinitions.MainMenu:
                    {
                        if (!IsLoaded)
                        {

                            if (Debug.IsDebug)
                            {
                                foreach (var kv in PnlTipsManager.instance.m_MessageBoxs)
                                {
                                    Console.Write($"{kv.key}: ");
                                    var value = kv.value;
                                    if (value is null)
                                    {
                                        Console.WriteLine("<null>");
                                        continue;
                                    }

                                    Console.WriteLine($"<{value.GetIl2CppType().Name}>");

                                    try
                                    {
                                        value.ResetMsgBox();
                                        Console.WriteLine($"\t-name: {value.name}");
                                        Console.WriteLine($"\t-title: {value.MessageBoxTitle}");
                                        Console.WriteLine($"\t-button type: {value.m_ButtonType}");
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("\t-<FAILED>");
                                    }
                                }
                            }
                            PopupUtils.ActiveMenu = MenuType.MainMenu;

                            MessageWindow.wrapper = new MessageBoxKeyWrapper("GeneralMessageBox");
                            PromptWindow.wrapper = new MessageBoxKeyWrapper("PnlSongHideCancelAsk");
                            InputWindow.wrapper = new MessageBoxKeyWrapper("PnlTerminal");
                            if (IsKeybindsLoaded)
                            {
                                KeybindWindow.wrapper = new MessageBoxKeyWrapper("PnlSongHideCancelAsk");
                            }
                            //ForumWindow.wrapper = new ManagedMessageBoxWrapper(CreateLegacyBulletin);
                            ForumWindow.wrapper = new MessageBoxKeyWrapper("PnlBulletinNew");
                            ForumWindow.wrapper.MessageBox.Cast<PnlStageBulletinController>().RefreshBulletinInfo();
                            IsLoaded = true;

                            //ForumWindow.wrapper.MessageBox.Show();

                            //BulletinTest.Run();

                            //copy.
                            //Action close = () => copy.Close();
                            //copy.onCancelClicked = close;
                            //copy.onShutClicked = close;
                            //copy.onNoClicked = close;
                            //copy.gameObject.SetActive(true);
                            //copy.onCancelClicked = close;
                            //copy.onShutClicked = close;
                            //copy.onNoClicked = close;


                            //ClassInjector.RegisterTypeInIl2Cpp<Asd>();
                            //ClassInjector.RegisterTypeInIl2Cpp<SomeClass>();
                            //var t = new GameObject("TestMsgBox");
                            //UnityEngine.Object.DontDestroyOnLoad(t);
                            //var msgBox = t.AddComponent<SomeClass>();
                            //msgBox.Show();
                        }
                        break;
                    }
                case SceneDefinitions.Loading:
                    {
                        PopupUtils.ActiveMenu = MenuType.Loading;
                        break;
                    }
                case SceneDefinitions.InGame:
                    {
                        //GameInputField.RegisterType();
                        PopupUtils.ActiveMenu = MenuType.InGame;
                        break;
                    }
                case SceneDefinitions.WelcomeScreen:
                    {
                        PopupUtils.ActiveMenu = MenuType.Welcome;
                        break;
                    }
                default:
                    {
                        PopupUtils.ActiveMenu = MenuType.Unknown;
                        break;
                    }
            }
        }

        private static AbstractMessageBox CreateLegacyBulletin()
        {

            var tipsTransform = GameObject.Find("Tips").transform;
            var gameObject = new GameObject("LegacyPnlBulletin");
            var rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.position = new(-0.02f, 0, 100);
            rectTransform.right = new(1, 0, 0);
            rectTransform.up = new(0, 1, 0);

            var canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.ignoreParentGroups = false;
            canvasGroup.interactable = true;

            var bulletin = gameObject.AddComponent<PnlBulletin>();
            bulletin.txtContent = new();
            bulletin.toggleGroup = new();
            bulletin.m_Tgls = new();

            //bulletin.image = ;

            var bulletinSelect = gameObject.AddComponent<PnlBulletinSelect>();
            //bulletinSelect.noColor = new(0, 0.7529f, 1, 1);
            //bulletinSelect.yesColor = new(0.0353f, 0.9216f, 1, 1);

            gameObject.transform.parent = tipsTransform;
            gameObject.SetActive(false);
            return bulletin;
        }

        public override void OnInitializeMelon()
        {
            var category = MelonPreferences.CreateCategory("PopupLib");
            category.SetFilePath(Path.Combine("UserData", "PopupLib.cfg"));
            Debug.Entry = category.CreateEntry("Debug", false);
            //AutoPushPopPanel_OnPnlHomeActiveChanged_HookPatch.AttachHook();
            Events.MenuChanged += (Events.MenuEventArgs e) =>
            {
                Debug.DevMsg($"MenuChanged: {e.From} => {e.To}");
            };
            Events.PauseActiveChanged += (bool x) =>
            {
                Debug.DevMsg($"PauseActiveChanged: {x}");
            };
            Events.SearchActiveChanged += (bool x) =>
            {
                Debug.DevMsg($"SearchActiveChanged: {x}");
            };
            Events.SceneLoaded += x =>
            {
                Debug.DevMsg($"SceneChanged: {x.From} => {x.To}");
            };
        }
        public override void OnUpdate()
        {
            WindowManager.Update();
        }
    }

    internal class BulletinTest
    {
        internal static void Run()
        {
            MelonLogger.Msg("1");

            var origMsgBox = Il2CppAssets.Scripts.UI.Tips.PnlTipsManager.instance.GetMessageBox("PnlBulletinNew");
            var copy = UnityEngine.GameObject.Instantiate(origMsgBox.gameObject, origMsgBox.transform.parent, true);
            var pnlStageBulletinController = copy.GetComponent<Il2CppAssets.Scripts.UI.Panels.Bulletin.PnlStageBulletinController>();
            UnityEngine.Component.Destroy(pnlStageBulletinController);

            var pnlBulletinSelect = copy.GetComponent<Il2Cpp.PnlBulletinSelect>();
            UnityEngine.Component.Destroy(pnlBulletinSelect);

            var txtTittle = copy.transform.Find("TxtTittle");
            var localization = txtTittle.GetComponent<Il2CppAssets.Scripts.PeroTools.GeneralLocalization.Localization>();
            UnityEngine.Component.Destroy(localization);

            var contentText = copy.transform.Find("ImgBase/ScrollView/Viewport/Content/Text");
            localization = contentText.GetComponent<Il2CppAssets.Scripts.PeroTools.GeneralLocalization.Localization>();
            UnityEngine.Component.Destroy(localization);

        }

        private static void Show(GameObject copy)
        {
            copy.gameObject.SetActive(true);

            var startTime = Time.time;
            var initialPosition = copy.transform.position;
            /// parabola:
            /// f(x) = (ax+b)^2 + c
            /// 
            /// ease formula:
            /// f(x) = (|c|/-c)(ax+b)^2 + c
            /// 
            /// x1 = (-b + sqrt(|c|)) / a
            /// x2 = (-b - sqrt(|c|)) / a
            /// 
            /// so that f(1) = 0:
            /// b = (|a|/-a)*(|a| - sqrt(|c|))
            /// 
            /// if you also want f(0) = 0:
            /// a = 2 * sqrt(|c|)
            /// 
            /// 
            /// f(1) == 0
            /// 
            /// x = (sqrt(b)-a)/c
            ///
            void Update()
            {
                //var formula = (percent) => ;
                //startTime - lastTime;
                //MelonEvents.OnUpdate.Unsubscribe(Update);
            }
            MelonEvents.OnUpdate.Subscribe(Update);
        }
    }
}