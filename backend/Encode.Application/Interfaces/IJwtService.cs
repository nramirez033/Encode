using Encode.Domain.Entities;

namespace Encode.Application.Interfaces
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}
