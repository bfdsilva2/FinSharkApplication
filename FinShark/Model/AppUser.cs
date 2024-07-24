using FinShark.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace FinShark.Model
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
        public AppUser()
        {
           
        }
        public AppUser(RegisterDto registerDto)
        {
            UserName = registerDto.Username;
            Email = registerDto.Email;
        }
    }
}
