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
                EconTool.Authenticator.Authenticator authenticator = EconTool.Authenticator.Authenticator.Instance;

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

                AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(authenticator.AccessToken);
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
    }
}
