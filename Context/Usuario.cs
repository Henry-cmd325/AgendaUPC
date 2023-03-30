using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Matricula { get; set; } = null!;

    public string? ContraSiupc { get; set; }

    public string? ContraUpdc { get; set; }

    public virtual ICollection<HorarioMateria> HorarioMateria { get; } = new List<HorarioMateria>();

    public virtual ICollection<MateriasUsuario> MateriasUsuarios { get; } = new List<MateriasUsuario>();

    public virtual ICollection<TareasUnica> TareasUnicas { get; } = new List<TareasUnica>();

    public virtual ICollection<TareasUsuario> TareasUsuarios { get; } = new List<TareasUsuario>();
}
