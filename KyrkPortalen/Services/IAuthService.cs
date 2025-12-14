using KyrkPortalen.Domain.DTOs;

namespace KyrkPortalen.Services
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterDTO dto);
        Task<string?> LoginAsync(LoginDTO dto);
    }
}
