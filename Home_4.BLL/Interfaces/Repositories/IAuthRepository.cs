using Home_4.BLL.Models;

namespace Home_4.BLL.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<RefreshToken> CreateAsync(RefreshToken token);
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);
    Task<RefreshToken?> UpdateAsync(RefreshToken token);
    Task<IEnumerable<RefreshToken>> GetAllActiveByMemberIdAsync(int memberId);
    Task RevokeAllMemberTokensAsync(int memberId, string reason);
    Task DeleteExpiredTokensAsync();
}