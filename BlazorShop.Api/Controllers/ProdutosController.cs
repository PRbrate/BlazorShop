using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories.Intefaces;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IprodutoRepository _produtoRepository;

        public ProdutosController(IprodutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItems()
        {
            try
            {
                var produtos = await _produtoRepository.GetItens();
                if(produtos is null)
                {
                    return NotFound();
                }
                else
                {
                    var produtosDtos = produtos.ConverterProdutoParaDto();
                    return Ok(produtosDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a base de dados");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDto>> GetItem(int id)
        {
            try
            {
                var produtos = await _produtoRepository.GetItem(id);
                if (produtos is null)
                {
                    return NotFound("Produto Não Localizado");
                }
                else
                {
                    var produtosDtos = produtos.ConverterProdutoParaDto();
                    return Ok(produtosDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a base de dados");
            }
        }

        [HttpGet]
        [Route("GetItensPorCategoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItensPorCategoria(int categoriaId)
        {
            try
            {
                var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);
                var produtosDtos = produtos.ConverterProdutoParaDto();
                return Ok(produtosDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar a base de dados");
            }
        }
    }
}
