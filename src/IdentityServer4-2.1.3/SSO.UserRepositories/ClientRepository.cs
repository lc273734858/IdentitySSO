using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using SSO.IUserRepositories;

namespace SSO.UserRepositories
{
    public class ClientRepository : IClientRepository
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllAllowedCorsOriginsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
