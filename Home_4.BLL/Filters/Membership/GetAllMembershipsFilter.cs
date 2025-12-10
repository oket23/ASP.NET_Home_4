using Home_4.BLL.Enums;

namespace Home_4.BLL.Filters.Membership;

public class GetAllMembershipsFilter : PaginationFilter
{
    public string? Search { get; set; }
    public int? MemberId { get; set; }
    public MembershipStatusEnum? Status { get; set; }
    public DateOnly? ExpirationDateFrom { get; set; } 
    public DateOnly? ExpirationDateTo { get; set; }
}