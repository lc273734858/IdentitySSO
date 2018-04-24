using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentitySSOClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonFormatters();
			#region use IdentityServer4.AccessTokenValidation

			services
				.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
		        .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, (option) =>
		        {
			        option.Authority = "http://localhost:5000";//identityserver4地址
			        option.RequireHttpsMetadata = false;//使用https
			        option.ApiName = "api1";//api scope
		        });

	        #endregion


            // services.AddAuthentication((options) =>
            // {
            //     options.DefaultScheme =IdentityServer4.AccessTokenValidation. JwtBearerDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // })
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters();
            //     options.RequireHttpsMetadata = false;
            //     options.Audience = "api1";//api范围
            //     options.Authority = "http://localhost:5000";//IdentityServer地址
            // });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
