namespace BackendApiWEB.Models {
    public class Produtos {
        public int id { get; set; }
        public string Nome { get; set; }
        public string ? Descricao { get; set; } // ? para dizer que é opcional pode ser Nula
        public decimal Preco { get; set; }
        public string Estoque {get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? AtualizadoEm { get; set; }

    }
}
