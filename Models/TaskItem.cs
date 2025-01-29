using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Models;

[Table("Task")]
public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(80)]
    [MinLength(3)]
    [Column("Title", TypeName = "NVARCHAR")]
    public required string Title { get; set; }
    
    [Required]
    [Column("Description", TypeName = "TITLE")]
    [MinLength(3)]
    [MaxLength(200)]
    public required string Description { get; set; }
    
    [Required]
    public bool Status { get; set; }
    
    public DateTime Date { get; set; }
}