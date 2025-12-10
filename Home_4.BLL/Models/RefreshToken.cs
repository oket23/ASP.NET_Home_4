using System.ComponentModel.DataAnnotations;

namespace Home_4.BLL.Models;

public class RefreshToken : EntityBase
{
    public int MemberId { get; set; }
    public required Member Member { get; set; }
    public required string TokenHash { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime? RevokedAtUtc { get; set; }
    public string? RevokeReason { get; set; }
    public int? ReplacedByTokenId { get; set; }
    public RefreshToken? ReplacedByToken { get; set; }
}