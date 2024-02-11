namespace Trading.MlClient.Resources.Models;

public interface IModelResource<TFeature> : IMlClientResource where TFeature : Enum 
{
    Task<bool> PredictAsync(IEnumerable<(TFeature Type, decimal Value)> features);
}