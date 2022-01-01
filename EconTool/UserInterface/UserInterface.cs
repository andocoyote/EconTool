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
                        break;
                    case "p":
                        break;
                    case "s":
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
    }
}
