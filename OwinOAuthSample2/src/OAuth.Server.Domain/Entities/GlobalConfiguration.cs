using System.ComponentModel.DataAnnotations;

namespace OAuth.Server.Domain.Entities
{
    public class GlobalConfiguration
    {
        public int Id { get; set; }
        public string AuthorizationServerName { get; set; }
        public string AuthorizationServerLogoUrl { get; set; }

        [Required]
        public string Issuer { get; set; }
    }
}
