using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scorekeeper.Models
{
    public class Scoreboard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OwnerId { get; set; } = default!;
        public string Name { get; set; } = "New Scoreboard";
        public string Color { get; set; } = "#FF0000";
        
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    }
}
