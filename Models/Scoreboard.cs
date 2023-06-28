using Scorekeeper.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scorekeeper.Models
{
    public class Scoreboard
    {
        private static RandomColorGenerator _randomColorGenerator = new RandomColorGenerator();

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OwnerId { get; set; } = default!;
        public string Name { get; set; } = "New Scoreboard";
        public string Color { get; set; } = _randomColorGenerator.GetRandomColor();
        
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
