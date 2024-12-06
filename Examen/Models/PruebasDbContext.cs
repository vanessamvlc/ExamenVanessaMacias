using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Examen.Models;

public partial class PruebasDbContext : DbContext
{
    public PruebasDbContext()
    {
    }

    public PruebasDbContext(DbContextOptions<PruebasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatBrand> CatBrands { get; set; }

    public virtual DbSet<CatModel> CatModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-O23NPL0;Database=PruebasDB;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatBrand>(entity =>
        {
            entity.HasKey(e => e.IdBrand).HasName("PK__CatBrand__E353A48EEDDEC03D");

            entity.Property(e => e.IdBrand).HasColumnName("idBrand");
            entity.Property(e => e.AveragePrice)
                .HasColumnType("decimal(30, 2)")
                .HasColumnName("averagePrice");
            entity.Property(e => e.Namee)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("namee");
        });

        modelBuilder.Entity<CatModel>(entity =>
        {
            entity.HasKey(e => e.IdModel).HasName("PK__CatModel__144F1E55DA3C0D7E");

            entity.Property(e => e.IdModel).HasColumnName("idModel");
            entity.Property(e => e.AveragePrice)
                .HasColumnType("decimal(30, 2)")
                .HasColumnName("averagePrice");
            entity.Property(e => e.IdBrand).HasColumnName("idBrand");
            entity.Property(e => e.Namee)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("namee");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
