using System.ComponentModel.DataAnnotations;

namespace ProdutosApi.DTOs;

/// <summary>
/// DTO utilizado para criação e atualização de um produto.
/// Não expõe o campo Id, que é controlado pela API.
/// </summary>
public class ProdutoRequest
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 120 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, 1_000_000, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [StringLength(60, ErrorMessage = "A categoria deve ter no máximo 60 caracteres.")]
    public string Categoria { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
    public int Estoque { get; set; }
}
