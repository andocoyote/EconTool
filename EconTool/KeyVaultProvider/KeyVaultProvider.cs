using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace EconTool.KeyVaultProvider
{
    public class KeyVaultProvider : IKeyVaultProvider
    {
        private readonly string _keyVaultName = "kv-general-key-vault";
        private readonly SecretClient _client = null;

        public KeyVaultProvider()
        {
            // The executing user must have an access policy in the Key Vault
            _client = new SecretClient(
                new Uri("https://" + _keyVaultName + ".vault.azure.net"),
                new DefaultAzureCredential());
        }

        // Returns the Application Client ID for the App Registration/Service Principal
        public async Task<string> GetClientID()
        {
            return await GetKeyVaultSecret("AndoEconAPIs-ServicePrincipal-ClientID");
        }

        // Returns the token secret for the App Registration/Service Principal
        public async Task<string> GetTokenSecret()
        {
            return await GetKeyVaultSecret("AndoEconAPIs-ServicePrincipal-TokenSecret");
        }

        // Returns the Tenant ID for the App Registration/Service Principal
        public async Task<string> GetTenantID()
        {
            return await GetKeyVaultSecret("AndoEconAPIs-ServicePrincipal-TenantID");
        }

        // Returns the Application ID URI for the App Registration/Service Principal
        public async Task<string> GetAppIDURI()
        {
            return await GetKeyVaultSecret("AndoEconAPIs-ServicePrincipal-AppIDURI");
        }

        private async Task<string> GetKeyVaultSecret(string secretname)
        {
            Azure.Response<KeyVaultSecret> secret = await _client.GetSecretAsync(secretname);
            string tokenSecret = secret?.Value?.Value;

            return tokenSecret;
        }
    }
}
