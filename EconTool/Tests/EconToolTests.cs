using EconTool.AndoEconAPIProvider;

namespace EconTool.Tests
{
    /// <summary>
    /// Tests the Derivative API
    /// </summary>
    public class DerivativeTest : IEconToolTest
    {
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private AndoEconDerivativeRequestModel _request = null;
        private AndoEconDerivativeResponseModel _response = null;

        public DerivativeTest(IAndoEconAPIProvider andoEconAPIProvider, string Symbols, string Fx)
        {
            this._andoEconAPIProvider = andoEconAPIProvider;

            // Create a new POST body for the Derivative API
            this._request = new AndoEconDerivativeRequestModel
            {
                Symbols = Symbols,
                Fx = Fx
            };
            _andoEconAPIProvider = andoEconAPIProvider;
        }

        public async Task Run()
        {
            // Call Econ API: Derivative
            try
            {
                _response = await _andoEconAPIProvider.DerivativeAsync(_request);
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
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private AndoEconPartialDerivativeRequestModel _request = null;
        private AndoEconPartialDerivativeResponseModel _response = null;

        public PartialDerivativeTest(IAndoEconAPIProvider andoEconAPIProvider, string Symbols, string Fx, string Variable)
        {
            this._andoEconAPIProvider = andoEconAPIProvider;

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
                _response = await _andoEconAPIProvider.PartialDerivativeAsync(_request);
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
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private AndoEconEvaluateRequestModel _request = null;
        private AndoEconEvaluateResponseModel _response = null;

        public EvaluateTest(IAndoEconAPIProvider andoEconAPIProvider, string Symbols, string Fx, Dictionary<string, double> Subs)
        {
            this._andoEconAPIProvider = andoEconAPIProvider;

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
                _response = await _andoEconAPIProvider.EvaluateAsync(_request);
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
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private AndoEconSolveRequestModel _request = null;
        private AndoEconSolveResponseModel _response = null;

        public SolveTest(IAndoEconAPIProvider andoEconAPIProvider, string Symbols, string Fx)
        {
            this._andoEconAPIProvider = andoEconAPIProvider;

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
                _response = await _andoEconAPIProvider.SolveAsync(_request);
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

    /// <summary>
    /// Tests the MaximumRevenue API
    /// </summary>
    public class MaximumRevenueTest : IEconToolTest
    {
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private AndoEconMaximumRevenueRequestModel _request = null;
        private AndoEconMaximumRevenueResponseModel _response = null;

        public MaximumRevenueTest(IAndoEconAPIProvider andoEconAPIProvider, string Symbols, string Fx)
        {
            this._andoEconAPIProvider = andoEconAPIProvider;

            // Create a new POST body for the MaximumRevenue API
            _request = new AndoEconMaximumRevenueRequestModel
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
                _response = await _andoEconAPIProvider.MaximumRevenueAsync(_request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"MaximumRevenueTest failed with the following exception: {e}");
            }
        }

        public void Display()
        {
            Console.WriteLine($"Response (MaximumRevenue):");
            Console.WriteLine($"DemandFunction: {_response?.DemandFunction}");
            Console.WriteLine($"RevenueFunction: {_response?.RevenueFunction}");
            Console.WriteLine($"MarginalRevenueFunction: {_response?.MarginalRevenueFunction}");
            Console.WriteLine($"OptimumQuantity: {_response?.OptimumQuantity}");
            Console.WriteLine($"ItemPrice: {_response?.ItemPrice}");
            Console.WriteLine($"TotalRevenue: {_response?.TotalRevenue}");
        }
    }

    /// <summary>
    /// Tests the MaximumProfit API
    /// </summary>
    public class MaximumProfitTest : IEconToolTest
    {
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private AndoEconMaximumProfitRequestModel _request = null;
        private AndoEconMaximumProfitResponseModel _response = null;

        public MaximumProfitTest(IAndoEconAPIProvider andoEconAPIProvider, string Symbols, string Fx, string Cx)
        {
            this._andoEconAPIProvider = andoEconAPIProvider;

            // Create a new POST body for the MaximumProfit API
            _request = new AndoEconMaximumProfitRequestModel
            {
                Symbols = Symbols,
                Fx = Fx,
                Cx = Cx
            };
        }

        public async Task Run()
        {
            // Call Econ API: Solve
            try
            {
                _response = await _andoEconAPIProvider.MaximumProfitAsync(_request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"MaximumProfitTest failed with the following exception: {e}");
            }
        }

        public void Display()
        {
            Console.WriteLine($"Response (MaximumProfit):");
            Console.WriteLine($"DemandFunction: {_response?.DemandFunction}");
            Console.WriteLine($"CostFunction: {_response?.CostFunction}");
            Console.WriteLine($"RevenueFunction: {_response?.RevenueFunction}");
            Console.WriteLine($"ProfitFunction: {_response?.ProfitFunction}");
            Console.WriteLine($"MarginalProfitFunction: {_response?.MarginalProfitFunction}");
            Console.WriteLine($"OptimumQuantity: {_response?.OptimumQuantity}");
            Console.WriteLine($"ItemPrice: {_response?.ItemPrice}");
            Console.WriteLine($"TotalRevenue: {_response?.TotalRevenue}");
        }
    }
}