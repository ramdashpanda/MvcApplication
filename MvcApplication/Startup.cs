﻿using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using MvcApplication.Support;
using Owin;

[assembly: OwinStartup(typeof(MvcApplication.Startup))]

namespace MvcApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Auth0 parameters
            string auth0Domain = ConfigurationManager.AppSettings["auth0:Domain"];
            string auth0ClientId = ConfigurationManager.AppSettings["auth0:ClientId"];
            string auth0ClientSecret = ConfigurationManager.AppSettings["auth0:ClientSecret"];
            string auth0RedirectUri = ConfigurationManager.AppSettings["auth0:RedirectUri"];
            string auth0PostLogoutRedirectUri = ConfigurationManager.AppSettings["auth0:PostLogoutRedirectUri"];
            string Audience = ConfigurationManager.AppSettings["auth0:Audience"];
            string grant_type = ConfigurationManager.AppSettings["auth0:grant_type"];
            string content_type = ConfigurationManager.AppSettings["auth0:content_type"];
            var domain = $"https://{ConfigurationManager.AppSettings["auth0:Domain"]}/";
            var apiIdentifier = ConfigurationManager.AppSettings["auth0ApiIdentifier"];
            var keyResolver = new OpenIdConnectSigningKeyResolver(domain);

            app.UseJwtBearerAuthentication(
             new JwtBearerAuthenticationOptions
             {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = apiIdentifier,
                    ValidIssuer = domain,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) => keyResolver.GetSigningKey(kid)
                }
            });
                    WebApiConfig.Configure(app);

            // Set Cookies as default authentication type
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/Account/Login"),
                CookieSameSite = SameSiteMode.Lax,
                // More information on why the CookieManager needs to be set can be found here: 
                // https://github.com/aspnet/AspNetKatana/wiki/System.Web-response-cookie-integration-issues
                CookieManager = new SameSiteCookieManager(new SystemWebCookieManager())
            });
            var myProvider = new APIAUTHORIZATIONSERVERPROVIDER();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            // Configure Auth0 authentication
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "Auth0",
                
                Authority = $"https://{auth0Domain}",

                ClientId = auth0ClientId,

                RedirectUri = auth0RedirectUri,
                PostLogoutRedirectUri = auth0PostLogoutRedirectUri,

                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                },



                // More information on why the CookieManager needs to be set can be found here: 
                // https://docs.microsoft.com/en-us/aspnet/samesite/owin-samesite
                CookieManager = new SameSiteCookieManager(new SystemWebCookieManager()),

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = notification =>
                    {
                        notification.AuthenticationTicket.Identity.AddClaim(new Claim("id_token", notification.ProtocolMessage.IdToken));

                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = notification =>
                    {
                        if (notification.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                        {
                            var logoutUri = $"https://{auth0Domain}/v2/logout?client_id={auth0ClientId}";

                            var postLogoutUri = notification.ProtocolMessage.PostLogoutRedirectUri;
                            if (!string.IsNullOrEmpty(postLogoutUri))
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    // transform to absolute
                                    var request = notification.Request;
                                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                                }
                                logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                            }

                            notification.Response.Redirect(logoutUri);
                            notification.HandleResponse();
                        }
                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}
