namespace BackendApiWEB.DTOs {
    public class ProdutoCreateRequest {
    public string Nome { get; set; } = "";
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }  
        public int Estoque { get; set; }
        public bool Ativo { get; set; } = true;

    }
}
