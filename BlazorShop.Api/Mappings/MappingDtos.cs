using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Api.Mappings
{
    public static class MappingDtos
    {
        public static IEnumerable<CategoriaDto> ConverterCategoriasParaDto
            (this IEnumerable<Categoria> categorias)
        {

            return (from categoria in categorias
                    select new CategoriaDto
                    {
                        Id = categoria.Id,
                        Nome = categoria.Nome,
                        IconCSS = categoria.IconCSS

                    }).ToList();
        }

        public static IEnumerable<ProdutoDto> ConverterProdutoParaDto
            (this IEnumerable<Produto> produtos)
        {
            return (from produto in produtos
                    select new ProdutoDto
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        ImagemUrl = produto.ImagemUrl,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.Categoria.Id,
                        CategoriaNome = produto.Categoria.Nome
                    }).ToList();       
        }

        public static ProdutoDto ConverterProdutoParaDto(this Produto produto)
        {
            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                ImagemUrl = produto.ImagemUrl,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                CategoriaId = produto.Categoria.Id,
                CategoriaNome = produto.Categoria.Nome
            };
        }

        public static IEnumerable<CarrinhoItemDto> ConverterCarrinhoItensParaDto
            (this IEnumerable<CarrinhoItem> carrinhoItens, IEnumerable<Produto> produtos)
        {
            return (from carrinhoItem in carrinhoItens
                    join produto in produtos
                    on carrinhoItem.ProdutoId equals produto.Id
                    select new CarrinhoItemDto
                    {
                        Id = carrinhoItem.Id,
                        ProdutoId = produto.Id,
                        ProdutoNome = produto.Nome,
                        ProdutoDescricao = produto.Descricao,
                        ProdutoImagemURL = produto.ImagemUrl,
                        Preco = produto.Preco,
                        CarrinhoId = carrinhoItem.CarrinhoId,
                        Quantidade = carrinhoItem.Quantidade,
                        PrecoTotal = produto.Preco * carrinhoItem.Quantidade

                    }).ToList();
        }

        public static CarrinhoItemDto ConverterCarrinhoItensParaDto
            (this CarrinhoItem carrinhoItens, Produto produtos)
        {
            return new CarrinhoItemDto
            {
                Id = carrinhoItens.Id,
                ProdutoId = produtos.Id,
                ProdutoNome = produtos.Nome,
                ProdutoDescricao = produtos.Descricao,
                ProdutoImagemURL = produtos.ImagemUrl,
                Preco = produtos.Preco,
                CarrinhoId = carrinhoItens.CarrinhoId,
                Quantidade = carrinhoItens.Quantidade,
                PrecoTotal = produtos.Preco * carrinhoItens.Quantidade

            };
        }
    }
}
