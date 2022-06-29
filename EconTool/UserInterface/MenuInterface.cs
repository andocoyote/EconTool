using EconTool.AndoEconAPIProvider;
using EconTool.Tests;
using EconTool.UserInterface;

namespace EconTool.MenuInterface
{
    public class MenuInterface : IUserInterface
    {
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;

        public MenuInterface(IAndoEconAPIProvider andoEconAPIProvider)
        {
            _andoEconAPIProvider = andoEconAPIProvider;
        }

        public async Task<bool> Run()
        {
            string selection = "";

            while (selection.ToLower() != "q")
            {
                selection = MainMenu();

                switch (selection.ToLower())
                {
                    case "d":
                        await CalculateDerivative();
                        break;
                    case "e":
                        await EvaluateFunction();
                        break;
                    case "p":
                        await MaximumProfitFunction();
                        break;
                    case "pd":
                        await CalculatePartialDerivative();
                        break;
                    case "r":
                        await MaximumRevenueFunction();
                        break;
                    case "s":
                        await SolveFunction();
                        break;
                    case "sp":
                        Console.WriteLine("Running the sample problems:");

                        SampleProblems problems = new SampleProblems(_andoEconAPIProvider);
                        await problems.MaxRevenueProblem();

                        break;
                    case "t":
                        Console.WriteLine("Running the tests:");

                        EconToolTestRunner tests = new EconToolTestRunner(_andoEconAPIProvider);
                        await tests.RunTests();

                        break;
                    case "tm":
                        Console.WriteLine("No EconTool tests at this time.");
                        break;
                    case "q":
                        Console.WriteLine("Good-bye.");
                        break;
                    default:
                        Console.WriteLine($"You entered: {selection}");
                        Console.WriteLine("Please try agian.");
                        break;
                }
            }

            return true;
        }

        private string MainMenu()
        {
            string selection = "";

            Console.WriteLine("Math functions:");
            Console.WriteLine("\tD:   Calculate a derivative");
            Console.WriteLine("\tE:   Evaluate a function");
            Console.WriteLine("\tPD:  Calculate a partial derivative");
            Console.WriteLine("\tS:   Solve a function");
            Console.WriteLine("Economics functions:");
            Console.WriteLine("\tP:   Calculate maximum profit");
            Console.WriteLine("\tR:   Calculate maximum revenue");
            Console.WriteLine("Test functions:");
            Console.WriteLine("\tSP:  Run the sample problems");
            Console.WriteLine("\tT:   Run the tests");
            Console.WriteLine("\tTM:  Display the test menu");
            Console.WriteLine("Other:");
            Console.WriteLine("\tQ:   Quit");
            Console.Write("Enter your selection: ");

            selection = Console.ReadLine();

            return selection;
        }

        private async Task<string> CalculateDerivative()
        {
            Console.Write("Enter the function (e.g. x**5 + 7*x**4 + 3): ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol (e.g. x): ");
            string symbols = Console.ReadLine();

            AndoEconDerivativeRequestModel request = new AndoEconDerivativeRequestModel
            {
                Symbols = symbols,
                Fx = fx
            };

            AndoEconDerivativeResponseModel response = await _andoEconAPIProvider.DerivativeAsync(request);

            string result = ParseResult(response?.Derivative);

            Console.WriteLine($"Derivative: {result}");

            return response?.Derivative;
        }

        private async Task<string> CalculatePartialDerivative()
        {
            Console.Write("Enter the function (e.g. sqrt(p * c)): ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces (e.g c p): ");
            string symbols = Console.ReadLine();

            Console.Write("Enter the variable with respect which to calculate (e.g c): ");
            string variable = Console.ReadLine();

            AndoEconPartialDerivativeRequestModel request = new AndoEconPartialDerivativeRequestModel
            {
                Symbols = symbols,
                Fx = fx,
                Variable = variable
            };

            AndoEconPartialDerivativeResponseModel response = await _andoEconAPIProvider.PartialDerivativeAsync(request);

            string result = ParseResult(response?.PartialDerivative);

            Console.WriteLine($"PartialDerivative: {result}");

            return response?.PartialDerivative;
        }

        private async Task<string> EvaluateFunction()
        {
            Console.Write("Enter the function (e.g. x**2 + 4*y**3): ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces (e.g. x y): ");
            string symbols = Console.ReadLine();

            Console.Write("Enter the key:value pairs for all variables, separated by commas (x:5,y:8): ");
            string values = Console.ReadLine();

            // Create the substitution dictionary of key:value pairs for all variables
            string[] rawsubs = values.Split(',');

            if (rawsubs.Length == 0)
            {
                Console.WriteLine($"The values you entered are not correctly formatted: {values}");
                return null;
            }

            Dictionary<string, double> subs = new Dictionary<string, double>();

            foreach (string s in rawsubs)
            {
                string[] keysandvalues = s.Split(':');

                if (keysandvalues.Length != 2)
                {
                    Console.WriteLine($"The values you entered are not correctly formatted: {values}");
                    return null;
                }

                subs.Add(keysandvalues[0].Trim(), double.Parse(keysandvalues[1].Trim()));
            }

            AndoEconEvaluateRequestModel request = new AndoEconEvaluateRequestModel
            {
                Symbols = symbols,
                Fx = fx,
                Subs = subs
            };

            AndoEconEvaluateResponseModel response = await _andoEconAPIProvider.EvaluateAsync(request);

            string result = ParseResult(response?.Result);

            Console.WriteLine($"Result: {result}");

            return response?.Result;
        }

        private async Task<string> SolveFunction()
        {
            Console.Write("Enter the function (e.g. 12 - 2/3*q): ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol for which to solve (e.g. q): ");
            string symbols = Console.ReadLine();

            AndoEconSolveRequestModel request = new AndoEconSolveRequestModel
            {
                Symbols = symbols,
                Fx = fx
            };

            AndoEconSolveResponseModel response = await _andoEconAPIProvider.SolveAsync(request);

            string result = ParseResult(response?.Result != null ? String.Join(", ", response?.Result.ToArray()) : null);

            Console.WriteLine($"Result: {result}");

            return result;
        }

        private string ParseResult(string response)
        {
            string result = response ?? "Result could not be calculated";

            return result;
        }

        private async Task MaximumProfitFunction()
        {
            Console.Write("Enter the demand function (e.g. 10 - 0.001*x): ");
            string fx = Console.ReadLine();

            Console.Write("Enter the cost function (e.g 5000 + 2*x): ");
            string cx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces (e.g x y): ");
            string symbols = Console.ReadLine();

            AndoEconMaximumProfitRequestModel request = new AndoEconMaximumProfitRequestModel
            {
                Symbols = symbols,
                Fx = fx,
                Cx = cx
            };

            AndoEconMaximumProfitResponseModel response = await _andoEconAPIProvider.MaximumProfitAsync(request);

            if (response == null)
            {
                Console.WriteLine($"Response (MaximumProfit): Result could not be calculated");
            }
            else
            {
                Console.WriteLine($"Response (MaximumProfit):");
                Console.WriteLine($"DemandFunction: {response?.DemandFunction}");
                Console.WriteLine($"CostFunction: {response?.CostFunction}");
                Console.WriteLine($"RevenueFunction: {response?.RevenueFunction}");
                Console.WriteLine($"ProfitFunction: {response?.ProfitFunction}");
                Console.WriteLine($"MarginalProfitFunction: {response?.MarginalProfitFunction}");
                Console.WriteLine($"OptimumQuantity: {response?.OptimumQuantity}");
                Console.WriteLine($"ItemPrice: {response?.ItemPrice}");
                Console.WriteLine($"TotalRevenue: {response?.TotalRevenue}");
            }
        }

        private async Task MaximumRevenueFunction()
        {
            Console.Write("Enter the demand function (e.g. 10 - 0.001*x): ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces (e.g x y): ");
            string symbols = Console.ReadLine();

            AndoEconMaximumRevenueRequestModel request = new AndoEconMaximumRevenueRequestModel
            {
                Symbols = symbols,
                Fx = fx
            };

            AndoEconMaximumRevenueResponseModel response = await _andoEconAPIProvider.MaximumRevenueAsync(request);

            if (response == null)
            {
                Console.WriteLine($"Response (MaximumRevenue): Result could not be calculated");
            }
            else
            {
                Console.WriteLine($"Response (MaximumRevenue):");
                Console.WriteLine($"DemandFunction: {response?.DemandFunction}");
                Console.WriteLine($"RevenueFunction: {response?.RevenueFunction}");
                Console.WriteLine($"MarginalRevenueFunction: {response?.MarginalRevenueFunction}");
                Console.WriteLine($"OptimumQuantity: {response?.OptimumQuantity}");
                Console.WriteLine($"ItemPrice: {response?.ItemPrice}");
                Console.WriteLine($"TotalRevenue: {response?.TotalRevenue}");
            }
        }
    }
}
