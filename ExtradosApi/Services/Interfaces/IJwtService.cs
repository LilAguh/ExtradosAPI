using DataAccess.Models;

namespace ExtradosApi.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userID, string userEmail);
    }
}
