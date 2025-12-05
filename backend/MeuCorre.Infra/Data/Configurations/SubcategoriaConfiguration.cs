using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class SubcategoriaConfiguration : IEntityTypeConfiguration<Subcategoria>
    {
        public void Configure(EntityTypeBuilder<Subcategoria> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Subcategorias");

            //Define a chave primária.
            builder.HasKey(subcategoria => subcategoria.Id);
            
            //Define as propriedades da entidade e suas configurações.
            builder.Property(subcategoria => subcategoria.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(subcategoria => subcategoria.Descricao)
                .HasMaxLength(255);

            builder.Property(subcategoria => subcategoria.Cor)
                .HasMaxLength(10);

            builder.Property(subcategoria => subcategoria.Icone)
                .HasMaxLength(10);

            builder.Property(subcategoria => subcategoria.TipoDaTransacao)
                .IsRequired();

            builder.Property(subcategoria => subcategoria.DataCriacao)
                .IsRequired();

            builder.Property(subcategoria => subcategoria.DataAtualizacao)
                .IsRequired(false);

            //Chaves Estrangeiras FK
            //Define o relacionamento entre Subcategoria e Usuario 
            builder.HasOne(subcategoria => subcategoria.Usuario)
                .WithMany()
                .HasForeignKey(subcategoria => subcategoria.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            //Define o relacionamento entre Subcategoria e Categoria
            builder.HasOne(subcategoria => subcategoria.Categoria)
                .WithMany()
                .HasForeignKey(subcategoria => subcategoria.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
