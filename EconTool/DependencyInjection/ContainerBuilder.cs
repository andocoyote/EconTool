using EconTool.AndoEconAPIProvider;
using EconTool.Authenticator;
using EconTool.KeyVaultProvider;
using EconTool.ServicePrincipalProvider;
using EconTool.UserInterface;
using Microsoft.Extensions.DependencyInjection;

namespace EconTool.DependencyInjection
{
    public static class ContainerBuilder
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Using Singleton for these as we only ever need one instance of each in the entire system
            serviceCollection.AddSingleton<IKeyVaultProvider, EconTool.KeyVaultProvider.KeyVaultProvider>();

            // ServicePrincipalProvider needs a KeyVaultProvider in the constructor
            serviceCollection.AddSingleton<IServicePrincipalProvider, EconTool.ServicePrincipalProvider.ServicePrincipalProvider>();

            // Authenticator needs a ServicePrincipalProvider in the constructor
            serviceCollection.AddSingleton<IAuthenticator, EconTool.Authenticator.Authenticator>();

            // AndoEconAPIProvider needs the bearer token from Authenticator in the constructor
            //_serviceCollection.AddSingleton<IAndoEconAPIProvider>(x => new EconTool.AndoEconAPIProvider.AndoEconAPIProvider(x.GetService<IAuthenticator>().AccessToken));
            serviceCollection.AddSingleton<IAndoEconAPIProvider, EconTool.AndoEconAPIProvider.AndoEconAPIProvider>();

            // MenuInterface uses everything
            serviceCollection.AddSingleton<IUserInterface, EconTool.MenuInterface.MenuInterface>();
        }
    }
}
