using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class MateriasUsuario
{
    public int IdMateriaUsuario { get; set; }

    public int IdMateria { get; set; }

    public int IdUsuario { get; set; }

    public virtual Materia IdMateriaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
