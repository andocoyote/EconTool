using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool
{
    public class KeyVaultProvider
    {
        private string keyVaultName = "kv-general-key-vault";
        private SecretClient client;

        public KeyVaultProvider()
        {
            // The executing user must have an access policy in the Key Vault
            client = new SecretClient(
                new Uri("https://" + keyVaultName + ".vault.azure.net"),
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
            Azure.Response<KeyVaultSecret> secret = await client.GetSecretAsync(secretname);
            string tokenSecret = secret?.Value?.Value;

            return tokenSecret;
        }
    }
}
