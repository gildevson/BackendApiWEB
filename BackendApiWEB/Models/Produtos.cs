namespace BackendApiWEB.Models {
    public class Produtos {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string ? Descricao { get; set; } // ? para dizer que é opcional pode ser Nula
        public decimal Preco { get; set; }
        public int Estoque {get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }

    }
}
