using Microsoft.EntityFrameworkCore;
using Scorekeeper.Data;
using Scorekeeper.Models;

namespace Scorekeeper.Services
{
    public class TeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE
        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        // READ
        public List<Team> GetAllTeams()
        {
            return _context.Teams
                .Include(sb => sb.Scoreboard)
                .AsSplitQuery()
                .ToList();
        }
        public Team? GetTeam(string id)
        {
            return _context.Teams
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        // UPDATE
        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }

        // DELETE
        public void DeleteTeam(Team team)
        {
            _context.Teams.Remove(team);
            _context.SaveChanges();
        }
        public void DeleteTeam(string id)
        {
            Team? team = GetTeam(id);

            if (team != null)
            {
                DeleteTeam(team);
            }
        }
    }
}
