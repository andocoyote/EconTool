// See https://aka.ms/new-console-template for more information

using EconTool;
using Microsoft.Identity.Client;

Console.WriteLine($"Arg count: {args.Length}");

ServicePrincipalProvider servicePrincipalProvider = new ServicePrincipalProvider();
ServicePrincipalModel servicePrincipalModel = servicePrincipalProvider.GetServicePrincipalModel();

Console.WriteLine($"ClientID: {servicePrincipalModel.ClientID}");
Console.WriteLine($"TokenSecret: {servicePrincipalModel.TokenSecret}");
Console.WriteLine($"TenantId: {servicePrincipalModel.TenantID}");
Console.WriteLine($"AppIDURI: {servicePrincipalModel.AppIDURI}");


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
Console.WriteLine($"Authentication result: {authenticationResult.AccessToken}");

// Step 3: Call Econ API: Differentiate
try
{
    // Create a new POST body for the Differentiate API
    AndoEconDifferentiateRequestModel request = new AndoEconDifferentiateRequestModel
    {
        Symbols = "x",
        Fx = "x**5 + 7*x**4 + 3"
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconDifferentiateResponseModel response = await andoEconAPIProvider.DifferentiateAsync(request);

    Console.WriteLine($"Response (Differentiate):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"Derivative: {response?.Derivative}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Step 4: Call Econ API: MarginalUtility
try
{
    // Create a new POST body for the MarginalUtility API
    AndoEconMarginalUtilityRequestModel request = new AndoEconMarginalUtilityRequestModel
    {
        Symbols = "c p",
        Fx = "sqrt(p * c)",
        Variable = "c"
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconMarginalUtilityResponseModel response = await andoEconAPIProvider.MarginalUtilityAsync(request);

    Console.WriteLine($"Response (MarginalUtility):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"MarginalUtility: {response?.MarginalUtility}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Step 5: Call Econ API: Solve
try
{
    // Create a new POST body for the Solve API
    AndoEconSolveRequestModel request = new AndoEconSolveRequestModel
    {
        Symbols = "p c",
        Fx = "sqrt(c*p)/(2*c)",
        Subs = new Dictionary<string, double>()
        {
            { "c", 2 },
            { "p", 5 }
        }
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconSolveResponseModel response = await andoEconAPIProvider.SolveAsync(request);

    Console.WriteLine($"Response (Solve):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"Result: {response?.Result}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}