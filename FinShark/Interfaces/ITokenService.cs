using FinShark.Model;

namespace FinShark.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
