using RestSharp;

namespace Trading.MlClient.Resources;

public abstract class MlClientResource : IMlClientResource
{
    private static string ToSnakeCase(string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
    
    
    private readonly RestClient _client;
    private readonly string _resource;

    protected MlClientResource(string host, string resource)
    {
        _client = new RestClient(host);
        _resource = resource ?? throw new ArgumentException(nameof(resource));
    }

    protected async Task<TResponse> GetAsync<TResponse>(string subResource, IEnumerable<QueryParameter> parameters)
    {
        var request = string.IsNullOrWhiteSpace(subResource) 
            ? new RestRequest(_resource) 
            : new RestRequest($"{_resource}/{subResource}");

        foreach (var parameter in parameters)
        {
            var innerParameter = new QueryParameter(ToSnakeCase(parameter.Name), parameter.Value.ToString());
            request.AddParameter(innerParameter);
        }

        var response = await _client.ExecuteAsync<TResponse>(request);

        if (!response.IsSuccessful)
            throw new Exception(response.ErrorMessage);

        return response.Data;
    }

    protected async Task<TResponse> GetAsync<TResponse>(IEnumerable<QueryParameter> parameters)
    {
        return await GetAsync<TResponse>(string.Empty, parameters);
    }
}