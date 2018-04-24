using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Authentication.WeChatCompany
{
    public static class WeChatCompanyExtensions
    {
        public static AuthenticationBuilder AddWeChatCompany(this AuthenticationBuilder builder)
            => builder.AddWeChatCompany(WeChatCompanyDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddWeChatCompany(this AuthenticationBuilder builder, Action<WeChatCompanyOptions> configureOptions)
            => builder.AddWeChatCompany(WeChatCompanyDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddWeChatCompany(this AuthenticationBuilder builder, string authenticationScheme, Action<WeChatCompanyOptions> configureOptions)
            => builder.AddWeChatCompany(authenticationScheme, WeChatCompanyDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddWeChatCompany(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<WeChatCompanyOptions> configureOptions)
            => builder.AddOAuth<WeChatCompanyOptions, WeChatCompanyHandler>(authenticationScheme, displayName, configureOptions);
    }
}
