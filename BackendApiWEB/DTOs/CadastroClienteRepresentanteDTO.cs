namespace BackendApiWEB.DTOs {
    public class CadastroClienteRepresentanteDTO {
        public PessoaCadastroDTO cliente { get; set; } = new() { TipoPessoa = "CLIENTE"};
        public PessoaCadastroDTO representante { get; set; } = new() { TipoPessoa = "REPRESENTANTE" }; 
    }
}
