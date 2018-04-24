// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Host.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.IdentityModel.Tokens;
using IdentityServer4;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Quickstart.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.WeChatCompany;
using log4net;
using log4net.Config;
using log4net.Repository;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Host
{
    public class Startup
    {
        public static ILoggerRepository repository { get; set; }

        private static void InitLog4Net()
        {
            repository = LogManager.CreateRepository("NETCoreRepository");            
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(repository, logCfg);
        }
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            InitLog4Net();
            _config = config;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "域账号";
                iis.AutomaticAuthentication = false;
            });

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseErrorEvents = true;
                })
                .AddInMemoryClients(Clients.Get())
                //.AddInMemoryClients(_config.GetSection("Clients"))
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddDeveloperSigningCredential()
                .AddExtensionGrantValidator<Extensions.ExtensionGrantValidator>()
                .AddExtensionGrantValidator<Extensions.NoSubjectExtensionGrantValidator>()
                .AddJwtBearerClientAuthentication()
                .AddAppAuthRedirectUriValidator()
                .AddTestUsers(TestUsers.Users)
                .AddNanoFabricIDS(Configuration); 

            services.AddExternalIdentityProviders();
            ILoggerFactory loggerFactory = new Log4NetFactory(repository);
            services.AddSingleton<ILoggerFactory>(loggerFactory);
            return services.BuildServiceProvider(validateScopes: true);
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseDeveloperExceptionPage();            
            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }

    public static class ServiceExtensions
    {
        public static IServiceCollection AddExternalIdentityProviders(this IServiceCollection services)
        {
            // configures the OpenIdConnect handlers to persist the state parameter into the server-side IDistributedCache.
            services.AddOidcStateDataFormatterCache("aad", "demoidsrv");

            services.AddAuthentication()
                //.AddGoogle("Google", options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                //    options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
                //    options.ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh";
                //})
                .AddWeChatCompany("WeChatCompany", options =>
                {
                    options.AppId = "ww85a6abac8a4faac6";
                    options.AppKey = "IEGbvmlHoC8m_XxNvYSXaaAEh0QGrbWKACavOElYG_c";
                    options.Agentid = "1000012";

                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                });
                //.AddOpenIdConnect("demoidsrv", "IdentityServer", options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                //    options.Authority = "https://demo.identityserver.io/";
                //    options.ClientId = "implicit";
                //    options.ResponseType = "id_token";
                //    options.SaveTokens = true;
                //    options.CallbackPath = "/signin-idsrv";
                //    options.SignedOutCallbackPath = "/signout-callback-idsrv";
                //    options.RemoteSignOutPath = "/signout-idsrv";

                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        NameClaimType = "name",
                //        RoleClaimType = "role"
                //    };
                //})
                //.AddOpenIdConnect("aad", "Azure AD", options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                //    options.Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705";
                //    options.ClientId = "96e3c53e-01cb-4244-b658-a42164cb67a9";
                //    options.ResponseType = "id_token";
                //    options.CallbackPath = "/signin-aad";
                //    options.SignedOutCallbackPath = "/signout-callback-aad";
                //    options.RemoteSignOutPath = "/signout-aad";
                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        NameClaimType = "name",
                //        RoleClaimType = "role"
                //    };
                //})
                //.AddOpenIdConnect("adfs", "ADFS", options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                //    options.Authority = "https://adfs.leastprivilege.vm/adfs";
                //    options.ClientId = "c0ea8d99-f1e7-43b0-a100-7dee3f2e5c3c";
                //    options.ResponseType = "id_token";

                //    options.CallbackPath = "/signin-adfs";
                //    options.SignedOutCallbackPath = "/signout-callback-adfs";
                //    options.RemoteSignOutPath = "/signout-adfs";
                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        NameClaimType = "name",
                //        RoleClaimType = "role"
                //    };
                //})
                //.AddOpenIdConnect("qq", "QQ", options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                //    options.Authority = "https://open.work.weixin.qq.com/wwopen/sso/qrConnect?appid=ww85a6abac8a4faac6&agentid=1000012&redirect_uri=http%3A%2F%2Filogin.5173.com&state=123sdfsdf";
                //    options.ClientId = "c0ea8d99-f1e7-43b0-a100-7dee3f2e5c34";
                //    options.ResponseType = "id_token";

                //    options.CallbackPath = "/signin-adfs";
                //    options.SignedOutCallbackPath = "/signout-callback-adfs";
                //    options.RemoteSignOutPath = "/signout-adfs";
                //    options.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        NameClaimType = "name",
                //        RoleClaimType = "role"
                //    };
                    
                //});            

            return services;
        }
    }
}
