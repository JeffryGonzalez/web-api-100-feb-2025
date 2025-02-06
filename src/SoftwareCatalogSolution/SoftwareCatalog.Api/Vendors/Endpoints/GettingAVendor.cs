
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints;

public static class GettingAVendor
{

  
    public static  async Task<Results<Ok<VendorDetailsResponseModel>, NotFound>> GetVendorAsync(Guid id, IDocumentSession session)
    {
        var response = await session.Query<VendorEntity>()
            .Where(v => v.Id == id)
            .ProjectToModel()
            .SingleOrDefaultAsync();

        return response switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(response)
        };
    }

}