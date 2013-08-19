using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OAuth.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions 
            {
                AuthorizeEndpointPath = new PathString("/oauth/authorize"),
                TokenEndpointPath = new PathString("/oauth/token"),
                ApplicationCanDisplayErrors = true,
                AccessTokenFormat = new JwtFormat(new X509CertificateSigningCredentialsProvider(AppConstants.Issuer, GetSigningCertificate())),

                Provider = new OAuthAuthorizationServerProvider 
                {
                },

                AuthorizationCodeProvider = new AuthenticationTokenProvider
                {
                },

                RefreshTokenProvider = new AuthenticationTokenProvider
                {
                }
            });
        }

        // Provider methods

        // Helpers

        private static X509Certificate2 GetSigningCertificate()
        {
            X509Store store = new X509Store("My");
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2 signingCert = store.Certificates[0];
            store.Close();

            // We only need the public key of the certificate
            return signingCert;
        }
    }

    public class X509CertificateSigningCredentialsProvider : ISigningCredentialsProvider
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;

        public SigningCredentials SigningCredentials { get { return _signingCredentials; } }
        public string Issuer { get { return _issuer; } }
        public IEnumerable<SecurityToken> SecurityTokens { get { return null; } }

        public X509CertificateSigningCredentialsProvider(string issuer, X509Certificate2 signingCert)
        {
            _signingCredentials = new X509SigningCredentials(signingCert);
            _issuer = issuer;
        }
    }
}