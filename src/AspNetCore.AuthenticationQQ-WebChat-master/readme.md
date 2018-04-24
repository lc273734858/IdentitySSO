# Microsoft.AspNetCore.Authentication Extensions
QQ and Webchat extensions for Microsoft.AspNetCore.Authentication

# Get Started
## dotnet core 1.1 

- QQ   
~~~ csharp
 // startup.cs 
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // config 
    app.UseQQAuthentication(new Microsoft.AspNetCore.Authentication.QQ.QQAuthenticationOptions()
            {
                ClientId = "[you client id]",
                ClientSecret ="[you client Secret]",
            });

    // .... others code ...
}
~~~   

Then get external login information when login success . eg:  AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // get information from AuthenticationManager (using Microsoft.AspNetCore.Authentication.QQ;)
    var loginInfo = await HttpContext.Authentication.GetExternalQQLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
~~~

- Webchat
~~~ csharp
 // startup.cs 
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // config 
    app.UseWeixinAuthentication(new Microsoft.AspNetCore.Authentication.Weixin.WeixinAuthenticationOptions()
            {
                ClientId = "[you client id]",
                ClientSecret ="[you client Secret]",
            });

    // .... others code ...
}
~~~   

Then get external login information when login success . eg:  AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // get information from AuthenticationManager (using Microsoft.AspNetCore.Authentication.Weixin;)
    var loginInfo = await HttpContext.Authentication.GetExternalWeixinLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
~~~
 
## dotnet core 2.0

- QQ   
~~~ csharp
 // startup.cs 
public void ConfigureServices(IServiceCollection services)
{
    // .... others code ...
    // config 
    services.AddAuthentication() 
        .AddQQAuthentication(options =>
        {
            options.ClientId = "[you app id]";
            options.ClientSecret = "[you app secret]";
        });

    // .... others code ...
}
~~~   

Then get current external login information when login success . eg: AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // get information from HttpContext (using Microsoft.AspNetCore.Authentication.QQ;)
    var loginInfo = await HttpContext.GetExternalQQLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
 
~~~
- WebChat   
~~~ csharp
 // startup.cs 
public void ConfigureServices(IServiceCollection services)
{
    // .... others code ...
    // config 
    services.AddAuthentication() 
        .AddWeixinAuthentication(options =>
        {
            options.ClientId = "[you app id]";
            options.ClientSecret = "[you app secret]";
        });

    // .... others code ...
}
~~~   

Then get current external login information when login success . eg: AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // get information from HttpContext (using Microsoft.AspNetCore.Authentication.Weixin;)
    var loginInfo = await HttpContext.GetExternalWeixinLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
 
~~~

 


     

# Microsoft.AspNetCore.Authentication 扩展
QQ 和 微信 Microsoft.AspNetCore.Authentication 扩展

# 使用方法
### dotnet core 1.1

- QQ   
~~~ csharp
 // startup.cs 
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // 配置 
    app.UseQQAuthentication(new Microsoft.AspNetCore.Authentication.QQ.QQAuthenticationOptions()
            {
                ClientId = "[you client id]",
                ClientSecret ="[you client Secret]",
            });

    // .... others code ...
}
~~~   

获取登陆成功后的信息。 eg:  AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // 获取登录者信息 (using Microsoft.AspNetCore.Authentication.QQ;)
    var loginInfo = await HttpContext.Authentication.GetExternalQQLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
~~~

- 微信
~~~ csharp
 // startup.cs 
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // 配置 
    app.UseWeixinAuthentication(new Microsoft.AspNetCore.Authentication.Weixin.WeixinAuthenticationOptions()
            {
                ClientId = "[you client id]",
                ClientSecret ="[you client Secret]",
            });

    // .... others code ...
}
~~~   

获取登陆成功后的信息。 eg:  AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // 获取登录者信息 (using Microsoft.AspNetCore.Authentication.Weixin;)
    var loginInfo = await HttpContext.Authentication.GetExternalWeixinLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
~~~

## dotnet core 2.0

- QQ   
~~~ csharp
 // startup.cs 
public void ConfigureServices(IServiceCollection services)
{
    // .... others code ...
    // 配置 
    services.AddAuthentication() 
        .AddQQAuthentication(options =>
        {
            options.ClientId = "[you app id]";
            options.ClientSecret = "[you app secret]";
        });

    // .... others code ...
}
~~~   

获取登陆成功后的信息。  eg: AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // 获取登录者信息 (using Microsoft.AspNetCore.Authentication.QQ;)
    var loginInfo = await HttpContext.GetExternalQQLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
 
~~~
- 微信   
~~~ csharp
 // startup.cs 
public void ConfigureServices(IServiceCollection services)
{
    // .... others code ...
    // 配置 
    services.AddAuthentication() 
        .AddWeixinAuthentication(options =>
        {
            options.ClientId = "[you app id]";
            options.ClientSecret = "[you app secret]";
        });

    // .... others code ...
}
~~~   

获取登陆成功后的信息。 eg: AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // 获取登录者信息 (using Microsoft.AspNetCore.Authentication.Weixin;)
    var loginInfo = await HttpContext.GetExternalWeixinLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
 
~~~
