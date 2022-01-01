using EconTool.Authenticator;
using EconTool.Tests;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconTool.UserInterface
{
    public class UserInterface
    {
        private EconTool.Authenticator.Authenticator _authenticator = EconTool.Authenticator.Authenticator.Instance;

        public UserInterface()
        {
            ;
        }

        public async Task Run()
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
                        await CalculatePartialDerivative();
                        break;
                    case "s":
                        await SolveFunction();
                        break;
                    case "sp":
                        Console.WriteLine("Running the sample problems:");

                        SampleProblems problems = new SampleProblems();
                        await problems.MaxRevenueProblem();

                        break;
                    case "t":
                        Console.WriteLine("Running the tests:");

                        EconToolTestRunner tests = new EconToolTestRunner();
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
        }

        private string MainMenu()
        {
            string selection = "";

            Console.WriteLine("D:   Calculate a derivative");
            Console.WriteLine("E:   Evaluate a function");
            Console.WriteLine("P:   Calculate a partial derivative");
            Console.WriteLine("S:   Solve a function");
            Console.WriteLine("SP:  Run the sample problems");
            Console.WriteLine("T:   Run the tests");
            Console.WriteLine("TM:  Display the test menu");
            Console.WriteLine("Q:   Quit");
            Console.Write("Enter your selection: ");

            selection = Console.ReadLine();

            return selection;
        }

        private async Task<string> CalculateDerivative()
        {
            Console.Write("Enter the function: ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces: ");
            string symbols = Console.ReadLine();

            AndoEconDerivativeRequestModel request = new AndoEconDerivativeRequestModel
            {
                Symbols = symbols,
                Fx = fx
            };

            AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_authenticator.AccessToken);
            AndoEconDerivativeResponseModel response = await andoEconAPIProvider.DerivativeAsync(request);

            Console.WriteLine($"Derivative: {response?.Derivative}");

            return response?.Derivative;
        }

        private async Task<string> CalculatePartialDerivative()
        {
            Console.Write("Enter the function: ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces: ");
            string symbols = Console.ReadLine();

            Console.Write("Enter the variable with respect which to calculate: ");
            string variable = Console.ReadLine();

            AndoEconPartialDerivativeRequestModel request = new AndoEconPartialDerivativeRequestModel
            {
                Symbols = symbols,
                Fx = fx,
                Variable = variable
            };

            AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_authenticator.AccessToken);
            AndoEconPartialDerivativeResponseModel response = await andoEconAPIProvider.PartialDerivativeAsync(request);

            Console.WriteLine($"PartialDerivative: {response?.PartialDerivative}");

            return response?.PartialDerivative;
        }

        private async Task<string> EvaluateFunction()
        {
            Console.Write("Enter the function: ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol(s) separated by spaces: ");
            string symbols = Console.ReadLine();

            Console.Write("Enter the key:value pairs for all variables, separated by commas (x:5,y:8): ");
            string values = Console.ReadLine();

            // Create the substitution dictionary of key:value pairs for all variables
            string[] rawsubs = values.Split(',');

            if (rawsubs.Length != 2)
            {
                Console.WriteLine($"The values you entered are not correctly formatted: {rawsubs}");
                return null;
            }

            Dictionary<string, double> subs = new Dictionary<string, double>();

            foreach (string s in rawsubs)
            {
                string[] keysandvalues = s.Split(':');

                if (keysandvalues.Length != 2)
                {
                    Console.WriteLine($"The values you entered are not correctly formatted: {keysandvalues}");
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

            AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_authenticator.AccessToken);
            AndoEconEvaluateResponseModel response = await andoEconAPIProvider.EvaluateAsync(request);

            Console.WriteLine($"Result: {response?.Result}");

            return response?.Result;
        }

        private async Task<string> SolveFunction()
        {
            Console.Write("Enter the function: ");
            string fx = Console.ReadLine();

            Console.Write("Enter the symbol for which to solve: ");
            string symbols = Console.ReadLine();

            AndoEconSolveRequestModel request = new AndoEconSolveRequestModel
            {
                Symbols = symbols,
                Fx = fx
            };

            AndoEconAPIProvider andoEconAPIProvider = new AndoEconAPIProvider(_authenticator.AccessToken);
            AndoEconSolveResponseModel response = await andoEconAPIProvider.SolveAsync(request);

            string result = String.Join(", ", response?.Result.ToArray());

            Console.WriteLine($"Result: {result}");

            return result;
        }
    }
}
