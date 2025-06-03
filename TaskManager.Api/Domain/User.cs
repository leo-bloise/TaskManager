using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Domain
{
    [
        Index(nameof(Email), IsUnique = true),
        Index(nameof(Password), IsUnique = true)
    ]
    public class User : BaseEntity
    {
        [Column("username")]
        [NotNull]
        public string Username { get; set; }
        [Column("password")]
        [NotNull]
        public string Password { get; set; }
        [Column("email")]
        [NotNull]
        public string Email { get; set; }     
        public User() {}
        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}