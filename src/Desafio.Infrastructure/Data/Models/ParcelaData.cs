using System.ComponentModel.DataAnnotations;

namespace Desafio.Infrastructure.Data.Models
{
    public class ParcelaData
    {
        [Key]
        public int Id { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime DataVencimento { get; set; } 
        public decimal ValorParcela { get; set; }
    }
}