using System.Reflection;

namespace PopupLib.Properties
{
    [Obfuscation(Exclude = true, ApplyToMembers = true, Feature = "all")]
    internal static class MelonModInfo
    {
        public const string Name = "PopupLib";

        public const string Description = "A library to display pop-ups, windows, or take input";

        public const string Author = "PBalint817";

        public const string Version = "2.1.1";

        public const string DownloadLink = "";

        //Lower == Greater priority
        public const int Priority = 0;
    }
}
