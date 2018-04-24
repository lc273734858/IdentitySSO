# Microsoft.AspNetCore.Authentication Extensions
QQ and Webchat extensions for Microsoft.AspNetCore.Authentication

# Get Started

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
 


     

# Microsoft.AspNetCore.Authentication ��չ
QQ �� ΢�� Microsoft.AspNetCore.Authentication ��չ

# ʹ�÷���

- QQ   
~~~ csharp
 // startup.cs 
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // ���� 
    app.UseQQAuthentication(new Microsoft.AspNetCore.Authentication.QQ.QQAuthenticationOptions()
            {
                ClientId = "[you client id]",
                ClientSecret ="[you client Secret]",
            });

    // .... others code ...
}
~~~   

��ȡ��½�ɹ������Ϣ . eg:  AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // ��ȡ��¼����Ϣ (using Microsoft.AspNetCore.Authentication.QQ;)
    var loginInfo = await HttpContext.Authentication.GetExternalQQLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
~~~

- ΢��
~~~ csharp
 // startup.cs 
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // ���� 
    app.UseWeixinAuthentication(new Microsoft.AspNetCore.Authentication.Weixin.WeixinAuthenticationOptions()
            {
                ClientId = "[you client id]",
                ClientSecret ="[you client Secret]",
            });

    // .... others code ...
}
~~~   

��ȡ��½�ɹ������Ϣ . eg:  AccountController
~~~  csharp
// GET: /Account/ExternalLoginCallback
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{ 
    // .... others code ...
    // .....
  
    // ��ȡ��¼����Ϣ (using Microsoft.AspNetCore.Authentication.Weixin;)
    var loginInfo = await HttpContext.Authentication.GetExternalWeixinLoginInfoAsync();
    
    // todo ...
    // .... others code ...
}
~~~