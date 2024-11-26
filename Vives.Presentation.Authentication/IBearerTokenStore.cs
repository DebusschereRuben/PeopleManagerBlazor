namespace Vives.Presentation.Authentication
{
    public interface IBearerTokenStore
    {
        public string GetToken();
        public void SetToken(string token);

    }
}
