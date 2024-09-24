using WepApiCRUDHexagonal.Application.Interfaces;
using WepApiCRUDHexagonal.Infraestructure;
using Microsoft.EntityFrameworkCore;
using WepApiCRUDHexagonal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WepApiCRUDHexagonal.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {

            return await _context.Products.ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<Product> GetProductByID(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        [HttpPost]
        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        [HttpPut("{id:int}")]
        public async Task UpdateProduct(int id, Product UpdatedProduct)
        {
            var productoExistente = await _context.Products.FindAsync(id);
            if (productoExistente != null)
            {
                productoExistente.Name = UpdatedProduct.Name;
                productoExistente.Price = UpdatedProduct.Price;
                await _context.SaveChangesAsync();
            }


        }

        [HttpDelete("{id:int}")]
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

    }
}
