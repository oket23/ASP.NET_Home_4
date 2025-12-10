namespace Home_4.BLL.Filters;

public class GetAllMembersFilter : PaginationFilter
{
    public string? Search { get; set; }
    public bool? HasActiveMembership { get; set; } 
    public bool? IsActive { get; set; } 
}