using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories
{
    public class CarrinhoCompraRepository : IcarrinhoCompraRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarrinhoCompraRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private async Task<bool> CarrinhoItemJaExite(int carrinhoId, int produtoId) 
        { 
            return await _appDbContext.CarrinhoItens.AnyAsync(c=> c.CarrinhoId == carrinhoId && 
                                                              c.ProdutoId == produtoId);
        }

        public async Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
        {
            if(await CarrinhoItemJaExite(carrinhoItemAdicionaDto.CarrinhoId, 
                                         carrinhoItemAdicionaDto.ProdutoId) == false) 
            { 
                //verifica se o produto já existe
                //cria um novo item no carrinho
                var item = await(from produto in _appDbContext.Produtos
                                 where produto.Id == carrinhoItemAdicionaDto.ProdutoId
                                 select new CarrinhoItem
                                 { 
                                    CarrinhoId = carrinhoItemAdicionaDto.CarrinhoId,
                                    ProdutoId = produto.Id,
                                    Quantidade = carrinhoItemAdicionaDto.Quantidade
                                 }).SingleOrDefaultAsync();

                //Se  o item existe então incluir o item no carrinho
                if (item is not null)
                { 
                    var resultado = await _appDbContext.CarrinhoItens.AddAsync(item); 
                    await _appDbContext.SaveChangesAsync();
                    return resultado.Entity;
                }
            }
            return null;
        }

        public async Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualiza)
        {
            var carrinhoItem = await _appDbContext.CarrinhoItens.FindAsync(id);

            if(carrinhoItem is not null)
            {
                carrinhoItem.Quantidade = carrinhoItemAtualiza.Quantidade;
                await _appDbContext.SaveChangesAsync();
                return carrinhoItem;
            }
            return null;
        }

        public async Task<CarrinhoItem> DeletaItem(int id)
        {
            var item = await _appDbContext.CarrinhoItens.FindAsync(id);

            if(item is not null)
            {
                _appDbContext.CarrinhoItens.Remove(item);
                await _appDbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<CarrinhoItem> GetItem(int id)
        {
            return await (from carrinho in _appDbContext.Carrinhos
                          join carrinhoItem in _appDbContext.CarrinhoItens
                          on carrinho.Id equals carrinhoItem.CarrinhoId
                          where carrinhoItem.Id == id
                          select new CarrinhoItem
                          {
                              Id = carrinhoItem.Id,
                              ProdutoId = carrinhoItem.ProdutoId,
                              Quantidade = carrinhoItem.Quantidade,
                              CarrinhoId = carrinhoItem.CarrinhoId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CarrinhoItem>> GetItems(int usuario)
        {
            return await (from carrinho in _appDbContext.Carrinhos
                          join carrinhoIntem in _appDbContext.CarrinhoItens
                          on carrinho.Id equals carrinhoIntem.CarrinhoId
                          where carrinho.UsuarioId == usuario 
                          select new CarrinhoItem 
                          { 
                            Id = carrinhoIntem.Id,
                            ProdutoId = carrinhoIntem.ProdutoId,
                            Quantidade = carrinhoIntem.Quantidade,
                            CarrinhoId = carrinhoIntem.CarrinhoId,
                          }).ToListAsync();
        }
    }
}
