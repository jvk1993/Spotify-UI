using SpotifyUI.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SpotifyUI.Services
{
    public sealed class SpotifyAuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : ISpotifyAuthenticationService
    {
        // TO check the flow of authentication, see https://developer.spotify.com/documentation/web-api/tutorials/code-pkce-flow

        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SpotifyClient");
        private readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        private const string PossibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int LengthOfCodeVerifier = 128;
        private readonly string? ClientId = configuration["spotifyHttpClient:client_id"];
        private readonly string? RedirectUrl = configuration["spotifyHttpClient:redirect_url"];
        private readonly string? response_type = configuration["spotifyHttpClient:redirect_url"];
        private readonly string? code_challenge_method = configuration["spotifyHttpClient:redirect_url"];

        public async Task<bool> Authenticate()
        {
            string randomString = GenerateRandomString();
            byte[] bytes = CreateHashValueFromString(randomString);
            string base64String = CreateBase64FromByteArray(bytes);
            await Task.Delay(100);
            return true;
        }

        private string GenerateRandomString()
        {
            byte[] randomBytes = new byte[LengthOfCodeVerifier];
            _randomNumberGenerator.GetBytes(randomBytes);

            // Map the random bytes to characters from the allowed set
            StringBuilder result = new(LengthOfCodeVerifier);
            foreach (byte b in randomBytes)
            {
                result.Append(PossibleChars[b % PossibleChars.Length]);
            }
            return result.ToString();
        }

        private static byte[] CreateHashValueFromString(string randomString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(randomString); // Use UTF-8 encoding
            byte[] hash = SHA256.HashData(bytes);
            return hash;
        }

        private static string CreateBase64FromByteArray(byte[] bytes)
        {
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
