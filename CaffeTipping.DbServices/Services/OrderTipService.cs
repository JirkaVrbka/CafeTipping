using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbServices.Mappers;
using CaffeTipping.ServicesContract;
using CaffeTipping.ServicesContract.Dtos;
using CaffeTipping.ServicesContract.Exceptions;

namespace CaffeTipping.DbServices.Services;

public class OrderTipService(IOrderTipRepository orderTipRepository) : IOrderTipService
{
    private readonly IOrderTipRepository _orderTipRepository = orderTipRepository;
    private readonly Random _random = new Random();

    public async Task<OrderTipDto> GetOrCreateOrderTip(Guid tableId)
    {
        var order = await _orderTipRepository.GetAsync(o => o.TableId.Equals(tableId));
        
        return order?.ToDto() ?? OrderTipDto.GetEmpty(tableId);
    }

    public async Task CreateOrderTip(OrderTipDto orderTipDto)
    {
        var order = await _orderTipRepository.GetAsync(o => o.TableId.Equals(orderTipDto.TableId));

        if (order is not null)
            throw new DuplicateTableIdException();

        await _orderTipRepository.AddAsync(orderTipDto.ToEntity());
    }

    public async Task CreateOrUpdate(OrderTipDto orderTipDto)
    {
        // TODO validate tip over zero
        if (orderTipDto.Id != Guid.Empty)
            await _orderTipRepository.UpdateAsync(orderTipDto.ToEntity());
        else
            await _orderTipRepository.AddAsync(orderTipDto.ToEntity());
    }

    public async Task<List<OrderTipDto>> GetAllOrderTips()
    {
        return (await _orderTipRepository.GetAllAsync()).Select(o => o.ToDto()).ToList();
    }

    public async Task<OrderTipDto?> GetOrderTip(Guid? orderTipId)
    {
        if (orderTipId is null)
            return null;
        
        return ( await _orderTipRepository.GetAsync(orderTipId.Value))?.ToDto();
    }
}