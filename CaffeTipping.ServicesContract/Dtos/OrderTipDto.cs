namespace CaffeTipping.ServicesContract.Dtos;

public class OrderTipDto
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public string Email { get; set; } = "";
    public double Bill { get; set; }
    public double Tip { get; set; } = -1;
    public short Rating { get; set; } = -1;
    
    public DateTime Date { get; set; } = DateTime.Now;
    
    public static OrderTipDto GetEmpty(Guid tableId)
        => new()
    {
        Id = Guid.Empty,
        TableId = tableId,
        Bill = new Random().Next(20, 2000)
    };

}