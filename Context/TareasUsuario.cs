using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class TareasUsuario
{
    public int IdTareasUsuarios { get; set; }

    public int IdTarea { get; set; }

    public int IdUsuario { get; set; }

    public ulong Terminada { get; set; }

    public virtual Tarea IdTareaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
