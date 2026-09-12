using Domain.Entities;


namespace Application.Interfaces.Repositories;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}