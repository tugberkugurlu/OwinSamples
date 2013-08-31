using OAuth.Server.Domain.Entities;
using System.Data.Entity;

namespace OAuth.Server.Domain
{
    public class AuthorizationServerContext : DbContext
    {
        public AuthorizationServerContext()
            : base("AuthorizationServer")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<GlobalConfiguration> GlobalConfigurations { get; set; }
        public IDbSet<Application> Applications { get; set; }
        public IDbSet<Client> Clients { get; set; }
        public IDbSet<Scope> Scopes { get; set; }
        public IDbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
    }
}