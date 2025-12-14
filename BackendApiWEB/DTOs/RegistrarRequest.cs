using System.ComponentModel.DataAnnotations;

namespace BackendApiWEB.DTOs {
    public class RegistrarRequest {
        [Required]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Senha { get; set; }

        public int? PermissaoId { get; set; }
    }


}
