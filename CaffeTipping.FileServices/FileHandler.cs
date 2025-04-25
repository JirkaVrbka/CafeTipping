using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CaffeTipping.FileServices;

public abstract class GenericFileService<T>(ILogger<GenericFileService<T>> logger) where T : class, new()
{
    protected abstract string FilePath { get; }
    
    protected async Task<List<T>> GetFromFile()
    {
        try
        {
            using StreamReader reader = new(FilePath);
            var text = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<List<T>>(text) ?? [];
        }
        catch (IOException e)
        {
            logger.LogError(e, "An error occured while reading file");
        }
        
        return [];
    }

    protected async Task WriteToFile(List<T> orderTipDtos)
    {
        try
        {
            await using StreamWriter writer = new(FilePath);
            var text = JsonConvert.SerializeObject(orderTipDtos);
            await writer.WriteAsync(text);
        }
        catch (IOException e)
        {
            logger.LogError(e, "An error occured while writing to file");
        }
    } 
}