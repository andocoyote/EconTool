using EconTool.KeyVaultProvider;

namespace EconTool.ServicePrincipalProvider
{
    public class ServicePrincipalProvider : IServicePrincipalProvider
    {
        private readonly IKeyVaultProvider _keyVaultProvider = null;

        public ServicePrincipalProvider(IKeyVaultProvider keyVaultProvider)
        {
            _keyVaultProvider = keyVaultProvider;
        }

        // Uses the KeyVaultProvider to build an instance of the ServicePrincipalModel
        public async Task<ServicePrincipalModel> GetServicePrincipalModel()
        {
            ServicePrincipalModel servicePrincipalModel = null;

            try
            {
                string clientID = await _keyVaultProvider.GetClientID();
                string tokenSecret = await _keyVaultProvider.GetTokenSecret();
                string tenantID = await _keyVaultProvider.GetTenantID();
                string appIDURI = await _keyVaultProvider.GetAppIDURI();

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
