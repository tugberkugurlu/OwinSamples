using System.Runtime.Serialization;

namespace CarsGallery.Clien.Web.Models
{
    [DataContract]
    public class AccessTokenResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "expires_in")]
        public uint ExpiresIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}