# Microsoft.AspNetCore.Authentication Extensions
QQ and Webchat extensions for Microsoft.AspNetCore.Authentication

# Get Started

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

 

# Microsoft.AspNetCore.Authentication ��չ
QQ �� ΢�� Microsoft.AspNetCore.Authentication ��չ

# ʹ�÷���

- QQ   
~~~ csharp
 // startup.cs 
public void ConfigureServices(IServiceCollection services)
{
    // .... others code ...
    // ���� 
    services.AddAuthentication() 
        .AddQQAuthentication(options =>
        {
            options.ClientId = "[you app id]";
            options.ClientSecret = "[you app secret]";
        });

    // .... others code ...
}
~~~   

��ȡ��½�ɹ������Ϣ��  eg: AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // ��ȡ��¼����Ϣ (using Microsoft.AspNetCore.Authentication.QQ;)
    var loginInfo = await HttpContext.GetExternalQQLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
 
~~~
- ΢��   
~~~ csharp
 // startup.cs 
public void ConfigureServices(IServiceCollection services)
{
    // .... others code ...
    // ���� 
    services.AddAuthentication() 
        .AddWeixinAuthentication(options =>
        {
            options.ClientId = "[you app id]";
            options.ClientSecret = "[you app secret]";
        });

    // .... others code ...
}
~~~   

��ȡ��½�ɹ������Ϣ�� eg: AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // ��ȡ��¼����Ϣ (using Microsoft.AspNetCore.Authentication.Weixin;)
    var loginInfo = await HttpContext.GetExternalWeixinLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
 
~~~