using Host.Models;
using Host.Repositories;
using Host.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Host.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder Add5173IDS(this IIdentityServerBuilder builder, IConfigurationRoot config)
        {
            builder.Services.ConfigurePOCO(config.GetSection("IdentityOptions"), () => new IdentityOptions());
            //builder.Services.AddTransient<IUserRepository, UserInMemoryRepository>();
            //builder.Services.AddTransient<IResourceRepository, ResourceInMemoryRepository>();
            //builder.Services.AddTransient<IClientRepository, ClientInMemoryRepository>();
            builder.Services.AddTransient<IClientStore, ClientRepository>();
            builder.Services.AddTransient<IResourceStore, ResourceRepository>();
            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            //services
            //builder.Services.AddTransient<IUserService, UserService>();
            //builder.Services.AddTransient<IPasswordService, PasswordService>();
            //validators
            builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            //builder.AddProfileService<ProfileService>();

            return builder;
        }
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration, Func<TConfig> pocoProvider) where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (pocoProvider == null) throw new ArgumentNullException(nameof(pocoProvider));

            var config = pocoProvider();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
