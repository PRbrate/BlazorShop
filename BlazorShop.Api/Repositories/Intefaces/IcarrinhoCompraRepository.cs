using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Api.Repositories;

public interface IcarrinhoCompraRepository
{
    Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
    Task<CarrinhoItem> AtualizaQuantidade(int id , CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualiza);
    Task<CarrinhoItem> DeletaItem(int id);
    Task<CarrinhoItem> GetItem(int id);
    Task<IEnumerable<CarrinhoItem>> GetItems(int usuario);

}
