namespace CleanArchitecture.Blazor.Application.Features.Vehicles.Specifications;

#nullable disable warnings
/// <summary>
/// Specifies the different views available for the Vehicle list.
/// </summary>
public enum VehicleListView
{
    [Description("All")]
    All,
    [Description("My")]
    My,
    [Description("Created Toady")]
    CreatedToday,
    [Description("Created within the last 30 days")]
    Created30Days
}
/// <summary>
/// A class for applying advanced filtering options to Vehicle lists.
/// </summary>
public class VehicleAdvancedFilter: PaginationFilter
{
    public VehicleListView ListView { get; set; } = VehicleListView.All;
    public UserProfile? CurrentUser { get; set; }
}