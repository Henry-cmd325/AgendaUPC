using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public int IdMateria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaLimite { get; set; }

    public virtual Materia IdMateriaNavigation { get; set; } = null!;

    public virtual ICollection<TareasUsuario> TareasUsuarios { get; } = new List<TareasUsuario>();
}
