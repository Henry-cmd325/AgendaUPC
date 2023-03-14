using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class Dia
{
    public int IdDia { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<HorarioMateria> HorarioMateria { get; } = new List<HorarioMateria>();
}
