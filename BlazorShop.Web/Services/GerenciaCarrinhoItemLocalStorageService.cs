using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public class GerenciaCarrinhoItemLocalStorageService : IGerenciaCarrinhoItensLocalStorageService
    {
        const string key = "CarrinhoItemCollection";

        private readonly ILocalStorageService _storageService;
        private readonly IcarrinhoCompraService _carrinhoCompraService;

        public GerenciaCarrinhoItemLocalStorageService(ILocalStorageService storageService, IcarrinhoCompraService carrinhoCompraService)
        {
            _storageService = storageService;
            _carrinhoCompraService = carrinhoCompraService;
        }

        public async Task<List<CarrinhoItemDto>> GetCollection()
        {
            return await _storageService.GetItemAsync<List<CarrinhoItemDto>>(key)?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _storageService.RemoveItemAsync(key);
        }

        public async Task Savecollection(List<CarrinhoItemDto> carrinhoItemDtos)
        {
            await _storageService.SetItemAsync(key, carrinhoItemDtos);
        }

        private async Task<List<CarrinhoItemDto>> AddCollection()
        {
            var carrinhoCompraCollection = await _carrinhoCompraService.GetItem(UsuarioLogado.UsuarioId);

            if( carrinhoCompraCollection != null)
            {
                await _storageService.SetItemAsync(key, carrinhoCompraCollection);
            }
            return carrinhoCompraCollection;
        }
    }
}
