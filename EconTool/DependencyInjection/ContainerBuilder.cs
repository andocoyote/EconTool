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
            // Using Singleton but could also use Transient
            _serviceCollection.AddSingleton<IKeyVaultProvider, EconTool.KeyVaultProvider.KeyVaultProvider>();

            // ServicePrincipalProvider needs a KeyVaultProvider in the constructor
            _serviceCollection.AddSingleton<IServicePrincipalProvider>(x => new EconTool.ServicePrincipalProvider.ServicePrincipalProvider(x.GetService<IKeyVaultProvider>()));

            // Authenticator needs a ServicePrincipalProvider in the constructor
            _serviceCollection.AddSingleton<IAuthenticator>(x => new EconTool.Authenticator.Authenticator(x.GetService<IServicePrincipalProvider>()));

            // AndoEconAPIProvider needs the bearer token from Authenticator in the constructor
            _serviceCollection.AddSingleton<IAndoEconAPIProvider>(x => new EconTool.AndoEconAPIProvider.AndoEconAPIProvider(x.GetService<IAuthenticator>().AccessToken));

            return _serviceCollection.BuildServiceProvider();
        }
    }
}
