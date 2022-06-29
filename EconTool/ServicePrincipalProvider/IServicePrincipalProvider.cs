namespace EconTool.ServicePrincipalProvider
{
    public interface IServicePrincipalProvider
    {
        Task<ServicePrincipalModel> GetServicePrincipalModel();
    }
}