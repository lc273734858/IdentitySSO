using System.Threading.Tasks;

namespace SSO.IUserRepositories
{
    public interface IUserService
    {
        Task<User> GetAsync(string username);
        Task<User> GetAsync(string username, string password);
        Task AddAsync(User entity, string password);
    }
}