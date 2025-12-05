using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Commands
{
    public class AtualizarSubcategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id da subcategoria é obrigatório")]
        public required Guid SubcategoriaId { get; set; }

        [Required(ErrorMessage = "Nome da subcategoria é obrigatório")]
        public required string Nome { get; set; } 
        
        [Required(ErrorMessage = "Tipo (despesa ou receita) da subcategoria é obrigatório")]
        public required TipoTransacao Tipo { get; set; }

        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
    }

    internal class AtualizarSubcategoriaCommandHandler : IRequestHandler<AtualizarSubcategoriaCommand, (string, bool)>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        public AtualizarSubcategoriaCommandHandler(ISubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<(string, bool)> Handle(AtualizarSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            var subcategoria = await _subcategoriaRepository.ObterPorIdAsync(request.SubcategoriaId);

            if (subcategoria == null)
            {
                return ("Subcategoria não encontrada", false);
            }

            var subcategoriaEstaDuplicada = 
                await _subcategoriaRepository.NomeExisteParaCategoriaAsync(request.Nome, request.Tipo, subcategoria.CategoriaId);

            if (subcategoriaEstaDuplicada)
            {
                return ("Já existe uma subcategoria cadastrada com esses dados", false);

            }

            subcategoria.AtualizarInformacoes(
                request.Nome, 
                request.Tipo, 
                request.Descricao, 
                request.Cor, 
                request.Icone);

            await _subcategoriaRepository.AtualizarAsync(subcategoria);
            return ("Subcategoria atualizada com sucesso", true);

        }
    }
}
