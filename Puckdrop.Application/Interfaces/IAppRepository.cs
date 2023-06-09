using PuckDrop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuckDrop.Application.Interfaces
{
    public interface IAppRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(string username);
        Task<bool> UserExistsAsync(string username);
        void AddUser(User user);
        void DeleteUser(User user);
        Task<bool> SaveChangesAsync();
        Task<bool> TeamExistsAsync(int teamId);
        Task<int> GetTeamId(string teamName);
    }
}
