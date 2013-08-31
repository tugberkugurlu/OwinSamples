using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth.Server.Providers.OAuth
{
    public class AuthorizationServerProvider : IOAuthAuthorizationServerProvider
    {
        public Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            throw new NotImplementedException();
        }

        public Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            throw new NotImplementedException();
        }

        public Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            throw new NotImplementedException();
        }

        public Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                // Validate the credentials here
                bool isValid = true;

                if (isValid)
                {
                    context.Validated();
                }
            }

            return Task.FromResult(0);
        }

        public Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            throw new NotImplementedException();
        }

        public Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            throw new NotImplementedException();
        }
    }
}
