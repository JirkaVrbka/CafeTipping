using CaffeTipping.DbServices.Dtos;
using CaffeTipping.DbModel;

namespace CaffeTipping.DbServices.Mappers;

internal static class OrderTipMapper
{
    public static OrderTipDto ToDto(this OrderTipEntity orderTip)
        => new()
        {
            Id = orderTip.Id,
            TableId = orderTip.TableId,
            Email = orderTip.Email,
            Bill = orderTip.Bill,
            Rating = orderTip.Rating,
            Tip = orderTip.Tip,
            Date = orderTip.Date
        };

    public static OrderTipEntity ToEntity(this OrderTipDto orderTip)
        => new()
        {
            Id = orderTip.Id,
            TableId = orderTip.TableId,
            Email = orderTip.Email,
            Bill = orderTip.Bill,
            Rating = orderTip.Rating,
            Tip = orderTip.Tip,
            Date = orderTip.Date
        };
}