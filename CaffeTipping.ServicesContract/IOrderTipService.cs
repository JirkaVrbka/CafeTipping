using CaffeTipping.ServicesContract.Dtos;

namespace CaffeTipping.ServicesContract;

public interface IOrderTipService
{
    Task<OrderTipDto> GetOrCreateOrderTip(Guid tableId);
    Task CreateOrderTip(OrderTipDto orderTipDto);
    Task CreateOrUpdate(OrderTipDto orderTipDto);
    
    Task<List<OrderTipDto>> GetAllOrderTips();
    
    Task<OrderTipDto?> GetOrderTip(Guid? orderTipId);
    
}