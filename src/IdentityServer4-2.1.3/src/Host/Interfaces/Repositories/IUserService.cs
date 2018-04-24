using Host.Models;
using System.Threading.Tasks;

namespace Host.Interfaces.Repositories
{
    public interface IUserService
    {
        Task<User> GetAsync(string username);
        Task<User> GetAsync(string username, string password);
        Task AddAsync(User entity, string password);
    }
}