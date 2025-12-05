using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public SubcategoriaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task<Subcategoria?> ObterPorIdAsync(Guid subcategoriaId)
        {
            var subcategoria = await _meuDbContext.Subcategorias.FindAsync(subcategoriaId);
            return subcategoria;
        }

        public async Task<IList<Subcategoria>> ListarTodasPorUsuarioAsync(Guid usuarioId)
        {
            var listaSubcategorias =  _meuDbContext.Subcategorias
                .Where(s => s.UsuarioId == usuarioId);

            return await listaSubcategorias.ToListAsync();
        }

        public async Task<IList<Subcategoria>> ListarPorCategoriaAsync(Guid categoriaId)
        {
            var listaSubcategorias = _meuDbContext.Subcategorias
                .Where(s => s.CategoriaId == categoriaId);

            return await listaSubcategorias.ToListAsync();
        }

        public async Task<bool> ExisteAsync(Guid subcategoriaId)
        {
            var existe = await _meuDbContext.Subcategorias
                .AnyAsync(s => s.Id == subcategoriaId);

            return existe;
        }

        public async Task<bool> NomeExisteParaCategoriaAsync(string nome, TipoTransacao tipo, Guid categoriaId)
        {
            var existe = await _meuDbContext.Subcategorias
                .AnyAsync(
                            s => s.Nome == nome && 
                            s.CategoriaId == categoriaId &&
                            s.TipoDaTransacao == tipo
                        );

            return existe;
        }

        public async Task AdicionarAsync(Subcategoria subcategoria)
        {
            _meuDbContext.Subcategorias.Add(subcategoria);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Subcategoria subcategoria)
        {
            _meuDbContext.Subcategorias.Update(subcategoria);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task RemoverAsync(Subcategoria subcategoria)
        {
            _meuDbContext.Subcategorias.Remove(subcategoria);
            await _meuDbContext.SaveChangesAsync();
        }
    }
}
