namespace SeuProjeto.DTOs;

public class CadastroClienteRepresentanteDTO
{
    public PessoaCadastroDTO Cliente { get; set; } = new() { TipoPessoa = "CLIENTE" };
    public PessoaCadastroDTO Representante { get; set; } = new() { TipoPessoa = "REPRESENTANTE" };
}
