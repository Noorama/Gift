using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace HedyoCom_AspNet.Contants
{
    public static class EventConstants
    {
        public const string ManRole = "man";
        public const string WomanRole = "woman";
        public const string BabyRole = "baby";
        public const string SimpleManRole = "simple_man";
        public const string SimpleWomanRole = "simple_woman";
        public const string BirthdayRole = "birthday";

        public static ReadOnlyDictionary<string, string> RoleIcons = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>() {
            { WomanRole, "~/Content/Images/bride_icon.svg" },
            { ManRole, "~/Content/Images/groom_icon.svg" },
            { BabyRole, "~/Content/Images/baby_icon.svg" },
            { SimpleManRole, "~/Content/Images/man_icon.svg" },
            { SimpleWomanRole, "~/Content/Images/woman_icon.svg" },
            { BirthdayRole, "~/Content/Images/birthday.svg" }
        });

        public static string ManIcon { get; } = RoleIcons[ManRole];
        public static string WomanIcon { get; } = RoleIcons[WomanRole];
        public static string BabyIcon { get; } = RoleIcons[BabyRole];
        public static string SimpleManIcon { get; } = RoleIcons[SimpleManRole];
        public static string SimpleWomanIcon { get; } = RoleIcons[SimpleWomanRole];
        public static string BirthdayIcon { get; } = RoleIcons[BirthdayRole];

    }
}