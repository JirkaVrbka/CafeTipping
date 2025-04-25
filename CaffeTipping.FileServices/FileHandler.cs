using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CaffeTipping.FileServices;

public abstract class GenericFileService<T> where T : class, new()
{
    protected abstract string FileName { get; }
    private readonly string _filePath;
    private readonly ILogger<GenericFileService<T>> _logger;

    protected GenericFileService(ILogger<GenericFileService<T>> logger)
    {
        _logger = logger;
        _filePath =  Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, FileName);
    }


    protected async Task<List<T>> GetFromFile()
    {
        try
        {
            using StreamReader reader = new(_filePath);
            var text = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<T>>(text) ?? [];
        }
        catch (IOException)
        {
            _logger.LogWarning("Unable to read file at: {Path}", _filePath);
        }
        
        return [];
    }

    protected async Task WriteToFile(List<T> orderTipDtos)
    {
        try
        {
            await using StreamWriter writer = new(_filePath);
            var text = JsonConvert.SerializeObject(orderTipDtos);
            await writer.WriteAsync(text);
        }
        catch (IOException)
        {
            _logger.LogWarning("Unable to write to file at: {Path}", _filePath);
        }
    } 
}