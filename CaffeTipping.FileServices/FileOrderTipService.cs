using CaffeTipping.ServicesContract;
using CaffeTipping.ServicesContract.Dtos;
using CaffeTipping.ServicesContract.Exceptions;
using Microsoft.Extensions.Logging;

namespace CaffeTipping.FileServices;

public class FileOrderTipService(ILogger<FileOrderTipService> logger) : GenericFileService<OrderTipDto>(logger), IOrderTipService
{
    private const string FileName = "OrderTip.json";
    protected override string FilePath { get; } = Directory.GetCurrentDirectory() + FileName;
    
    public async Task<OrderTipDto> GetOrCreateOrderTip(Guid tableId)
    {
        var order = (await GetFromFile()).FirstOrDefault(o => o.TableId.Equals(tableId));
        return order ?? OrderTipDto.GetEmpty(tableId);
    }

    public async Task CreateOrderTip(OrderTipDto orderTipDto)
    {
        var orders = await GetFromFile();
        var order = orders.FirstOrDefault(o => o.Id.Equals(orderTipDto.Id));

        if (order is not null)
            throw new DuplicateTableIdException();
        
        orderTipDto.Id = Guid.NewGuid();
        orders.Add(orderTipDto);
        await WriteToFile(orders);
    }

    public async Task CreateOrUpdate(OrderTipDto orderTipDto)
    {
        var orders = await GetFromFile();
        var order = orders.FirstOrDefault(o => o.Id.Equals(orderTipDto.Id));

        if (order is null)
        {
            orderTipDto.Id = Guid.NewGuid();
            orders.Add(orderTipDto);
        }
        else
        {
            orders[orders.IndexOf(order)] = orderTipDto;
        }
        
        await WriteToFile(orders);
    }

    public async Task<List<OrderTipDto>> GetAllOrderTips()
    {
        return await GetFromFile();
    }

    public async Task<OrderTipDto?> GetOrderTip(Guid? orderTipId)
    {
        if (orderTipId is null)
            return null;
        
        var orders = await GetFromFile();
        var order = orders.FirstOrDefault(o => o.Id.Equals(orderTipId.Value));
        return order;
    }


}