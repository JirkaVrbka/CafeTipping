using CaffeTipping.ServicesContract;
using CaffeTipping.ServicesContract.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaffeTipping.WebApi.Controllers;

[ApiController]

[Route("api/[controller]")]
public class DownloadController(IOrderTipService orderTipService)
{
    [HttpGet("download")]
    [Authorize]
    public async Task<ActionResult<List<OrderTipDto>>> Download()
    {
        var all = await orderTipService.GetAllOrderTips();
        return new OkObjectResult(all);
    }
}