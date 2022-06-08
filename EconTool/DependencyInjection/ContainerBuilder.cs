using EconTool.AndoEconAPIProvider;
using EconTool.Authenticator;
using EconTool.KeyVaultProvider;
using EconTool.ServicePrincipalProvider;
using Microsoft.Extensions.DependencyInjection;

namespace EconTool.DependencyInjection
{
    public class ContainerBuilder
    {
        private readonly ServiceCollection _serviceCollection = new ServiceCollection();

        public IServiceProvider Build()
        {
            // Using Singleton for these as we only ever need one instance of each in the entire system
            _serviceCollection.AddSingleton<IKeyVaultProvider, EconTool.KeyVaultProvider.KeyVaultProvider>();

            // ServicePrincipalProvider needs a KeyVaultProvider in the constructor
            _serviceCollection.AddSingleton<IServicePrincipalProvider, EconTool.ServicePrincipalProvider.ServicePrincipalProvider>();

            // Authenticator needs a ServicePrincipalProvider in the constructor
            _serviceCollection.AddSingleton<IAuthenticator, EconTool.Authenticator.Authenticator>();

            // AndoEconAPIProvider needs the bearer token from Authenticator in the constructor
            //_serviceCollection.AddSingleton<IAndoEconAPIProvider>(x => new EconTool.AndoEconAPIProvider.AndoEconAPIProvider(x.GetService<IAuthenticator>().AccessToken));
            _serviceCollection.AddSingleton<IAndoEconAPIProvider, EconTool.AndoEconAPIProvider.AndoEconAPIProvider>();

            _serviceCollection.AddSingleton<EconTool.UserInterface.UserInterface>();

            return _serviceCollection.BuildServiceProvider();
        }
    }
}
