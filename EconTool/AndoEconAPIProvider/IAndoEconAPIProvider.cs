namespace EconTool.AndoEconAPIProvider
{
    public interface IAndoEconAPIProvider
    {
        Task<TResponse> CallAPIAsync<TRequest, TResponse>(TRequest request, string api);
        Task<AndoEconDerivativeResponseModel> DerivativeAsync(AndoEconDerivativeRequestModel request);
        Task<AndoEconEvaluateResponseModel> EvaluateAsync(AndoEconEvaluateRequestModel request);
        Task<AndoEconMaximumProfitResponseModel> MaximumProfitAsync(AndoEconMaximumProfitRequestModel request);
        Task<AndoEconMaximumRevenueResponseModel> MaximumRevenueAsync(AndoEconMaximumRevenueRequestModel request);
        Task<AndoEconPartialDerivativeResponseModel> PartialDerivativeAsync(AndoEconPartialDerivativeRequestModel request);
        Task<AndoEconSolveResponseModel> SolveAsync(AndoEconSolveRequestModel request);
    }
}