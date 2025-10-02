using AutoMapper;
using Desafio.Core.Handler.Interface;
using Desafio.Core.Handler.Models;
using Desafio.Infrastructure.Data.Models;
using Desafio.Infrastructure.Repositories;

namespace Desafio.Core.Handler.Services
{
    public class DesafioService(IDesafioRepository desafioRepository, IMapper mapper) : IDesafioService
    {
        private readonly IDesafioRepository _desafioRepository = desafioRepository;
        private readonly IMapper _mapper = mapper;

        public async Task AddDividaAsync(DividaDto dividaDto)
        {
            AtualizaDataVencimento(dividaDto);

            var dividaData = _mapper.Map<DividaData>(dividaDto);
            await _desafioRepository.AddAsync(dividaData);
        }

        public async Task<List<DividaDto>> GetDividaAsync()
        {
            var dividaData = await _desafioRepository.GetAllAsync();
            var result = _mapper.Map<List<DividaDto>>(dividaData.ToList());
            result.ForEach(p => AtualizarValores(p));
            return result;
        }

        public async Task<DividaDto> GetByIdDividaAsync(int idDivida)
        {
            var dividaData = await _desafioRepository.GetByIdAsync(idDivida);
            return _mapper.Map<DividaDto>(dividaData);
        }

        private void AtualizarValores(DividaDto divida)
        {
            if (divida.Parcelas == null || divida.Parcelas.Count == 0) return;

            decimal valorAtualizado = 0.0M;
            int diasAtraso = 0;

            foreach (var parcela in divida.Parcelas)
            {
                decimal valorParcela = parcela.ValorParcela;
                diasAtraso = (DateTime.Now - parcela.DataVencimento).Days;

                if (diasAtraso > 0)
                {
                    decimal juros = (divida.PorcentagemJuros / 100 / 30) * diasAtraso * valorParcela;
                    decimal multa = (divida.PorcentagemMulta / 100) * valorParcela;
                    valorParcela += juros + multa;
                }

                valorAtualizado += valorParcela;
            }

            divida.ValorOriginal = divida.Parcelas.Sum(p => p.ValorParcela);
            divida.ValorAtualizado = valorAtualizado;
            divida.DiasAtraso = diasAtraso;
        }

        private void AtualizaDataVencimento(DividaDto dividaDto)
        {
            dividaDto.Parcelas = dividaDto.Parcelas.Select(p => new ParcelaDto
            {
                ValorParcela = p.ValorParcela,
                DataVencimento = DateTime.SpecifyKind(p.DataVencimento, DateTimeKind.Utc)
            }).ToList();
        }
    }
}