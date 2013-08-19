
namespace OAuth.Server.Core
{
    public static class OAuthConstants
    {
        public static class Parameters
        {
            public const string ResponseType = "response_type";
            public const string GrantType = "grant_type";
            public const string ClientId = "client_id";
            public const string RedirectUri = "redirect_uri";
            public const string Scope = "scope";
            public const string State = "state";
            public const string Code = "code";
            public const string RefreshToken = "refresh_token";
            public const string Username = "username";
            public const string Password = "password";
            public const string Error = "error";
            public const string ErrorDescription = "error_description";
            public const string ErrorUri = "error_uri";
            public const string ExpiresIn = "expires_in";
            public const string AccessToken = "access_token";
            public const string TokenType = "token_type";
        }

        public static class GrantTypes
        {
            public const string Password = "password";
            public const string AuthorizationCode = "authorization_code";
            public const string ClientCredentials = "client_credentials";
            public const string RefreshToken = "refresh_token";
        }

        public static class ResponseTypes
        {
            public const string Token = "token";
            public const string Code = "code";
        }

        public static class Errors
        {
            public const string Error = "error";
            public const string InvalidRequest = "invalid_request";
            public const string InvalidClient = "invalid_client";
            public const string InvalidGrant = "invalid_grant";
            public const string UnauthorizedClient = "unauthorized_client";
            public const string UnsupportedGrantType = "unsupported_grant_type";
            public const string UnsupportedResponseType = "unsupported_response_type";
            public const string InvalidScope = "invalid_scope";
            public const string AccessDenied = "access_denied";
            public const string ServerError = "server_error";
        }

        public static class Extra
        {
            public const string ClientId = "client_id";
            public const string RedirectUri = "redirect_uri";
        }
    }
}
