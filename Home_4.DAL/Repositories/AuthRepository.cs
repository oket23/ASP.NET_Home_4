using Home_4.BLL.Interfaces.Repositories;
using Home_4.BLL.Models;
using Home_4.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Home_4.DAL.Repositories;

public class AuthRepository : BaseRepository<RefreshToken>, IAuthRepository
{
    public AuthRepository(HomeContext context) : base(context)
    {
    }
    
    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash)
    {
        return await Set.FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllActiveByMemberIdAsync(int memberId)
    {
        return await Set.Where(x => 
                x.MemberId == memberId && 
                x.RevokedAtUtc == null && 
                x.ExpiresAtUtc > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task RevokeAllMemberTokensAsync(int memberId, string reason)
    {
        await Set.Where(x => x.MemberId == memberId && x.RevokedAtUtc == null)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.RevokedAtUtc, DateTime.UtcNow)
                .SetProperty(t => t.RevokeReason, reason));
    }

    public async Task DeleteExpiredTokensAsync()
    {
        await Set.Where(x => x.ExpiresAtUtc < DateTime.UtcNow)
            .ExecuteDeleteAsync();
    }
}