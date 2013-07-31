using CarsGallery.Clien.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarsGallery.Clien.Web.Controllers
{
    // ref: http://developer.github.com/v3/oauth/

    public class HomeController : Controller
    {
        private const string ClientId = "123456";
        private const string ClientSecret = "abcdef";
        private const string UserName = "tugberk";
        private const string Password = "098765";

        public async Task<ActionResult> Index()
        {
            // Resource Owner Password Credentials Grant http://tools.ietf.org/html/rfc6749#section-4.3
            // Access Token Request http://tools.ietf.org/html/rfc6749#section-4.3.2
            using (HttpClient client = new HttpClient())
            {
                AccessTokenResponse accessTokenResponse = await GetAccessTokenAsync(client);
                IEnumerable<Car> cars = await GetCarsAsync(client, accessTokenResponse);

                return View(cars);
            }
        }

        // privates

        private async Task<AccessTokenResponse> GetAccessTokenAsync(HttpClient client)
        {
            /*
             *** If success, returns something as follows:
             {
                 "access_token": "AQAAANCMnd8BFdERjHoAwE_Cl-sBAAAAL0Tx4aDDzEy32ulnOq3LdwAAAAACAAAAAAADZgAAwAAAABAAAAANbKP3adQpekCa-oSGJn8ZAAAAAASAAACgAAAAEAAAAH3Rzt0q4gfIsP4XhjB7cScIAQAAmj0CKW5XuJx2nadjiZcFRYLZPbF3NRUZnuc5C0-BQS4iJ04LQgErgv-bAYpO8WuDLW9RRUjFWAEpvWSY8ohmAPojWqNWrHcwe3bMeXLK7lBT12YJo0EgbeWKRgKer-yhctn3HybHuf43fGrej7RgzTca_0aKXQRV38uIv6LN7t9_ebx1Ov7QYZmoTZPqbbXhFPvjr0hRxEljUrJbABU_lS3FOz5hMTs0k8pz5WfH4BBJr2oqTMwxRrstQDUKUs0gcFAJKSOpjxbuKyqVbD7cFHJEKhbRQZO_9T_oQY29BxRXFysxCDWrWkJj0Crn1RB3Tw4v-ytZ1jDDTeOOBtJnXHIzBNvC-1vuFAAAAM3K3bYpIoId8XWs6JNc3O7vQD2I",
                 "token_type": "bearer",
                 "expires_in": 1209600,
                 "refresh_token": "AQAAANCMnd8BFdERjHoAwE_Cl-sBAAAAL0Tx4aDDzEy32ulnOq3LdwAAAAACAAAAAAADZgAAwAAAABAAAAAVRydlGVhaZNrxt4-oNdyxAAAAAASAAACgAAAAEAAAAEqR-EIUM99O9sMHbJkeJPoIAQAAeddehchEbWjQiLCH8DiPRbZ_KLCBbhwPufIH7sGgBSAeZI9RDPwYTt0ThzOt6Ea9XCmPgv63WNY86UdlwUDy4fIY2g4sRxL9MrB0TEYt90tOJQojxVWFAvDOkxn_E5Mz5IuAJ3cGuZYLaxCxGJW4G_eBOrFsDPlOCQll1Pd6VEI_DYGFzfmVR3KsW41OyAyKnO-388zylouaM31chOC0aj6JiOW2YwJMhfGxKBZgq6gSZba2oMW327LT0vGLm4c3rHVvU5TkeyfgnRIO7s4mUh4FyEgmLFR32a9NyRDQj5ir3i86U2SMPPuJiR8F8trT_6E07_ASYQdtEnQr-pHQUgM46IWWJOD4FAAAAECwQpRIKa_rKdOtYawVhw8H3Fs9"
             }
             */

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:12345/token"))
            {
                List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>> { 
                    new KeyValuePair<string, string>(Constants.Parameters.GrantType, Constants.GrantTypes.Password),
                    new KeyValuePair<string, string>(Constants.Parameters.Username, UserName),
                    new KeyValuePair<string, string>(Constants.Parameters.Password, Password),
                    new KeyValuePair<string, string>(Constants.Parameters.Scope, "Read Write")
                };

                // client_id and client_secret: http://tools.ietf.org/html/rfc6749#section-2.3.1
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", EncodeToBase64(string.Format("{0}:{1}", ClientId, ClientSecret)));
                request.Content = new FormUrlEncodedContent(values);
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    AccessTokenResponse result = await response.Content.ReadAsAsync<AccessTokenResponse>();

                    return result;
                }
            }
        }

        private async Task<IEnumerable<Car>> GetCarsAsync(HttpClient client, AccessTokenResponse accessTokenResponse)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:12345/api/cars"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenResponse.AccessToken);
                
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    IEnumerable<Car> result = await response.Content.ReadAsAsync<IEnumerable<Car>>();

                    return result;
                }
            }
        }

        private async Task<IEnumerable<Car>> GetCarsThroughAuthCode()
        {
            // Authorization Code Grant http://tools.ietf.org/html/rfc6749#section-4.1

            // Flow steps:
            // * Authorization Request
            // * Retrieve the auth code on the redirected endpoint
            // * Use the auth code to get the access_token
            // * Get the access_token on the same redirected endpoint

            throw new NotImplementedException();
        }

        private static string EncodeToBase64(string value)
        {

            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(toEncodeAsBytes);
        }
    }
}