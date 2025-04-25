namespace CaffeTipping.DbServices.Dtos;

public class OrderTipDto
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public string Email { get; set; } = "";
    public double Bill { get; set; }
    public double Tip { get; set; } = -1;
    public short Rating { get; set; } = -1;
    
    public DateTime Date { get; set; } = DateTime.Now;

}