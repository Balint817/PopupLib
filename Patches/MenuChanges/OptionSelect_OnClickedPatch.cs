using HarmonyLib;
using Il2Cpp;
using LocalizeLib;
using PopupLib.UI;

namespace PopupLib.Patches.MenuChanges
{
    [HarmonyPatch(typeof(OptionSelect), nameof(OptionSelect.OnClicked))]
    internal class OptionSelect_OnClickedPatch
    {
        private enum OptionIndexDefines
        {
            Tutorial = 0,
            Language = 1,
            Audio = 2,
            Display = 3,
            Controls = 4,
            Offset = 5,
            Anchor = 6, // streamer mode
            Credit = 7,
            RateUs = 8,
            FollowUs = 9,
            QA = 10,
            Feedback = 11,
            Terminal = 12,
            Bulletin = 13,
            Account = 14,
            PeroShop = 15
        }
        private static bool Prefix(int btnIndex)
        {
            switch ((OptionIndexDefines)btnIndex)
            {
                case OptionIndexDefines.Audio:
                    PopupUtils.ActiveMenu = MenuType.Settings_Audio;
                    break;
                case OptionIndexDefines.Display:
                    PopupUtils.ActiveMenu = MenuType.Settings_Display;
                    break;
                case OptionIndexDefines.Controls:
                    PopupUtils.ActiveMenu = MenuType.Settings_Controls;
                    break;
                case OptionIndexDefines.Offset:
                    PopupUtils.ActiveMenu = MenuType.Settings_Offset;
                    break;
                case OptionIndexDefines.Anchor:
                    PopupUtils.ActiveMenu = MenuType.Settings_Streamer;
                    break;
                case OptionIndexDefines.Credit:
                    PopupUtils.ActiveMenu = MenuType.Settings_Credits;
                    break;
                case OptionIndexDefines.Bulletin:
                    return BulletinFix();
                case OptionIndexDefines.PeroShop:
                    PopupUtils.ActiveMenu = MenuType.Settings_GoodsStore;
                    break;
                case OptionIndexDefines.QA:
                    PopupUtils.ActiveMenu = MenuType.Settings_QA;
                    break;
                case OptionIndexDefines.Tutorial:
                case OptionIndexDefines.Language:
                case OptionIndexDefines.RateUs:
                case OptionIndexDefines.FollowUs:
                case OptionIndexDefines.Feedback:
                case OptionIndexDefines.Terminal:
                case OptionIndexDefines.Account:
                    break;
                default:
                    break;
            }
            return true;
        }

        private static readonly LocalString NoBulletinMsg = new LocalString()
        {
            English = "The bulletin hasn't had a chance to load!",
            ChineseSimplified = null!,
            ChineseTraditional = null!,
            Japanese = null!,
            Korean = null!,
        };
        private static bool BulletinFix()
        {
            //PopupUtils.ShowInfo(NoBulletinMsg);
            //if (BulletinManagerFix.StoreOriginal is not null)
            //{
            //    var bulletin = ForumWindow.wrapper.MessageBox.Cast<PnlStageBulletinController>();
            //    bulletin.m_BulletinDataModels = BulletinManagerFix.StoreOriginal;
            //    bulletin.m_BulletinView.languageChangDirty = true;
            //    bulletin.RefreshUI();
            //}
            return true;
            //BulletinRefreshPatch.IsDefaultBulletin = true;
            //if (BulletinRefreshPatch.storeOriginal != null)
            //{
            //    BulletinManager.instance.bulletins = BulletinRefreshPatch.storeOriginal;
            //}
        }
    }
}
