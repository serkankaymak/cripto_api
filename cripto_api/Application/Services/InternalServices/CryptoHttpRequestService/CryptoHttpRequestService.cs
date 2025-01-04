using Application.Events;
using Application.Settings;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.ApiResponse;
using Shared.Events;
using Shared.LogCat;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Application.Services.InternalServices.CryptoHttpRequestService;
public class CryptoHttpRequestService(HttpClient httpClient, IMapper mapper, IEventDispatcher eventDispatcher) : ICryptoHttpRequestService
{
    private readonly IMapper mapper = mapper;
    private readonly IEventDispatcher eventDispatcher = eventDispatcher;

    private readonly HttpClient client = httpClient;

    private Task<List<CryptoData>> parse(string responseContent)
    {
        List<CryptoData>? cryptoDataList = JsonConvert.DeserializeObject<List<CryptoData>>(responseContent);
        if (cryptoDataList == null) throw new Exception("Veri parse edilir iken bir sorun yaşandı.");
        return Task.FromResult(cryptoDataList);

    }

    private async Task<HttpResponseMessage> request(string url, string secret)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Accept.Clear();
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add("x-cg-pro-api-key", secret);
        request.Headers.UserAgent.ParseAdd("CyriptoApp/1.0");
        HttpResponseMessage response = await client.SendAsync(request);
        return response;
    }

    public async Task<List<CryptoData>> GetCryptoDataAsync(ApiEndpointConfig apiEndPoint)
    {
        LogCat.Debug(" GetCryptoDataAsync(ApiEndpoint setting)");
        //https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&?x_cg_demo_api_key=CG-kLDwuFqeuEr2qVB7C7EdMAgz
        string url = apiEndPoint.Host + apiEndPoint.Route + apiEndPoint.Query + apiEndPoint.TargetCurrency;
        var response = await request(url, apiEndPoint.Secret);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"{response.StatusCode} : {await response.Content.ReadAsStringAsync()}");
            throw ExceptionFactory.InternalServerError($"response doğru gelmedi : {content}");
        }
        var data = await parse(content);
        // var res =  data.Select(x => mapper.Map<CryptoData, Ticker>(x)).ToList();
        return data;

    }
}