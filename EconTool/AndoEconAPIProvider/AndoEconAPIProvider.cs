using EconTool.Authenticator;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EconTool.AndoEconAPIProvider
{
    public class AndoEconAPIProvider : IAndoEconAPIProvider
    {
        private readonly HttpClient _client = new HttpClient();

        public AndoEconAPIProvider(IAuthenticator authenticator)
        {
            _client.BaseAddress = new Uri("https://andoeconapis.azurewebsites.net/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authenticator.AccessToken);
        }

        public async Task<AndoEconDerivativeResponseModel> DerivativeAsync(AndoEconDerivativeRequestModel request)
        {
            return await CallAPIAsync<AndoEconDerivativeRequestModel, AndoEconDerivativeResponseModel>(request, "Derivative");
        }

        public async Task<AndoEconPartialDerivativeResponseModel> PartialDerivativeAsync(AndoEconPartialDerivativeRequestModel request)
        {
            return await CallAPIAsync<AndoEconPartialDerivativeRequestModel, AndoEconPartialDerivativeResponseModel>(request, "PartialDerivative");
        }

        public async Task<AndoEconEvaluateResponseModel> EvaluateAsync(AndoEconEvaluateRequestModel request)
        {
            return await CallAPIAsync<AndoEconEvaluateRequestModel, AndoEconEvaluateResponseModel>(request, "Evaluate");
        }

        public async Task<AndoEconSolveResponseModel> SolveAsync(AndoEconSolveRequestModel request)
        {
            return await CallAPIAsync<AndoEconSolveRequestModel, AndoEconSolveResponseModel>(request, "Solve");
        }

        public async Task<AndoEconMaximumRevenueResponseModel> MaximumRevenueAsync(AndoEconMaximumRevenueRequestModel request)
        {
            return await CallAPIAsync<AndoEconMaximumRevenueRequestModel, AndoEconMaximumRevenueResponseModel>(request, "MaximumRevenue");
        }

        public async Task<AndoEconMaximumProfitResponseModel> MaximumProfitAsync(AndoEconMaximumProfitRequestModel request)
        {
            return await CallAPIAsync<AndoEconMaximumProfitRequestModel, AndoEconMaximumProfitResponseModel>(request, "MaximumProfit");
        }

        public async Task<TResponse> CallAPIAsync<TRequest, TResponse>(TRequest request, string api)
        {
            TResponse response = default(TResponse);

            HttpResponseMessage responsemessage = await _client.PostAsJsonAsync(
                $"api/{api}", request);

            //response.EnsureSuccessStatusCode();

            if (responsemessage.IsSuccessStatusCode)
            {
                string json = await responsemessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<TResponse>(json);
            }

            // return URI of the created resource.
            return response;
        }
    }
}
