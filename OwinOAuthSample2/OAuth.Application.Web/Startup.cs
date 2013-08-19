using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OAuth.Application.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions 
            {
                AccessTokenFormat = new JwtFormat(AppConstants.Audience, new X509CertificateSecurityTokenProvider("AS", GetSigningCertificate()))
            });
        }

        private static X509Certificate2 GetSigningCertificate()
        {
            X509Store store = new X509Store("My");
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2 signingCert = store.Certificates[0];
            store.Close();

            // We only need the public key of the certificate
            return new X509Certificate2(signingCert.RawData);
        }
    }
}
