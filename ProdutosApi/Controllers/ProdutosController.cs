using Microsoft.AspNetCore.Mvc;
using ProdutosApi.Data;
using ProdutosApi.DTOs;
using ProdutosApi.Models;

namespace ProdutosApi.Controllers;

/// <summary>
/// Endpoints CRUD para o recurso Produto.
/// </summary>
[ApiController]
[Route("api/v1/produtos")]
[Produces("application/json")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Lista todos os produtos cadastrados.</summary>
    /// <response code="200">Retorna a lista de produtos.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Produto>> GetAll()
    {
        return Ok(_context.Produtos);
    }

    /// <summary>Busca um produto pelo seu identificador.</summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto is null)
        {
            return NotFound(new { mensagem = $"Produto com Id {id} não foi encontrado." });
        }

        return Ok(produto);
    }

    /// <summary>Cria um novo produto.</summary>
    /// <response code="201">Produto criado com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Produto> Create([FromBody] ProdutoRequest request)
    {
        var produto = new Produto
        {
            Id = _context.GetNextId(),
            Nome = request.Nome,
            Preco = request.Preco,
            Categoria = request.Categoria,
            Estoque = request.Estoque
        };

        _context.Produtos.Add(produto);

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    /// <summary>Atualiza um produto existente.</summary>
    /// <response code="200">Produto atualizado com sucesso.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="400">Dados de entrada inválidos.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Produto> Update(int id, [FromBody] ProdutoRequest request)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto is null)
        {
            return NotFound(new { mensagem = $"Produto com Id {id} não foi encontrado." });
        }

        produto.Nome = request.Nome;
        produto.Preco = request.Preco;
        produto.Categoria = request.Categoria;
        produto.Estoque = request.Estoque;

        return Ok(produto);
    }

    /// <summary>Remove um produto pelo seu identificador.</summary>
    /// <response code="204">Produto removido com sucesso.</response>
    /// <response code="404">Produto não encontrado.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

        if (produto is null)
        {
            return NotFound(new { mensagem = $"Produto com Id {id} não foi encontrado." });
        }

        _context.Produtos.Remove(produto);

        return NoContent();
    }
}
