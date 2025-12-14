namespace BackendApiWEB.Models {
    public class Usuario {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }

        // 🔥 Adicionar esta propriedade
        public List<string> Permissoes { get; set; } = new List<string>();
    }
}
