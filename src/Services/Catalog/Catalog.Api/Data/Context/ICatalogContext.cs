using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data.Context
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
