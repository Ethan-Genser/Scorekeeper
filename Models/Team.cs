using Microsoft.AspNetCore.Routing.Constraints;
using Scorekeeper.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Scorekeeper.Models
{
    public class Team
    {
        public enum ColorName
        {
            Red = 1,
            Orange = 2,
            Yellow = 3,
            Green = 4, 
            Blue = 5,
            Purple = 6,
            Pink = 7,
            White = 8,
            Black = 9,
        }
        public enum ColorMode
        {
            Light,
            Dark,
        }

        public struct TeamColorInfo
        {
            public ColorName Name { get; set; }
            public ColorMode Mode { get; set; }
            public TeamColorInfo(ColorName name, ColorMode mode)
            {
                Name = name;
                Mode = mode;
            }
        }

        public static Dictionary<TeamColorInfo, string> TeamColorLookup = new Dictionary<TeamColorInfo, string>
        {
            { new TeamColorInfo(ColorName.Red, ColorMode.Light), "#F28B82" },
            { new TeamColorInfo(ColorName.Red, ColorMode.Dark), "#5C2B29" },
            { new TeamColorInfo(ColorName.Orange, ColorMode.Light), "#F7C085" },
            { new TeamColorInfo(ColorName.Orange, ColorMode.Dark), "#614A19" },
            { new TeamColorInfo(ColorName.Yellow, ColorMode.Light), "#FFEE91" },
            { new TeamColorInfo(ColorName.Yellow, ColorMode.Dark), "#635D19" },
            { new TeamColorInfo(ColorName.Green, ColorMode.Light), "#CCFF90" },
            { new TeamColorInfo(ColorName.Green, ColorMode.Dark), "#345920" },
            { new TeamColorInfo(ColorName.Blue, ColorMode.Light), "#AECBFA" },
            { new TeamColorInfo(ColorName.Blue, ColorMode.Dark), "#1E3A5F" },
            { new TeamColorInfo(ColorName.Purple, ColorMode.Light), "#C8BFE7" },
            { new TeamColorInfo(ColorName.Purple, ColorMode.Dark), "#42275E" },
            { new TeamColorInfo(ColorName.Pink, ColorMode.Light), "#FDCFE8" },
            { new TeamColorInfo(ColorName.Pink, ColorMode.Dark), "#5B2245" },
            { new TeamColorInfo(ColorName.Black, ColorMode.Light), "#202124" },
            { new TeamColorInfo(ColorName.Black, ColorMode.Dark), "#202124" },
            { new TeamColorInfo(ColorName.White, ColorMode.Light), "#E0E0E0" },
            { new TeamColorInfo(ColorName.White, ColorMode.Dark), "#E0E0E0" }
        };

        private static Random _random = new Random();

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "New Team";
        public int Color { get; set; } = (int)ColorName.White;
        public float Score { get; set; } = 0;

        [Required]
        public Scoreboard Scoreboard { get; set; } = default!;
    }
}