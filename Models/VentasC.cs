using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDesarrollo.B.P.Models;

public partial class VentasC : DbContext
{
    public VentasC()
    {
    }

    public VentasC(DbContextOptions<VentasC> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCategoria> TbCategorias { get; set; }
    public virtual DbSet<TbCliente> TbClientes { get; set; }

    public virtual DbSet<TbDetalleVenta> TbDetalleventas { get; set; }

    public virtual DbSet<TbProducto> TbProductos { get; set; }
    public virtual DbSet<TbVenta> TbVentas { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__VentasDB");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Falta configurar ConnectionStrings__VentasDB. Consulta README_VSCODE.md.");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<TbCategoria>(entity =>
        {
            entity.HasKey(e => e.CodCategoria).HasName("PK__TB_CATEG__5A4D9907AC91AB07");

            entity.ToTable("TB_CATEGORIA");

            entity.Property(e => e.CodCategoria)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_CATEGORIA");
            entity.Property(e => e.NomCat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOM_CAT");
        });

        modelBuilder.Entity<TbCliente>(entity =>
        {
            entity.HasKey(e => e.CodCliente).HasName("PK__TB_CLIEN__8112345F0B216450");

            entity.ToTable("TB_CLIENTE");

            entity.Property(e => e.CodCliente)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_CLIENTE");
            entity.Property(e => e.ApeCli)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APE_CLI");
            entity.Property(e => e.DirCli)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DIR_CLI");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DNI");
            entity.Property(e => e.NomCli)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOM_CLI");
        });

        modelBuilder.Entity<TbDetalleVenta>(entity =>
        {
            entity.HasKey(e => new { e.CodVenta, e.CodProducto }).HasName("PK__TB_DETAL__77752C1EDD846E75");

            entity.ToTable("TB_DETALLEVENTA");

            entity.Property(e => e.CodVenta)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.CodProducto)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_PRODUCTO");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CodProductoNavigation).WithMany(p => p.TbDetalleventa)
                .HasForeignKey(d => d.CodProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_DETALL__COD_P__5441852A");

            entity.HasOne(d => d.CodVentaNavigation).WithMany(p => p.TbDetalleventa)
                .HasForeignKey(d => d.CodVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_DETALL__COD_V__534D60F1");
        });

        modelBuilder.Entity<TbProducto>(entity =>
        {
            entity.HasKey(e => e.CodProducto).HasName("PK__TB_PRODU__8DF7FD2DC8CE7F24");

            entity.ToTable("TB_PRODUCTO");

            entity.Property(e => e.CodProducto)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_PRODUCTO");
            entity.Property(e => e.CodCategoria)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_CATEGORIA");
            entity.Property(e => e.EstaProducto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ESTA_PRODUCTO");
            entity.Property(e => e.Imagenes)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGENES");
            entity.Property(e => e.NomProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOM_PRODUCTO");
            entity.Property(e => e.PreProducto)
                .HasColumnType("money")
                .HasColumnName("PRE_PRODUCTO");
            entity.Property(e => e.StkProducto).HasColumnName("STK_PRODUCTO");

            entity.HasOne(d => d.CodCategoriaNavigation).WithMany(p => p.TbProductos)
                .HasForeignKey(d => d.CodCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_PRODUC__COD_C__534D60F1");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TB_USUAR__3214EC0729B208B5");

            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.UsuClave)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.UsuNombre)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbVenta>(entity =>
        {
            entity.HasKey(e => e.CodVenta).HasName("PK__TB_VENTA__BFAA53CC3E378D60");

            entity.ToTable("TB_VENTA");

            entity.Property(e => e.CodVenta)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_VENTA");
            entity.Property(e => e.CodCliente)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COD_CLIENTE");
            entity.Property(e => e.EstadoVenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ESTADO_VENTA");
            entity.Property(e => e.FecVenta)
                .HasColumnType("date")
                .HasColumnName("FEC_VENTA");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TOTAL_VENTA");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.TbVenta)
                .HasForeignKey(d => d.CodCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_VENTA__COD_CL__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
