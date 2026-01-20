namespace SeuProjeto.DTOs;

public class PessoaCadastroDTO
{
    public string TipoPessoa { get; set; } = ""; // CLIENTE / REPRESENTANTE

    public string CpfCnpj { get; set; } = "";
    public string RazaoSocial { get; set; } = "";
    public string? Fantasia { get; set; }

    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }

    public string? NomeContato { get; set; }
    public string? Email { get; set; }
    public string? Telefone1 { get; set; }
}
