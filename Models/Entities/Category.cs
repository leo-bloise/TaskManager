using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.Entities;

[Table("categories")]
public class Category : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}