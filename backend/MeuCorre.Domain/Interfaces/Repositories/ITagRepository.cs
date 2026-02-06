using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface ITagRepository
    {
        //Retorna do banco de dados os dados de uma tag que possua o Id informado
        Task<Tag?> ObterPorIdAsync(Guid tagId);

        //Retorna do banco de dados todas as tags que pertençam ao usuário informado
        Task<IList<Tag>> ListarTodasPorUsuarioAsync(Guid usuarioId);

        //Verificar se uma tag existe no banco de dados com o Id informado
        //SELECT * FROM Tags WHERE Id = 5
        Task<bool> ExisteAsync(Guid tagId);

        //Verifica se já existe uma categoria com o mesmo
        //nome e tipo para o usuário informado
        //nome e tipo para o usuário informado
        Task<bool> NomeExisteParaUsuarioAsync(string nome, Guid usuarioId);

        //Adiciona uma nova tag no banco de dados
        Task AdicionarAsync(Tag tag);

        //Atualiza os dados de uma tag no banco de dados
        Task AtualizarAsync(Tag tag);

        //Remove uma tag do banco de dados
        Task RemoverAsync(Tag tag);
    }
}
