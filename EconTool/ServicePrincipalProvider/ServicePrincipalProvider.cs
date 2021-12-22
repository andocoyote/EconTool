using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool
{
    public class ServicePrincipalProvider
    {
        public ServicePrincipalProvider()
        {
            ;
        }

        // Uses the KeyVaultProvider to build an instance of the ServicePrincipalModel
        public ServicePrincipalModel GetServicePrincipalModel()
        {
            ServicePrincipalModel servicePrincipalModel = null;

            try
            {
                KeyVaultProvider keyVaultProvider = new KeyVaultProvider();

                string clientID = keyVaultProvider.GetClientID().GetAwaiter().GetResult();
                string tokenSecret = keyVaultProvider.GetTokenSecret().GetAwaiter().GetResult();
                string tenantID = keyVaultProvider.GetTenantID().GetAwaiter().GetResult();
                string appIDURI = keyVaultProvider.GetAppIDURI().GetAwaiter().GetResult();

                if (string.IsNullOrEmpty(clientID) ||
                    string.IsNullOrEmpty(tokenSecret) ||
                    string.IsNullOrEmpty(tenantID) ||
                    string.IsNullOrEmpty(appIDURI))
                {
                    Console.WriteLine("[ServicePrincipalModel][GetServicePrincipalModel] " +
                        "Failed to obtain all secrets from Key Vault: " +
                        $"ClientID: {clientID}, TokenSecret: {tokenSecret}, TenantID: {tenantID}, AppIDURI: {appIDURI}");
                }
                else
                {
                    servicePrincipalModel = new ServicePrincipalModel()
                    {
                        ClientID = clientID,
                        TokenSecret = tokenSecret,
                        TenantID = tenantID,
                        AppIDURI = appIDURI
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ServicePrincipalModel][GetServicePrincipalModel] " +
                    $"Failed to obtain secrets from Key Vault with exception: {ex.ToString()}");
            }

            return servicePrincipalModel;
        }
    }
}
