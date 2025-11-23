namespace BackendApiWEB.DTOs {
    public class AuthResult {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public UserResponse? Usuario { get; set; }

        public AuthResult(bool sucesso, string mensagem, UserResponse? usuario) {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Usuario = usuario;
        }
    }
}
