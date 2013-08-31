using Microsoft.Owin.Security.Jwt;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace OAuth.Server.Providers
{
    public class X509CertificateSigningCredentialsProvider : ISigningCredentialsProvider
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly string _issuer;

        public SigningCredentials SigningCredentials { get { return _signingCredentials; } }
        public string Issuer { get { return _issuer; } }


        public X509CertificateSigningCredentialsProvider(string issuer, X509Certificate2 signingCert)
        {
            _signingCredentials = new X509SigningCredentials(signingCert);
            _issuer = issuer;
        }

        public IEnumerable<SecurityToken> SecurityTokens
        {
            get { throw new NotSupportedException(); }
        }
    }
}
