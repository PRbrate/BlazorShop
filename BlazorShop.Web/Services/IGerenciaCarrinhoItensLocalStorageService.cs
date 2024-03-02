using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IGerenciaCarrinhoItensLocalStorageService
    {
        Task<List<CarrinhoItemDto>> GetCollection();
        Task Savecollection (List<CarrinhoItemDto> carrinhoItemDtos);
        Task RemoveCollection();
    }
}
