using TaskTrac.DAL.Models;

namespace TaskTrac.API.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(Users users,IList<string> roles);
    }
}
