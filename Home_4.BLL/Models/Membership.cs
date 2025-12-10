using System.ComponentModel.DataAnnotations;
using Home_4.BLL.Enums;

namespace Home_4.BLL.Models;

public class Membership : EntityBase
{
    public int MemberId { get; set; }
    public required Member Member { get; set; }
    public MembershipStatusEnum StatusEnum { get; set; } = MembershipStatusEnum.Active;
    public DateOnly StartsOn { get; set; }
    public DateOnly EndsOn { get; set; }
    public required string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime? FrozenAtUtc { get; set; }
}