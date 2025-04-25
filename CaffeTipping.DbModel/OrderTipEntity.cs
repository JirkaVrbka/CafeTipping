using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CaffeTipping.DbModel;

[Index(nameof(TableId), IsUnique = true)]
public class OrderTipEntity : DbEntity
{
    [Required]
    public Guid TableId { get; set; }
    
    [Required]
    [MinLength(5)]
    public string Email { get; set; } = "";
    
    [Required]
    public double Bill { get; set; }
    
    [Required]
    public double Tip { get; set; }
    public short Rating { get; set; } 
    
    [Required]
    public DateTime Date { get; set; }
}