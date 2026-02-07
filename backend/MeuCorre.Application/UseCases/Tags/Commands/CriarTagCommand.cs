using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Tags.Commands
{
    public class CriarTagCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id do usuário é obrigatório")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage ="Nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage ="Cor é obrigatório")]
        public required string Cor { get; set; }
    }

    internal class CriarTagCommandHandler : IRequestHandler<CriarTagCommand, (string, bool)>
    {
        private readonly ITagRepository _tagRepository;
        public CriarTagCommandHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<(string, bool)> Handle(CriarTagCommand request, CancellationToken cancellationToken)
        {
            var existe = await _tagRepository.NomeExisteParaUsuarioAsync(request.Nome, request.UsuarioId);

            if (existe)
            {
               return ("Você já cadastrou uma tag com este nome", false);
            }

            var tag = new Tag(request.UsuarioId, request.Nome, request.Cor);

            await _tagRepository.AdicionarAsync(tag);

            return ("Tag criada com sucesso", true);
        }
    }
}