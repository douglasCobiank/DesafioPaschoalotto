namespace Desafio.API.Models
{
    public class Divida
    {
        public int NumeroTitulo { get; set; }
        public string? NomeDevedor { get; set; }
        public string? CPF { get; set; }
        public decimal PorcentagemJuros { get; set; }
        public decimal PorcentagemMulta { get; set; }
        public List<Parcela>? Parcelas { get; set; }
    }
}