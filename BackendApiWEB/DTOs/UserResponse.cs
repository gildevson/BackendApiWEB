namespace BackendApiWEB.DTOs {
    public class UserResponse {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public IEnumerable<string> Permissoes { get; set; } = new List<string>();
    }

}
