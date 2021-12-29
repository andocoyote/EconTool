using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool.Tests
{
    public class SampleProblems
    {
        private AuthenticationResult _authenticationResult = null;

        public SampleProblems()
        {
            ;
        }

        public async Task MaxRevenueProblem()
        {
            /*
            Problem statement: // Given the demand equation above, what price should be charged to maximuze revenue?

            C(x): cost of producing x units
            R(x): revenue from producing/selling x units, or x * price p, but price will vary based on quantity demanded
            P(x): profit is R(x) - C(x)

            Therefore, if:
            price/demand equation: p = 10 - 0.001x
            R(x) = x * price, or x *(10 - 0.001x), or 10x - 0.001x^2
            MR = 10 - 0.002x = 0
            */
            try
            {
                await Setup();

                string symbols = "x";
                string priceEquation = "10 - 0.001 * " + symbols;
                string revenueEquation = symbols + " * (" + priceEquation + ")";

                Console.WriteLine($"Demand function: {priceEquation}");

                // Step 1: Obtain function for Marginal Revenue
                AndoEconDerivativeRequestModel marginalRevenueRequest = new AndoEconDerivativeRequestModel
                {
                    Symbols = symbols,
                    Fx = revenueEquation
                };

                AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_authenticationResult.AccessToken);
                AndoEconDerivativeResponseModel marginalRevenueResponse = await andoEconAPIProvider.DerivativeAsync(marginalRevenueRequest);

                Console.WriteLine($"Marginal revenue function: {marginalRevenueResponse?.Derivative}");

                // Step 2: determine the price per item that maximizes revenue (revenue is maximized when MR == 0)
                AndoEconSolveRequestModel maxRevenueRequest = new AndoEconSolveRequestModel
                {
                    Symbols = symbols,
                    Fx = marginalRevenueResponse?.Derivative
                };

                AndoEconSolveResponseModel optimumQuantityResponse = await andoEconAPIProvider.SolveAsync(maxRevenueRequest);

                Console.WriteLine($"Item quantity: {String.Join(", ", optimumQuantityResponse?.Result.ToArray())}");

                AndoEconEvaluateRequestModel itemPriceRequest = new AndoEconEvaluateRequestModel
                {
                    Symbols = symbols,
                    Fx = priceEquation,
                    Subs = new Dictionary<string, double>()
                    {
                        { "x", optimumQuantityResponse.Result.ToArray().First<double>() }
                    }
                };

                AndoEconEvaluateResponseModel itemPriceResponse = await andoEconAPIProvider.EvaluateAsync(itemPriceRequest);

                Console.WriteLine($"Item price (dollars): {itemPriceResponse?.Result}");

                // Step 3: determine the total revenue at the price per item that maximizes revenue
                AndoEconEvaluateRequestModel totalRevenueRequest = new AndoEconEvaluateRequestModel
                {
                    Symbols = symbols,
                    Fx = revenueEquation,
                    Subs = new Dictionary<string, double>()
                    {
                        { "x", optimumQuantityResponse.Result.ToArray().First<double>() }
                    }
                };

                AndoEconEvaluateResponseModel totalRevenueResponse = await andoEconAPIProvider.EvaluateAsync(totalRevenueRequest);

                Console.WriteLine($"Total revenue (dollars): {totalRevenueResponse?.Result}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"SampleProblem failed with the following exception: {e}");
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
