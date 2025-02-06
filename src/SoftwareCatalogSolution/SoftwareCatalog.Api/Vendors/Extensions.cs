using FluentValidation;
using SoftwareCatalog.Api.Vendors.Endpoints;

namespace SoftwareCatalog.Api.Vendors;

public static class Extensions
{
    public static IServiceCollection AddVendors(this IServiceCollection services)
    {

        services.AddScoped<IValidator<VendorCreateModel>, UpdatedVendorCreateModelValidator>();
        return services;
    }

    public static IEndpointRouteBuilder MapVendors(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("vendors").WithTags("Approved Vendors").WithDescription("The Approved Vendors for the Company");

        group.MapPost("/", AddingAVendor.CanAddVendorAsync);
        group.MapGet("/{id:guid}", GettingAVendor.GetVendorAsync).WithTags("Approved Vendors", "Catalog");
        return group;
       
    }
}
