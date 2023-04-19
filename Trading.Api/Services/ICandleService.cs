using System.Threading.Tasks;

namespace Trading.Api.Services
{
    public interface ICandleService
    {
        Task LoadCandlesToFileAsync(CandlesLoadRequest request);
    }
}
