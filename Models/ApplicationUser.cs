using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scorekeeper.Models
{
    public class ApplicationUser : IdentityUser
    {
        public const string DEFAULT_USER_ROLE = "User";

        public ICollection<Scoreboard> Scoreboards { get; set; } = new List<Scoreboard>();
    }
}
