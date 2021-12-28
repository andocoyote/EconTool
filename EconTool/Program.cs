// See https://aka.ms/new-console-template for more information

using EconTool;
using Microsoft.Identity.Client;

Console.WriteLine($"{args.Length} arguments supplied.");

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

// Step 3: Call Econ API: Derivative
try
{
    // Create a new POST body for the Derivative API
    AndoEconDerivativeRequestModel request = new AndoEconDerivativeRequestModel
    {
        Symbols = "x",
        Fx = "x**5 + 7*x**4 + 3"
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconDerivativeResponseModel response = await andoEconAPIProvider.DerivativeAsync(request);

    Console.WriteLine($"Response (Derivative):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"Derivative: {response?.Derivative}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Step 4: Call Econ API: PartialDerivative
try
{
    // Create a new POST body for the MarginalUtility API
    AndoEconPartialDerivativeRequestModel request = new AndoEconPartialDerivativeRequestModel
    {
        Symbols = "c p",
        Fx = "sqrt(p * c)",
        Variable = "c"
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconPartialDerivativeResponseModel response = await andoEconAPIProvider.PartialDerivativeAsync(request);

    Console.WriteLine($"Response (PartialDerivative):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"PartialDerivative: {response?.PartialDerivative}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Step 5: Call Econ API: Evaluate
try
{
    // Create a new POST body for the Evaluate API
    AndoEconEvaluateRequestModel request = new AndoEconEvaluateRequestModel
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
    AndoEconEvaluateResponseModel response = await andoEconAPIProvider.EvaluateAsync(request);

    Console.WriteLine($"Response (Evaluate):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"Result: {response?.Result}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Step 6: Call Econ API: Solve
try
{
    // Create a new POST body for the Solve API
    AndoEconSolveRequestModel request = new AndoEconSolveRequestModel
    {
        Symbols = "q",
        Fx = "12 - 2/3*q"
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconSolveResponseModel response = await andoEconAPIProvider.SolveAsync(request);

    Console.WriteLine($"Response (Solve):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"Result: {String.Join(", ", response?.Result.ToArray())}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// Step 7: Call Econ API: Solve
try
{
    // Create a new POST body for the Solve API
    AndoEconSolveRequestModel request = new AndoEconSolveRequestModel
    {
        Symbols = "x",
        Fx = "5*x**2 + 6*x + 1"
    };

    AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticationResult.AccessToken);
    AndoEconSolveResponseModel response = await andoEconAPIProvider.SolveAsync(request);

    Console.WriteLine($"Response (Solve):");
    Console.WriteLine($"Fx: {response?.Fx}");
    Console.WriteLine($"Result: {String.Join(", ", response?.Result.ToArray())}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}