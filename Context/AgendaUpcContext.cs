﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgendaUpc.Context;

public partial class AgendaUpcContext : DbContext
{
    public AgendaUpcContext()
    {
    }

    public AgendaUpcContext(DbContextOptions<AgendaUpcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dia> Dias { get; set; }

    public virtual DbSet<HorarioMateria> HorarioMaterias { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<MateriasUsuario> MateriasUsuarios { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<TareasUnica> TareasUnicas { get; set; }

    public virtual DbSet<TareasUsuario> TareasUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Dia>(entity =>
        {
            entity.HasKey(e => e.IdDia).HasName("PRIMARY");

            entity.ToTable("dias");

            entity.Property(e => e.IdDia)
                .HasColumnType("int(11)")
                .HasColumnName("id_dia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<HorarioMateria>(entity =>
        {
            entity.HasKey(e => e.IdHorariosMaterias).HasName("PRIMARY");

            entity.ToTable("horario_materias");

            entity.HasIndex(e => e.IdUsuario, "fk_Horariomaterias_Usuarios");

            entity.HasIndex(e => e.IdDia, "fk_horariomaterias_dias");

            entity.HasIndex(e => e.IdMateria, "fk_horariomaterias_materias");

            entity.Property(e => e.IdHorariosMaterias)
                .HasColumnType("int(11)")
                .HasColumnName("id_horarios_materias");
            entity.Property(e => e.Hora)
                .HasColumnType("time")
                .HasColumnName("hora");
            entity.Property(e => e.IdDia)
                .HasColumnType("int(11)")
                .HasColumnName("id_dia");
            entity.Property(e => e.IdMateria)
                .HasColumnType("int(11)")
                .HasColumnName("id_materia");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");

            entity.HasOne(d => d.IdDiaNavigation).WithMany(p => p.HorarioMateria)
                .HasForeignKey(d => d.IdDia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_horariomaterias_dias");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.HorarioMateria)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("fk_horariomaterias_materias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HorarioMateria)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Horariomaterias_Usuarios");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PRIMARY");

            entity.ToTable("materias");

            entity.Property(e => e.IdMateria)
                .HasColumnType("int(11)")
                .HasColumnName("id_materia");
            entity.Property(e => e.Generacion)
                .HasMaxLength(20)
                .HasColumnName("generacion");
            entity.Property(e => e.Grupo)
                .HasMaxLength(15)
                .HasColumnName("grupo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MateriasUsuario>(entity =>
        {
            entity.HasKey(e => e.IdMateriaUsuario).HasName("PRIMARY");

            entity.ToTable("materias_usuarios");

            entity.HasIndex(e => e.IdUsuario, "Materias_usuarios");

            entity.HasIndex(e => e.IdMateria, "fk_materiasusuarios_materias");

            entity.Property(e => e.IdMateriaUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_materia_usuario");
            entity.Property(e => e.IdMateria)
                .HasColumnType("int(11)")
                .HasColumnName("id_materia");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.MateriasUsuarios)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_materiasusuarios_materias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.MateriasUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Materias_usuarios");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PRIMARY");

            entity.ToTable("tareas");

            entity.HasIndex(e => e.IdMateria, "fk_tareas_materias");

            entity.Property(e => e.IdTarea)
                .HasColumnType("int(11)")
                .HasColumnName("id_tarea");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(700)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaLimite)
                .HasColumnType("datetime")
                .HasColumnName("fecha_limite");
            entity.Property(e => e.IdMateria)
                .HasColumnType("int(11)")
                .HasColumnName("id_materia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tareas_materias");
        });

        modelBuilder.Entity<TareasUnica>(entity =>
        {
            entity.HasKey(e => e.IdUnica).HasName("PRIMARY");

            entity.ToTable("tareas_unicas");

            entity.HasIndex(e => e.IdMateria, "fk_tareasunicas_materias");

            entity.HasIndex(e => e.IdUsuario, "fk_tareasunicas_usuarios");

            entity.Property(e => e.IdUnica)
                .HasColumnType("int(11)")
                .HasColumnName("id_unica");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(700)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.IdMateria)
                .HasColumnType("int(11)")
                .HasColumnName("id_materia");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .HasColumnName("nombre");
            entity.Property(e => e.Terminada)
                .HasColumnType("bit(1)")
                .HasColumnName("terminada");

            entity.HasOne(d => d.IdMateriaNavigation).WithMany(p => p.TareasUnicas)
                .HasForeignKey(d => d.IdMateria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tareas_unicas_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TareasUnicas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tareasunicas_usuarios");
        });

        modelBuilder.Entity<TareasUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTareasUsuarios).HasName("PRIMARY");

            entity.ToTable("tareas_usuarios");

            entity.HasIndex(e => e.IdTarea, "fk_tareasusuarios_tareas");

            entity.HasIndex(e => e.IdUsuario, "fk_tareasusuarios_usuarios");

            entity.Property(e => e.IdTareasUsuarios)
                .HasColumnType("int(11)")
                .HasColumnName("id_tareas_usuarios");
            entity.Property(e => e.IdTarea)
                .HasColumnType("int(11)")
                .HasColumnName("id_tarea");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Terminada)
                .HasColumnType("bit(1)")
                .HasColumnName("terminada");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.TareasUsuarios)
                .HasForeignKey(d => d.IdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tareasusuarios_tareas");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TareasUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tareasusuarios_usuarios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.ContraSiupc)
                .HasMaxLength(100)
                .HasColumnName("contra_siupc");
            entity.Property(e => e.ContraUpdc)
                .HasMaxLength(100)
                .HasColumnName("contra_updc");
            entity.Property(e => e.Matricula)
                .HasMaxLength(20)
                .HasColumnName("matricula");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
