using System.ComponentModel.DataAnnotations;
using Home_4.BLL.Enums;

namespace Home_4.BLL.Models;

public class Member : EntityBase
{    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string Phone { get; set; }
    public UserRoleEnum RoleEnum { get; set; } = UserRoleEnum.Member;
    public bool IsActive { get; set; } = true;
    public string? DeactivateReason { get; set; }
    public List<Membership> Memberships { get; set; } = new();
    public List<RefreshToken> RefreshTokens { get; set; } = new();
}