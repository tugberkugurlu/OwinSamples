using OAuth.Server.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OAuth.Server.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string Namespace { get; set; }
        public string Audience { get; set; }

        [Range(0, Int32.MaxValue)]
        public int TokenLifetime { get; set; }
        public bool AllowRefreshToken { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsentDecision { get; set; }

        public ApplicationStatus Status { get; set; }

        public ICollection<Scope> Scopes { get; set; }
    }
}