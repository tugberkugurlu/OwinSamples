using OAuth.Server.Domain.Enums;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OAuth.Server.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public int ClientSecretHash { get; set; }

        public OAuthFlow Flow { get; set; }

        /// <summary>
        /// Indicates whether the refresh token should be issued for this client or not.
        /// </summary>
        public bool AllowRefreshToken { get; set; }

        /// <remarks>
        /// Only checked if (Flow == Code || Flow == Implicit)
        /// </remarks>
        public bool RequireConsent { get; set; }

        /// <summary>
        /// Indicates whether the client is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Represents the allowed scopes for the client. This implicitly maps the clients
        /// to an Application as a scope is tied to an application.
        /// </summary>
        public ICollection<Scope> Scopes { get; set; }

        /// <summary>
        /// Represents the allowed redirect URIs for the client.
        /// </summary>
        public ICollection<ClientRedirectUri> RedirectUris { get; set; }
    }
}