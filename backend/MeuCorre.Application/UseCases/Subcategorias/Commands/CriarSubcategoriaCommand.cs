using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Subcategorias.Commands
{
    public class CriarSubcategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id do usuário")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da categoria")]
        public required Guid CategoriaId { get; set; }
        
        [Required(ErrorMessage = "Nome da subcategoria é obrigatório")]
        public required string Nome { get; set; }
        
        [Required(ErrorMessage = "Tipo da transação (despesa ou receita) é obrigatório")]
        public required TipoTransacao Tipo { get; set; }
        
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

    }

    internal class CriarSubcategoriaCommandHandler : IRequestHandler<CriarSubcategoriaCommand, (string, bool)>
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarSubcategoriaCommandHandler(ISubcategoriaRepository subcategoriaRepository,
                                               ICategoriaRepository categoriaRepository,
                                               IUsuarioRepository usuarioRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(CriarSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            //VERIFICAR SE O USUÁRIO EXISTE
            var usuario = await _usuarioRepository.ObterUsuarioPorId(request.UsuarioId);
            if (usuario == null)
            {
                return ("Usuário inválido", false);
            }

            //VERIFICAR SE A CATEGORIA EXISTE
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
            if (categoria == null)
            {
                return ("Categoria não encontrada", false);
            }
            
            //NÃO PODE CADASTRAR SUBCATEGORIA REPETIDA PARA A MESMA CATEGORIA
            var existe =
                await _subcategoriaRepository.NomeExisteParaCategoriaAsync(
                    request.Nome, request.Tipo, request.CategoriaId);

            if (existe)
            {
                return ("Subcategoria já cadastrada para esta categoria", false);
            }

            var novaSubcategoria = new Subcategoria(
                    request.UsuarioId,
                    request.CategoriaId,
                    request.Nome,
                    request.Tipo,
                    request.Descricao,
                    request.Cor,
                    request.Icone
                );

            await _subcategoriaRepository.AdicionarAsync(novaSubcategoria);
            return ("Subcategoria cadastrada com sucesso", true);
        }
    }
}
