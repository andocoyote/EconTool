using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EconTool
{
    public class AndoEconAPIProvider
    {
        HttpClient client = new HttpClient();

        public AndoEconAPIProvider(string bearer)
        {
            client.BaseAddress = new Uri("https://andoeconapis.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer);
        }

        public async Task<AndoEconDifferentiateResponseModel> DifferentiateAsync(AndoEconDifferentiateRequestModel request)
        {
            return await CallAPIAsync<AndoEconDifferentiateRequestModel, AndoEconDifferentiateResponseModel>(request, "Differentiate");
        }

        public async Task<AndoEconMarginalUtilityResponseModel> MarginalUtilityAsync(AndoEconMarginalUtilityRequestModel request)
        {
            return await CallAPIAsync<AndoEconMarginalUtilityRequestModel, AndoEconMarginalUtilityResponseModel>(request, "MarginalUtility");
        }

        public async Task<AndoEconSolveResponseModel> SolveAsync(AndoEconSolveRequestModel request)
        {
            return await CallAPIAsync<AndoEconSolveRequestModel, AndoEconSolveResponseModel>(request, "Solve");
        }

        public async Task<TResponse> CallAPIAsync<TRequest, TResponse>(TRequest request, string api)
        {
            TResponse response = default(TResponse);

            HttpResponseMessage responsemessage = await client.PostAsJsonAsync(
                $"api/{api}", request);

            //response.EnsureSuccessStatusCode();

            if (responsemessage.IsSuccessStatusCode)
            {
                response = await responsemessage.Content.ReadFromJsonAsync<TResponse>();
            }

            // return URI of the created resource.
            return response;
        }
    }
}
