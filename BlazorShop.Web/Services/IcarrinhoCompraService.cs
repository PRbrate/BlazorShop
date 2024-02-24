using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IcarrinhoCompraService
    {
        Task<List<CarrinhoItemDto>> GetItem(int usuario);
        Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto item);
    }
}
