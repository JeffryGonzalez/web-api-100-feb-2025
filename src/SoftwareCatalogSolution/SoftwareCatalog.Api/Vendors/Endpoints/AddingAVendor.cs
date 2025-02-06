using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;
public static class AddingAVendor
{
  
    public static async Task<Results<Created<VendorDetailsResponseModel>, BadRequest>> CanAddVendorAsync(
        [FromBody] VendorCreateModel request,
        [FromServices] IValidator<VendorCreateModel> validator,
        [FromServices] IDocumentSession session
        )
    {
        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return TypedResults.BadRequest();
        }
        var entity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Link = request.Link,
            CreatedOn = DateTimeOffset.UnixEpoch
        };
        session.Store(entity);
        await session.SaveChangesAsync();
        var response = entity.MapToModel();
        return TypedResults.Created("some linky", response);
    }
}