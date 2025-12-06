namespace BackendApiWEB.DTOs {
    public class AuthResult {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public UserResponse? Usuario { get; set; }

        // ERRO (sem usuário)
        public AuthResult(bool sucesso, string mensagem) {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        // SUCESSO (com usuário)
        public AuthResult(bool sucesso, string mensagem, UserResponse usuario) {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Usuario = usuario;
        }
    }
}
