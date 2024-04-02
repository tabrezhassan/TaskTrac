namespace TaskTrac.API.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(string email,IList<string> roles);
    }
}
