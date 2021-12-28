using Microsoft.Identity.Client;
using System;

namespace EconTool.Tests
{
    /// <summary>
    /// Tests the Derivative API
    /// </summary>
    public class DerivativeTest : IEconToolTest
    {
        private string _accessToken = null;
        private AndoEconDerivativeRequestModel _request = null;
        private AndoEconDerivativeResponseModel _response = null;

        public DerivativeTest(string AccessToken, string Symbols, string Fx)
        {
            this._accessToken = AccessToken;

            // Create a new POST body for the Derivative API
            this._request = new AndoEconDerivativeRequestModel
            {
                Symbols = Symbols,
                Fx = Fx
            };
        }

        public async Task Run()
        {
            // Call Econ API: Derivative
            try
            {
                AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_accessToken);
                _response = await andoEconAPIProvider.DerivativeAsync(_request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"DerivativeTest failed with the following exception: {e}");
            }
        }

        public void Display()
        {
            Console.WriteLine($"Response (Derivative):");
            Console.WriteLine($"Fx: {_response?.Fx}");
            Console.WriteLine($"Derivative: {_response?.Derivative}");
        }
    }

    /// <summary>
    /// Tests the PartialDerivative API
    /// </summary>
    public class PartialDerivativeTest : IEconToolTest
    {
        private string _accessToken = null;
        private AndoEconPartialDerivativeRequestModel _request = null;
        private AndoEconPartialDerivativeResponseModel _response = null;

        public PartialDerivativeTest(string AccessToken, string Symbols, string Fx, string Variable)
        {
            this._accessToken = AccessToken;

            // Create a new POST body for the MarginalUtility API
            _request = new AndoEconPartialDerivativeRequestModel
            {
                Symbols = Symbols,
                Fx = Fx,
                Variable = Variable
            };
        }

        public async Task Run()
        {
            // Call Econ API: Derivative
            try
            {
                AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_accessToken);
                _response = await andoEconAPIProvider.PartialDerivativeAsync(_request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"PartialDerivativeTest failed with the following exception: {e}");
            }
        }

        public void Display()
        {
            Console.WriteLine($"Response (PartialDerivative):");
            Console.WriteLine($"Fx: {_response?.Fx}");
            Console.WriteLine($"PartialDerivative: {_response?.PartialDerivative}");
        }
    }

    /// <summary>
    /// Tests the Evaluate API
    /// </summary>
    public class EvaluateTest : IEconToolTest
    {
        private string _accessToken = null;
        private AndoEconEvaluateRequestModel _request = null;
        private AndoEconEvaluateResponseModel _response = null;

        public EvaluateTest(string AccessToken, string Symbols, string Fx, Dictionary<string, double> Subs)
        {
            this._accessToken = AccessToken;

            // Create a new POST body for the Evaluate API
            _request = new AndoEconEvaluateRequestModel
            {
                Symbols = Symbols,
                Fx = Fx,
                Subs = Subs
            };
        }

        public async Task Run()
        {
            // Call Econ API: Evaluate
            try
            {
                AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_accessToken);
                _response = await andoEconAPIProvider.EvaluateAsync(_request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"EvaluateTest failed with the following exception: {e}");
            }
        }

        public void Display()
        {
            Console.WriteLine($"Response (Evaluate):");
            Console.WriteLine($"Fx: {_response?.Fx}");
            Console.WriteLine($"Result: {_response?.Result}");
        }
    }

    /// <summary>
    /// Tests the Solve API
    /// </summary>
    public class SolveTest : IEconToolTest
    {
        private string _accessToken = null;
        private AndoEconSolveRequestModel _request = null;
        private AndoEconSolveResponseModel _response = null;

        public SolveTest(string AccessToken, string Symbols, string Fx)
        {
            this._accessToken = AccessToken;

            // Create a new POST body for the Solve API
            _request = new AndoEconSolveRequestModel
            {
                Symbols = Symbols,
                Fx = Fx
            };
        }

        public async Task Run()
        {
            // Call Econ API: Solve
            try
            {
                AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_accessToken);
                _response = await andoEconAPIProvider.SolveAsync(_request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"SolveTest failed with the following exception: {e}");
            }
        }

        public void Display()
        {
            Console.WriteLine($"Response (Solve):");
            Console.WriteLine($"Fx: {_response?.Fx}");
            Console.WriteLine($"Result: {String.Join(", ", _response?.Result.ToArray())}");
        }
    }
}