namespace ProdutosApi.Models;

/// <summary>
/// Entidade de domínio que representa um produto do catálogo de e-commerce.
/// </summary>
public class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public string Categoria { get; set; } = string.Empty;

    public int Estoque { get; set; }
}
