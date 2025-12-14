namespace BackendApiWEB.DTOs {
    public class UpdateUserRequest {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        // Opcional: só troca se vier preenchida
        public string? Senha { get; set; }

        public int? PermissaoId { get; set; }
    }
}
