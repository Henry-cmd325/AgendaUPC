using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class Materia
{
    public int IdMateria { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Grupo { get; set; }

    public string? Generacion { get; set; }

    public virtual ICollection<HorarioMateria> HorarioMateria { get; } = new List<HorarioMateria>();

    public virtual ICollection<MateriasUsuario> MateriasUsuarios { get; } = new List<MateriasUsuario>();

    public virtual ICollection<Tarea> Tareas { get; } = new List<Tarea>();

    public virtual ICollection<TareasUnica> TareasUnicas { get; } = new List<TareasUnica>();
}
