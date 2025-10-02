namespace Desafio.Core.Handler.Models
{
    public class DividaDto
    {
        public int NumeroTitulo { get; set; }
        public string? NomeDevedor { get; set; }
        public string? CPF { get; set; }
        public decimal ValorOriginal { get; set; }
        public int DiasAtraso { get; set; }
        public decimal ValorAtualizado { get; set; }
        public decimal PorcentagemJuros { get; set; }
        public decimal PorcentagemMulta { get; set; }
        public List<ParcelaDto>? Parcelas { get; set; }
    }
}