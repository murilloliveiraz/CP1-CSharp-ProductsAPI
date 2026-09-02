using ProdutosApi.Models;

namespace ProdutosApi.Data;

/// <summary>
/// Contexto de dados em memória que simula um banco de dados.
/// Registrado como Singleton para manter o estado durante a vida da aplicação.
/// </summary>
public class AppDbContext
{
    private readonly List<Produto> _produtos = new();
    private int _nextId = 1;

    public AppDbContext()
    {
        // Dados de exemplo (seed) para facilitar os testes no Swagger.
        _produtos.Add(new Produto { Id = _nextId++, Nome = "Teclado Mecânico", Preco = 349.90m, Categoria = "Periféricos", Estoque = 25 });
        _produtos.Add(new Produto { Id = _nextId++, Nome = "Monitor 27\" 144Hz", Preco = 1899.00m, Categoria = "Monitores", Estoque = 10 });
        _produtos.Add(new Produto { Id = _nextId++, Nome = "Mouse sem Fio", Preco = 129.90m, Categoria = "Periféricos", Estoque = 40 });
    }

    public List<Produto> Produtos => _produtos;

    public int GetNextId() => _nextId++;
}
