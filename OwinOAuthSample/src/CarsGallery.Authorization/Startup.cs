using CarsGallery.Authorization;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace CarsGallery.Authorization
{
    public class Startup
    {
        private readonly ConcurrentDictionary<string, string> _authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        public void Configuration(IAppBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Get<TextWriter>("host.TraceOutput").WriteLine("{0} {1}{2}", context.Request.Method, context.Request.PathBase, context.Request.Path);
                await next();
                context.Get<TextWriter>("host.TraceOutput").WriteLine("{0} {1}{2}", context.Response.StatusCode, context.Request.PathBase, context.Request.Path);
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath = "/Authorize",
                TokenEndpointPath = "/Token",
                AuthorizeEndpointDisplaysError = true,
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnLookupClient = LookupClient,
                    OnValidateTokenRequest = ValidateTokenRequest,
                    OnValidateAuthorizeRequest = ValidateAuthorizeRequest,
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                },
                AuthenticationCodeProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode,
                },
                RefreshTokenProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateRefreshToken,
                    OnReceive = ReceiveRefreshToken,
                }
            });

            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultHttpRoute", "api/{controller}");
            app.UseWebApi(config);
        }

        private async Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            var output = context.Request.Get<TextWriter>("host.TraceOutput");
            output.WriteLine("Authorize Request {0} {1} {2}",
                context.ClientContext.ClientId,
                context.AuthorizeRequest.ResponseType,
                context.AuthorizeRequest.RedirectUri);
        }

        private async Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            var output = context.Request.Get<TextWriter>("host.TraceOutput");
            output.WriteLine("Token Request {0} {1}",
                context.ClientContext.ClientId,
                context.TokenRequest.GrantType);
        }

        private Task LookupClient(OAuthLookupClientContext context)
        {
            // this's where you go to db and find your client based on the id

            if (context.ClientId == "123456")
            {
                context.ClientFound(
                    clientSecret: "abcdef",
                    redirectUri: "http://localhost:18002/Katana.Sandbox.WebClient/ClientApp.aspx");
            }
            else if (context.ClientId == "7890ab")
            {
                context.ClientFound(
                    clientSecret: "7890ab",
                    redirectUri: "http://localhost:18002/Katana.Sandbox.WebClient/ClientPageSignIn.html");
            }
            return Task.FromResult(0);
        }

        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // this's the place where u go to db and authanticate the client based on password and username

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(context.UserName, "Bearer"), context.Scope.Split(' ').Select(x => new Claim("urn:oauth:scope", x)));
            context.Validated(identity);

            return Task.FromResult(0);
        }

        private void CreateAuthenticationCode(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n"));
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }

        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authenticationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        private void CreateRefreshToken(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
        }

        private void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}
