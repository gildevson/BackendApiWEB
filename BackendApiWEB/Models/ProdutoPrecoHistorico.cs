namespace BackendApiWEB.Models {
    public class ProdutoPrecoHistorico {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal VigenciaInicio { get; set; }
        public decimal? VigenciaFim { get; set; }  // null

    }
}
