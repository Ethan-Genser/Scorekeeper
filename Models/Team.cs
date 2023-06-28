using Microsoft.AspNetCore.Routing.Constraints;
using Scorekeeper.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Scorekeeper.Models
{
    public class Team
    {
        private static RandomColorGenerator _randomColorGenerator = new RandomColorGenerator();

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "New Team";
        public string Color { get; set; } =_randomColorGenerator.GetRandomColor();
        public float Score { get; set; } = 0;

        [Required]
        public Scoreboard Scoreboard { get; set; } = default!;
    }
}