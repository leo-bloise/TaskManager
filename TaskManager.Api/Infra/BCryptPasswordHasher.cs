using Microsoft.AspNetCore.Identity;
using TaskManager.Domain;

namespace TaskManager.Api.Infra
{
    public class BCryptPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (!BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword))
            {
                return PasswordVerificationResult.Failed;
            }
            return PasswordVerificationResult.Success;
        }
    }
}