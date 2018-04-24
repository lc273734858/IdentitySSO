using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.IUserRepositories
{
    public interface IClientRepository: IClientStore
    {
        Task<IEnumerable<string>> GetAllAllowedCorsOriginsAsync();
    }
}
