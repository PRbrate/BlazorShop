using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public class GerenciaProdutosLocalStorageService : IGerenciaProdutosLocalStorageService
    {
        private const string key = "ProducoCollection";

        private readonly ILocalStorageService _storageService;
        private readonly IProdutoService _produtoService;

        public GerenciaProdutosLocalStorageService(ILocalStorageService storageService, IProdutoService produtoService)
        {
            _storageService = storageService;
            _produtoService = produtoService;
        }

        public async Task<IEnumerable<ProdutoDto>> GetCollection()
        {
            return await _storageService.GetItemAsync<IEnumerable<ProdutoDto>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _storageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProdutoDto>> AddCollection() 
        {
            var produtoCollection = await _produtoService.GetItens();

            if(produtoCollection != null)
            {
                await _storageService.SetItemAsync(key, produtoCollection); 
            }
            return produtoCollection;
        }
    }
}
