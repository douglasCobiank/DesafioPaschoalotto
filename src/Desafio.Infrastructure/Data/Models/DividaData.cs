using System.ComponentModel.DataAnnotations;

namespace Desafio.Infrastructure.Data.Models
{
    public class DividaData
    {
        [Key]
        public int Id { get; set; }
        public int NumeroTitulo { get; set; }
        public string? NomeDevedor { get; set; }
        public string? CPF { get; set; }
        public decimal PorcentagemJuros { get; set; }
        public decimal PorcentagemMulta { get; set; }
        public List<ParcelaData>? Parcelas { get; set; }
    }
}