using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Intefaces
{
    public interface IprodutoRepository
    {
        Task<IEnumerable<Produto>> GetItens();    
        Task<Produto> GetItem(int id);
        Task<IEnumerable<Produto>> GetItensPorCategoria(int id);
        Task<IEnumerable<Categoria>> GetCategorias();
    }
}
