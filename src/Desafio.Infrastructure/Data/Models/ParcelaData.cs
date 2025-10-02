using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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