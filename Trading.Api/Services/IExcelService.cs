using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trading.Api.Services;

public interface IExcelService
{
    Task SaveAsync<T>(string fileName, IEnumerable<T> items) where T : class;
    IEnumerable<T> Read<T>(string fileName) where T : class;
}