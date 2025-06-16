using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.Entities;

[Table("users")]
[Index("Username", IsUnique = true)]
[Index("Email", IsUnique = true)]
public class User : BaseEntity
{
    [Column("username")]
    public string Username { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
}