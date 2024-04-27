namespace SpotifyUI.Services.Interfaces
{
    public interface ISpotifyAuthenticationService
    {
        Task<bool> Authenticate();
    }
}
