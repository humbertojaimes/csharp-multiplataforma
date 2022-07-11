using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SpeakersApiClient;

public class ApiClient<T>
    where T : class
{
    private readonly string _uri;
    private readonly ILogger _logger;

    public ApiClient(ILogger logger, string uri)
    {
        _uri = uri;
        _logger = logger;
    }

    public async Task<RestResponse> Get()
        
    {
        RestResponse restResponse = new()
        {
            Data = default,
            Success = false
        };

        try
        {
            using HttpClient httpClient = new();
            _logger?.LogInformation("Sending request to {uri}", _uri);
            var json = await httpClient.GetStringAsync(_uri);
            if (json is not null)
            {
                _logger?.LogInformation("Json response {json}", json);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                restResponse.Data = JsonSerializer.Deserialize<IEnumerable<T>>(json, options);
                restResponse.Success = true;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,"Error in request");
            restResponse.Success = false;
        }

        return restResponse;
    }


    public async Task<T> GetById(string id)

    {
        T? response = default;

        try
        {
            using HttpClient httpClient = new();
            _logger?.LogInformation("Sending request to {uri}", $"{_uri}/{id}");
            var json = await httpClient.GetStringAsync($"{_uri}/{id}");
            if (json is not null)
            {
                _logger?.LogInformation("Json response {json}", json);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                response = JsonSerializer.Deserialize<T?>(json,options)!;
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Error in request");
        }

        return response!;
    }

    public class RestResponse
    {

        public IEnumerable<T>? Data { get; set; }

        public bool Success { get; set; }

    }
}

