using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool.Tests
{
    public class EconToolTestRunner
    {
        private AuthenticationResult _authenticationResult = null;
        private List<IEconToolTest> _tests = new List<IEconToolTest>();

        public EconToolTestRunner()
        {
            ;
        }

        public async Task RunTests()
        {
            await Setup();

            _tests.Add(
                new DerivativeTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "x",
                    Fx: "x**5 + 7*x**4 + 3"));

            _tests.Add(
                new PartialDerivativeTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "c p",
                    Fx: "sqrt(p * c)", "c"));

            _tests.Add(
                new EvaluateTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "p c",
                    Fx: "sqrt(c*p)/(2*c)",
                    Subs: new Dictionary<string, double>()
                    {
                        { "c", 2 },
                        { "p", 5 }
                    }));

            _tests.Add(
                new SolveTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "q",
                    Fx: "12 - 2/3*q"));

            _tests.Add(
                new SolveTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "x",
                    Fx: "5*x**2 + 6*x + 1"));

            _tests.Add(
                new MaximumRevenueTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "x",
                    Fx: "10 - 0.001*x"));

            _tests.Add(
                new MaximumProfitTest(
                    AccessToken: _authenticationResult.AccessToken,
                    Symbols: "x",
                    Fx: "10 - 0.001*x",
                    Cx: "5000 + 2*x"));

            foreach (IEconToolTest test in _tests)
            {
                await test.Run();
                test.Display();
            }
        }

        private async Task Setup()
        {
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
            this._authenticationResult = await app
                .AcquireTokenForClient(scopes)
                .ExecuteAsync();

            // authenticationResult.AccessToken is the bearer token
            Console.WriteLine($"Authentication result: {_authenticationResult.AccessToken}");
        }
    }
}
