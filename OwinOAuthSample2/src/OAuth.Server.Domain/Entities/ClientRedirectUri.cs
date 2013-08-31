using System.ComponentModel.DataAnnotations;

namespace OAuth.Server.Domain.Entities
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }

        [Required]
        public string Uri { get; set; }

        public string Description { get; set; }

        public Client Client { get; set; }
    }
}