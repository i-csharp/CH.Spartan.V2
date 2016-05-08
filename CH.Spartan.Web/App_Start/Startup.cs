using System;
using System.Collections.Generic;
using System.Configuration;
using Abp.Owin;
using CH.Spartan.Api.Controllers;
using CH.Spartan.Authorization;
using CH.Spartan.Infrastructure;
using CH.Spartan.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace CH.Spartan.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();
            app.MapSignalR();
            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
         
        }
    }
}