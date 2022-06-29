using EconTool.AndoEconAPIProvider;
using EconTool.UserInterface;

namespace EconTool.TestInterface
{
    public class TestInterface : IUserInterface
    {
        private readonly IAndoEconAPIProvider _andoEconAPIProvider = null;

        public TestInterface(IAndoEconAPIProvider andoEconAPIProvider)
        {
            _andoEconAPIProvider = andoEconAPIProvider;
        }

        public async Task<bool> Run()
        {
            string fx = "x**5 + 7*x**4 + 3";
            string symbols = "x";

            // Run the tests
            string actualResult = await CalculateDerivative(symbols, fx);
            string expectedResult = "5*x**4 + 28*x**3";

            return actualResult == expectedResult;
        }

        private async Task<string> CalculateDerivative(string symbols, string fx)
        {
            AndoEconDerivativeRequestModel request = new AndoEconDerivativeRequestModel
            {
                Symbols = symbols,
                Fx = fx
            };

            AndoEconDerivativeResponseModel response = await _andoEconAPIProvider.DerivativeAsync(request);

            return response?.Derivative;
        }
    }
}
