using Microsoft.EntityFrameworkCore;
using PuckDrop.Application.DbContexts;
using PuckDrop.Application.Interfaces;
using PuckDrop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PuckDrop.Application.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _context;

        public AppRepository(AppDbContext userDbContext)
        {
            _context = userDbContext;
        }

        #region Users
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(i => i.Username == username);
        }
        public async Task<User?> GetUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Username == username);
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .OrderBy(i => i.Username).ToListAsync();
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }
        #endregion

        #region Teams
        public async Task<bool> TeamExistsAsync(int teamId)
        {
            return await _context.Teams.AnyAsync(i => i.TeamId == teamId);
        }
        public async Task<int> GetTeamId(string teamName)
        {
            return await _context.Teams.Where(i => i.Name == teamName).Select(i => i.TeamId).FirstOrDefaultAsync();
        }
        #endregion

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
