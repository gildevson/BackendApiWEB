namespace BackendApiWEB.Models
{
    public class Cliente
    {
        Guid id { get; set; }
        string nome { get; set; }
        string cnpjcpf { get; set; }
        string cep { get; set; }
        string cidade { get; set; }
        string endereco { get; set; }
        string bairro { get; set; }
        string Numero { get; set; }
        string complemento { get; set; }
        string Estado { get; set; } 
        DateTime dataCadastro { get; set; }

    }
}
