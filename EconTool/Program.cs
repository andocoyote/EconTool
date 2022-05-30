// See https://aka.ms/new-console-template for more information

using EconTool.AndoEconAPIProvider;
using EconTool.Authenticator;
using EconTool.DependencyInjection;
using EconTool.UserInterface;
using Microsoft.Extensions.DependencyInjection;

IServiceProvider services = new ContainerBuilder().Build();

IAndoEconAPIProvider andoEconApiProvider = services.GetService<IAndoEconAPIProvider>();

UserInterface ui = new UserInterface(andoEconApiProvider);

// If you don't await this call, execution of the current method continues before the call is completed
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs4014?f1url=%3FappId%3Droslyn%26k%3Dk(CS4014)
await ui.Run();