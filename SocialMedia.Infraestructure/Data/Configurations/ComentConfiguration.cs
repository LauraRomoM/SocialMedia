using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infraestructure.Data.Configurations
{
    public class ComentConfiguration : IEntityTypeConfiguration<Coment>
    {
        public void Configure(EntityTypeBuilder<Coment> builder)
        {
            builder.ToTable("Comentario");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdComentario")
                .ValueGeneratedNever();

            builder.Property(e => e.PostId)          //mapeamos PostId
                        .HasColumnName("IdPublicacion");        //nombre de encabezadp de tabla

            builder.Property(e => e.UserId)
                        .HasColumnName("IdUsuario");

            builder.Property(e => e.IsActive)
                        .HasColumnName("Activo");

            builder.Property(e => e.Description)
                        .IsRequired()
                        .HasColumnName("Descripcion")
                        .HasMaxLength(500)
                        .IsUnicode(false);

            builder.Property(e => e.Date)
                        .HasColumnName("Fecha")
                        .HasColumnType("datetime");

            builder.HasOne(d => d.Post)
                        //  .HasColumnName("Publicacion")
                        .WithMany(p => p.Coments)
                        .HasForeignKey(d => d.PostId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Comentario_Publicacion");        //nombre de la llave foranea de la BD
            builder.HasOne(d => d.User)
                        .WithMany(p => p.Coments)
                        .HasForeignKey(d => d.UserId)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Comentario_Usuario");
        }
    }
}
