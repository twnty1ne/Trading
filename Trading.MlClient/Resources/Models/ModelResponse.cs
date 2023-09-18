using Newtonsoft.Json;

namespace Trading.MlClient.Resources.Models;

public class ModelResponse
{
    [JsonProperty("predicted_mark")]
    public bool PredictedMark { get; set; }
}