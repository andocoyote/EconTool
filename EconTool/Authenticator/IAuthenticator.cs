namespace EconTool.Authenticator
{
    public interface IAuthenticator
    {
        string AccessToken { get; set; }
        string AppIDURI { get; set; }
        string ClientID { get; set; }
        string TenantID { get; set; }
        string TokenSecret { get; set; }

        void Display();
    }
}