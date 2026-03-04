namespace BackendApiWEB.Models
{
    public class Cliente
    {
        Guid id { get; set; }
        public string nome { get; set; }
        public string cnpjcpf { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string Numero { get; set; }
        public string complemento { get; set; }
        public string Estado { get; set; } 
        public DateTime dataCadastro { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
