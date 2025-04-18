using CarBazaar.Models;

namespace CarBazaar.Interface
{
    public interface ITokenService
    {
         string CreateToken(User user);
    }
}
