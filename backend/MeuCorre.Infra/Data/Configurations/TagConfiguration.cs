using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Tags");

            //Define a chave primária.
            builder.HasKey(tag => tag.Id);

            //Define as propriedades da entidade e suas configurações.
            builder.Property(tag => tag.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(tag => tag.Cor)
                .HasMaxLength(10);

            builder.Property(tag => tag.DataCriacao)
                .IsRequired();

            builder.Property(tag => tag.DataAtualizacao)
                .IsRequired(false);

            //Chaves Estrangeiras FK
            //Define o relacionamento entre Categoria e Usuario 
            builder.HasOne(tag => tag.Usuario)
                .WithMany(usuario => usuario.Tags)
                .HasForeignKey(tag => tag.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
