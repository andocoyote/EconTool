using EconTool.AndoEconAPIProvider;

namespace EconTool.Tests
{
    public class EconToolTestRunner
    {
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;
        private List<IEconToolTest> _tests = new List<IEconToolTest>();

        public EconToolTestRunner(IAndoEconAPIProvider andoEconAPIProvider)
        {
            _andoEconAPIProvider = andoEconAPIProvider;
        }

        public async Task RunTests()
        {
            EconTool.Authenticator.AuthenticatorSingleton authenticator = EconTool.Authenticator.AuthenticatorSingleton.Instance;

            _tests.Add(
                new DerivativeTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
                    Symbols: "x",
                    Fx: "x**5 + 7*x**4 + 3"));

            _tests.Add(
                new PartialDerivativeTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
                    Symbols: "c p",
                    Fx: "sqrt(p * c)", "c"));

            _tests.Add(
                new EvaluateTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
                    Symbols: "p c",
                    Fx: "sqrt(c*p)/(2*c)",
                    Subs: new Dictionary<string, double>()
                    {
                        { "c", 2 },
                        { "p", 5 }
                    }));

            _tests.Add(
                new SolveTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
                    Symbols: "q",
                    Fx: "12 - 2/3*q"));

            _tests.Add(
                new SolveTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
                    Symbols: "x",
                    Fx: "5*x**2 + 6*x + 1"));

            _tests.Add(
                new MaximumRevenueTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
                    Symbols: "x",
                    Fx: "10 - 0.001*x"));

            _tests.Add(
                new MaximumProfitTest(
                    andoEconAPIProvider: _andoEconAPIProvider,
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
