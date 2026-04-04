using HarmonyLib;
using Il2CppAssets.Scripts.UI.Controls;
using LocalizeLib;

namespace PopupLib.Patches.TerminalFix
{
    /// <summary>
    /// Don't display the message that DBConfigUILocalization_GetLocalizationPatch sets to null
    /// </summary>
    [HarmonyPatch(typeof(ShowText), nameof(ShowText.ShowInfo))]
    internal class ShowText_ShowInfoPatch
    {
        public static bool Prefix(string info)
        {
            if (info == null)
            {
                return false;
            }
            if (info == NotFoundMessage.ToString())
            {
                return false;
            }
            return true;
        }
        private static LocalString NotFoundMessage = new()
        {
            English = "Redeem Code doesn't exist（T^T）",
            ChineseSimplified = "兑换码不存在哦（T^T）",
            ChineseTraditional = "兌換碼不存在哦（T^T）",
            Japanese = "コードが存在しません。（T^T）",
            Korean = "존재하지 않는 교환 코드에요.（T^T）"
        };
    }
}
