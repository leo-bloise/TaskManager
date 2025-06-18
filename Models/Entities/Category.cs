using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models.Entities;

[Table("categories")]
public class Category : BaseEntity
{
    [Column("name")]
    public string Name { get; set; }
    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}