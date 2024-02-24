using BlazorShop.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorShop.Web.Services
{
    public class CarrinhoCompraService : IcarrinhoCompraService
    {
        private readonly HttpClient _httpClient;

        public CarrinhoCompraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CarrinhoItemAdicionaDto>("api/CarrinhoCompra", item);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // retorna p valor "padrão" ou vazio para um objeto do tipo carrinhoItemDto
                        return default(CarrinhoItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
                }
                else
                { 
                    var mensage =  await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode} Mensage- {mensage}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CarrinhoItemDto>> GetItem(int usuario)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CarrinhoCompra/{usuario}/GetItens");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CarrinhoItemDto>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CarrinhoItemDto>>();
                }
                else
                {
                    var mensage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status code: {response.StatusCode} mensagem: {mensage}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
