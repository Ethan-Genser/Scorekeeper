﻿using Scorekeeper.Data;
using Scorekeeper.Models;
using Microsoft.EntityFrameworkCore;

namespace Scorekeeper.Services
{
    public class ScoreboardService
    {
        private readonly ApplicationDbContext _context;

        public ScoreboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE
        public void AddScoreboard(Scoreboard scoreboard)
        {
            _context.Scoreboards.Add(scoreboard);
            _context.SaveChanges();
        }

        // READ
        public List<Scoreboard> GetAllScoreboards()
        {
            return _context.Scoreboards
                .Include(sb => sb.Users)
                .ToList();
        }

        public Scoreboard? GetScoreboard(string id)
        {
            return _context.Scoreboards
                .Where(x => x.Id == id)
                .Include(sb => sb.Users)
                .FirstOrDefault();
        }

        // UPDATE
        public void UpdateScoreboard(Scoreboard scoreboard)
        {
            _context.Scoreboards.Update(scoreboard);
            _context.SaveChanges();
        }

        public void UpdateScoreboardAddUser(Scoreboard scoreboard, ApplicationUser user)
        {
        }

        public void UpdateScoreboardRemoveUser(Scoreboard scoreboard, ApplicationUser user)
        {
        }

        // DELETE
        public void DeleteScoreboard(Scoreboard scoreboard)
        {
            _context.Scoreboards.Remove(scoreboard);
            _context.SaveChanges();
        }

        public void DeleteScoreboard(string id)
        {
            Scoreboard? scoreboard = GetScoreboard(id);
            if (scoreboard != null)
            {
                DeleteScoreboard(scoreboard);
            }
        }
    }
}
