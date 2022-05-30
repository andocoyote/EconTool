namespace EconTool.KeyVaultProvider
{
    public interface IKeyVaultProvider
    {
        Task<string> GetAppIDURI();
        Task<string> GetClientID();
        Task<string> GetTenantID();
        Task<string> GetTokenSecret();
    }
}