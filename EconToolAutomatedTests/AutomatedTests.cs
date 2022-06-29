using EconTool.DependencyInjection;
using EconTool.TestInterface;
using EconTool.UserInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EconTool.Tests
{
    [TestClass]
    public class AutomatedTests
    {
        private IServiceProvider? _services = null;

        [TestInitialize]
        public void TestInitialize()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            ContainerBuilder.ConfigureServices(serviceCollection);

            serviceCollection.AddSingleton<EconTool.UserInterface.IUserInterface, EconTool.TestInterface.TestInterface>();

            _services = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task DerivativeTest()
        {
            IUserInterface? testInterface = _services?.GetRequiredService<IUserInterface>();

            if (_services == null || testInterface == null)
            {
                return;
            }

            bool result = await testInterface.Run();

            Assert.IsTrue(result);
        }
    }
}
