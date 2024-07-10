using FinShark.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace FinShark.Model
{
    public class AppUser : IdentityUser
    {
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
