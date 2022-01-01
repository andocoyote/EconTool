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
        private List<IEconToolTest> _tests = new List<IEconToolTest>();

        public EconToolTestRunner()
        {
            ;
        }

        public async Task RunTests()
        {
            EconTool.Authenticator.Authenticator authenticator = EconTool.Authenticator.Authenticator.Instance;

            _tests.Add(
                new DerivativeTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "x",
                    Fx: "x**5 + 7*x**4 + 3"));

            _tests.Add(
                new PartialDerivativeTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "c p",
                    Fx: "sqrt(p * c)", "c"));

            _tests.Add(
                new EvaluateTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "p c",
                    Fx: "sqrt(c*p)/(2*c)",
                    Subs: new Dictionary<string, double>()
                    {
                        { "c", 2 },
                        { "p", 5 }
                    }));

            _tests.Add(
                new SolveTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "q",
                    Fx: "12 - 2/3*q"));

            _tests.Add(
                new SolveTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "x",
                    Fx: "5*x**2 + 6*x + 1"));

            _tests.Add(
                new MaximumRevenueTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "x",
                    Fx: "10 - 0.001*x"));

            _tests.Add(
                new MaximumProfitTest(
                    AccessToken: authenticator.AccessToken,
                    Symbols: "x",
                    Fx: "10 - 0.001*x",
                    Cx: "5000 + 2*x"));

            foreach (IEconToolTest test in _tests)
            {
                await test.Run();
                test.Display();
            }
        }
    }
}
