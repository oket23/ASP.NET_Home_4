using Home_4.BLL.Filters;
using Home_4.BLL.Models;

namespace Home_4.BLL.Interfaces.Repositories;

public interface IMemberRepository
{
    Task<ResponseWrapper<Member>> GetAllAsync(GetAllMembersFilter? filter);
    Task<Member?> GetByIdAsync(int id);
    Task<Member> CreateAsync(Member member);
    Task<Member?> UpdateAsync(Member member);
    Task<Member?> DeleteAsync(int memberId);
    
    Task<Member?> DeactivateAsync(int id, string? reason);
    Task<bool> EmailExistsAsync(string email);
    Task<Member?> GetByEmailAsync(string email);
}