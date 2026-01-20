namespace BackendApiWEB.DTOs {
    public class CadastroClienteRepresentanteDTO {
        public PessoaCadastroDTO Cliente { get; set; } = new() { TipoPessoa = "CLIENTE"};
        public PessoaCadastroDTO Fornecedor { get; set; } = new() { TipoPessoa = "FORNECEDOR" }; 
    }
}
