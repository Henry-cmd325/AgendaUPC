using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class HorarioMateria
{
    public int IdHorariosMaterias { get; set; }

    public int IdDia { get; set; }

    public int? IdMateria { get; set; }

    public TimeOnly Hora { get; set; }

    public int IdUsuario { get; set; }

    public virtual Dia IdDiaNavigation { get; set; } = null!;

    public virtual Materia? IdMateriaNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
