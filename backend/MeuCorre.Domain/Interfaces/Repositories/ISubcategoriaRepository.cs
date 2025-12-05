using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface ISubcategoriaRepository
    {
        //Retorna do banco de dados os dados de uma subcategoria que possua o Id informado
        Task<Subcategoria?>ObterPorIdAsync(Guid subcategoriaId);

        //Retorna do banco de dados todas as subcategorias que pertençam ao usuário informado
        Task<IList<Subcategoria>> ListarTodasPorUsuarioAsync(Guid usuarioId);

        //Retorna todas as subcategorias de uma categoria específica
        Task<IList<Subcategoria>> ListarPorCategoriaAsync(Guid categoriaId);

        //Verificar se uma subcategoria existe no banco de dados com o Id informado
        Task<bool> ExisteAsync(Guid subcategoriaId);

        //Verifica se já existe uma subcategoria com o mesmo
        //nome e tipo para o usuário e categoria informados
        Task<bool> NomeExisteParaCategoriaAsync(string nome, TipoTransacao tipo, Guid categoriaId);

        //Adiciona uma nova subcategoria no banco de dados
        Task AdicionarAsync(Subcategoria subcategoria);

        //Atualiza os dados de uma subcategoria no banco de dados
        Task AtualizarAsync(Subcategoria subcategoria);

        //Remove uma subcategoria do banco de dados
        Task RemoverAsync(Subcategoria subcategoria);
    }
}
