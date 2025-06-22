using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.Entities;

[Table("tasks")]
public class Task : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("category_id")]
    public long? CategoryId { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
}