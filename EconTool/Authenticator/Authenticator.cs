using EconTool.ServicePrincipalProvider;
using Microsoft.Identity.Client;

namespace EconTool.Authenticator
{
    public sealed class Authenticator : IAuthenticator
    {
        public string ClientID { get; set; } = null;
        public string TokenSecret { get; set; } = null;
        public string TenantID { get; set; } = null;
        public string AppIDURI { get; set; } = null;
        public string AccessToken { get; set; } = null;

        private readonly IServicePrincipalProvider _servicePrincipalProvider = null;

        // Authenticator users a ServicePrincipalProvider to get AAD credentials and the bearer token
        public Authenticator(IServicePrincipalProvider servicePrincipalProvider)
        {
            this._servicePrincipalProvider = servicePrincipalProvider;

            Setup().GetAwaiter().GetResult();
        }

        private async Task Setup()
        {
            ServicePrincipalModel servicePrincipalModel = await _servicePrincipalProvider.GetServicePrincipalModel();

            this.ClientID = servicePrincipalModel.ClientID;
            this.TokenSecret = servicePrincipalModel.TokenSecret;
            this.TenantID = servicePrincipalModel.TenantID;
            this.AppIDURI = servicePrincipalModel.AppIDURI;


            // Step 1: Initialize MSAL which will be used to connect to the Econ APIs
            // Initialize Microsoft Authentication Library (MSAL, in the Microsoft.Identity.Client package)
            // MSLA is the library that's used to sign in users and request tokens for
            // accessing an API protected by the Microsoft identity platform
            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder
                .Create(servicePrincipalModel.ClientID)
                .WithClientSecret(servicePrincipalModel.TokenSecret)
                .WithAuthority(new Uri($"https://login.microsoftonline.com/{servicePrincipalModel.TenantID}"))
                .Build();

            string[] scopes = { servicePrincipalModel.AppIDURI + "/.default" };

            // Step 2: Obtain the bearer token
            AuthenticationResult authenticationResult = await app
                .AcquireTokenForClient(scopes)
                .ExecuteAsync();

            // authenticationResult.AccessToken is the bearer token
            // Step 3: Assign the token to AccessToken for use by the application
            this.AccessToken = authenticationResult.AccessToken;
        }

        public void Display()
        {
            Console.WriteLine($"ClientID: {ClientID}");
            Console.WriteLine($"TokenSecret: {TokenSecret}");
            Console.WriteLine($"TenantId: {TenantID}");
            Console.WriteLine($"AppIDURI: {AppIDURI}");
            Console.WriteLine($"Authentication result: {AccessToken}");

        }
    }
}
