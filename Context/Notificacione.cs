using System;
using System.Collections.Generic;

namespace AgendaUpc.Context;

public partial class Notificacione
{
    public int CveNotificaciones { get; set; }

    public int CveUsuarios { get; set; }

    public string? Mensaje { get; set; }

    public DateTime FechaHora { get; set; }

    public int IdUnica { get; set; }

    public ulong Notificado { get; set; }

    public virtual TareasUnica IdUnicaNavigation { get; set; } = null!;
}
