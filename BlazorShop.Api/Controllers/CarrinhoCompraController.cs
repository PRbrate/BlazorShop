using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories;
using BlazorShop.Api.Repositories.Intefaces;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoCompraController : ControllerBase
    {
        private readonly IcarrinhoCompraRepository _icarrinhoCompraRepository;
        private readonly IprodutoRepository _iprodutoRepository;

        private ILogger<CarrinhoCompraController> logger;

        public CarrinhoCompraController(IcarrinhoCompraRepository icarrinhoCompraRepository,
            IprodutoRepository iprodutoRepository, ILogger<CarrinhoCompraController> logger)
        {
            _icarrinhoCompraRepository = icarrinhoCompraRepository;
            _iprodutoRepository = iprodutoRepository;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{usuarioId:int}/GetItens")]

        public async Task<ActionResult<IEnumerable<CarrinhoItemDto>>> GetItens(int id)
        {
            try
            {
                var carrinhoItens = await _icarrinhoCompraRepository.GetItems(id);
                if(carrinhoItens is null)
                { 
                    return NoContent(); 
                }

                var produtos = await _iprodutoRepository.GetItens();
                if(produtos is null)
                {
                    throw new Exception("Não existem produtos...");
                }

                var carrinhoItensDto = carrinhoItens.ConverterCarrinhoItensParaDto(produtos);
                return Ok(carrinhoItensDto);
            }
            catch (Exception ex)
            {
                logger.LogError("## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarrinhoItemDto>> GetItem(int id)
        {
            try
            {
                var carrinhoItens = await _icarrinhoCompraRepository.GetItem(id);

                if (carrinhoItens is null)
                {
                    return NotFound("Item não encontrado");
                }

                var produtos = await _iprodutoRepository.GetItem(carrinhoItens.ProdutoId);
                if (produtos is null)
                {
                    throw new Exception("Item não existe na fonte de dados");
                }

                var carrinhoItensDto = carrinhoItens.ConverterCarrinhoItensParaDto(produtos);
                return Ok(carrinhoItensDto) ;

            }
            catch (Exception e) 
            {
                logger.LogError($"## Erro  ao obter o item = {id} do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError , e.Message);
            } 
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoItemDto>> PostItem([FromBody] CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
        {
            try
            {
                var carrinhoItem = await _icarrinhoCompraRepository.AdicionaItem(carrinhoItemAdicionaDto);

                if(carrinhoItem is null)
                {
                    return NoContent();
                }

                var produto = await _iprodutoRepository.GetItem(carrinhoItem.ProdutoId);

                if (produto is null) 
                {
                    throw new Exception($"Produto  não localizado (Id: ({carrinhoItem.ProdutoId})");
                }

                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItensParaDto(produto);
                return CreatedAtAction(nameof(GetItem), new { id = carrinhoItemDto.Id }, carrinhoItemDto);
            }
            catch (Exception e) 
            {
                logger.LogError("## Erro ao criar um novo item no carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
