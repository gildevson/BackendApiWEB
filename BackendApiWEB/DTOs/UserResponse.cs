namespace BackendApiWEB.DTOs {
    public class UserResponse {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Permissao { get; set; } = string.Empty;

    }

}
