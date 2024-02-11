using System.Diagnostics;
using RestSharp;

namespace Trading.MlClient.Resources.Models;

public abstract class ModelResource<TFeature> : MlClientResource, IModelResource<TFeature> where TFeature : Enum
{
    protected ModelResource(string host, string modelName) : base(host, modelName)
    {
    }
    
    public async Task<bool> PredictAsync(IEnumerable<(TFeature Type, decimal Value)> features)
    {
        var featuresAsQueryParameters = features
            .Select(x => new QueryParameter(x.Type.ToString(), 
                x.Value.ToString()));
        
        var response = await GetAsync<ModelResponse>("prediction", featuresAsQueryParameters);
        return response.PredictedMark;
    }
}