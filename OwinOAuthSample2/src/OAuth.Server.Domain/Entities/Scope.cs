using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OAuth.Server.Domain.Entities
{
    public class Scope
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsEmphasized { get; set; }

        /// <summary>
        /// Represents the related Application.
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// Represents the allowed clients for this Scope which means that the client can ask for
        /// a permision for the specified scope.
        /// </summary>
        public ICollection<Client> Clients { get; set; }
    }
}