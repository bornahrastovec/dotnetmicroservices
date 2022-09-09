﻿using Catalog.Api.Data.Context;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ICatalogContext _context;
		public ProductRepository(ICatalogContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task CreateProduct(Product product)
		{
			await _context.Products.InsertOneAsync(product);
		}

		public async Task<bool> DeleteProduct(string id)
		{
			FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

			DeleteResult result = await _context.Products.DeleteOneAsync(filter);

			return result.IsAcknowledged && result.DeletedCount > 0;

		}

		public async Task<Product> GetProduct(string id)
		{
			return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			return await _context.Products.Find(x => true).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
		{
			FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

			return await _context.Products.Find(filter).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductsByName(string name)
		{
			FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

			return await _context.Products.Find(filter).ToListAsync();
		}

		public async Task<bool> UpdateProduct(Product product)
		{
			var result = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

			return result.IsAcknowledged && result.ModifiedCount > 0;
		}
	}
}
