using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ganss.Excel;

namespace Trading.Api.Services;

public class ExcelService : IExcelService
{
    private readonly ExcelMapper _excelMapper = new();
    private readonly string _runtimeFilesDirectoryPath = Path.Combine("data", "runtime_files");
    public async Task SaveAsync<T>(string fileName, IEnumerable<T> items) 
        where T : class
    {
        var filePath = GetFilePath(fileName);
            
        await _excelMapper.SaveAsync(filePath, items, "position_variances");
    }

    public IEnumerable<T> Read<T>(string fileName) 
        where T : class
    {
        var filePath = GetFilePath(fileName);
        return _excelMapper.Fetch<T>(filePath);
    }

    private string GetFilePath(string fileName)
    {
        return Path.Combine(_runtimeFilesDirectoryPath, fileName);
        ;
    }
}