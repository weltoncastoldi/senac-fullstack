using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public TagRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task AdicionarAsync(Tag tag)
        {
            _meuDbContext.Tags.Add(tag);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Tag tag)
        {
            _meuDbContext.Tags.Update(tag);
            await _meuDbContext.SaveChangesAsync();
        }

        public Task<bool> ExisteAsync(Guid tagId)
        {
            var existe = _meuDbContext.Tags.AnyAsync(t => t.Id == tagId);
            return existe;
        }

        public async Task<IList<Tag>> ListarTodasPorUsuarioAsync(Guid usuarioId)
        {
            var listaDeTags = _meuDbContext.Tags
                 .Where(c => c.UsuarioId == usuarioId);

            return await listaDeTags.ToListAsync();
        }

        public Task<bool> NomeExisteParaUsuarioAsync(string nome, Guid usuarioId)
        {
            var existe = _meuDbContext.Tags.AnyAsync(
                t => t.Nome == nome &&
                t.UsuarioId == usuarioId);
            
            return existe;
        }

        public async Task<Tag?> ObterPorIdAsync(Guid tagId)
        {
            var tag = _meuDbContext.Tags.FirstOrDefaultAsync(t => t.Id == tagId);
            return await tag;
        }

        public async Task RemoverAsync(Tag tag)
        {
            _meuDbContext.Tags.Remove(tag);
            await _meuDbContext.SaveChangesAsync();
        }
    }
}
