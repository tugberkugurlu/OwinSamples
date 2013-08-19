using System.Runtime.Serialization;

namespace OAuth.Server.Core.Models
{
    [DataContract]
    public class TokenResponse
    {
        [DataMember(Name = OAuthConstants.Parameters.AccessToken)]
        public string AccessToken { get; set; }

        [DataMember(Name = OAuthConstants.Parameters.TokenType)]
        public string TokenType { get; set; }

        [DataMember(Name = OAuthConstants.Parameters.ExpiresIn)]
        public uint ExpiresIn { get; set; }

        [DataMember(Name = OAuthConstants.Parameters.RefreshToken)]
        public string RefreshToken { get; set; }
    }
}
