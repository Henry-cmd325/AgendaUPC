using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class TareasUnica
{
    public int IdUnica { get; set; }

    public int IdUsuario { get; set; }

    public int IdMateria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaEntrega { get; set; }

    public ulong Terminada { get; set; }

    public virtual Materia IdMateriaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Notificacione> Notificaciones { get; } = new List<Notificacione>();
}
