using Jwt_implementation.Dto;
using Jwt_implementation.Models;
using Jwt_implementation.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Jwt_implementation.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductResponseDto> Create(ProductCreateDto productCreateDto)
        {
            var product = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                price = productCreateDto.price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductResponseDto
            {
                Name = product.Name,
                Description = product.Description,
                price = product.price
            };
        }

        public async  Task<string> Delete(int id)
        {
           var product = _context.Products.Find(id);
            if (product == null)
            {
                return "Product not found";
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return "Product deleted successfully";
        }

        public async Task<List<ProductResponseDto>> GetAll()
        {
            return await _context.Products.Select(p => new ProductResponseDto
            {
                Name = p.Name,
                Description = p.Description,
                price = p.price

            }).ToListAsync();

        }

        public async Task<ProductResponseDto> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            return new ProductResponseDto
            {
                Name = product.Name,
                Description = product.Description,
                price = product.price
            };
        }

        public async Task<ProductResponseDto> Update(ProductUpdateDto productUpdateDto)
        {
            var product = await _context.Products.FindAsync(productUpdateDto.Id);
            if (product == null)
            {
                return null;
            }

            product.Name = productUpdateDto.Name;
            product.Description = productUpdateDto.Description;
            product.price = productUpdateDto.price;

            await _context.SaveChangesAsync();

            return new ProductResponseDto
            {
                Name = product.Name,
                Description = product.Description,
                price = product.price
            };
        }
    }
}
