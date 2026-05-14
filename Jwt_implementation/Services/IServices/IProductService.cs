using Jwt_implementation.Dto;

namespace Jwt_implementation.Services.IServices
{
    public interface IProductService
    {
        Task<ProductResponseDto> Create(ProductCreateDto productCreateDto);
        Task<ProductResponseDto> Update(ProductUpdateDto productUpdateDto);
        Task<string> Delete(int id);
        Task<ProductResponseDto> GetById(int id);
        Task<List<ProductResponseDto>> GetAll();
    }
}
