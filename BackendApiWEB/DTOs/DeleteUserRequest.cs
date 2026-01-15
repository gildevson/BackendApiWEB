namespace BackendApiWEB.DTOs {
    public class DeleteUserRequest {
        public Guid Id { get; set; }       // quem vai ser excluído
        public Guid IdLogado { get; set; } // quem está logado
    }
}
