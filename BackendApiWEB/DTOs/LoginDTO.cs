namespace BackendApiWEB.DTOs {
    public class LoginDTO {
        public string Token { get; set; }
        public string Nome { get; set; }
        public IEnumerable<string> Permissoes { get; set; }
    }
}
