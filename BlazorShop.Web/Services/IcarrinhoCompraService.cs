using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IcarrinhoCompraService
    {
        Task<List<CarrinhoItemDto>> GetItem(int usuario);
        Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto item);

        Task<CarrinhoItemDto> DeletaItem(int id);

        Task<CarrinhoItemDto> AtualizaQuantidade(CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidade);

        event Action<int> OnCarrinhoItemChanged;

        void RaiseEventOnCarrinhoCarrinhoChanged(int totalQuantidade);
    }
}
