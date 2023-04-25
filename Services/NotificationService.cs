using AgendaUpc.Context;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;

namespace AgendaUpc.Services;

public class NotificationService : INotificationService
{
    private readonly AgendaUpcContext _context;
    public NotificationService(AgendaUpcContext context)
    {
        _context = context;
    }

    public ServerResponse<List<NotificationResponse>> CheckNotifications(int idUsuario)
    {
        ServerResponse<List<NotificationResponse>> response = new();

        var dbTareasUnicas = _context.TareasUnicas.Where(t => t.IdUsuario == idUsuario && t.Terminada == 0).ToList();

        var now = DateTime.Now;

        foreach (var dbHomework in dbTareasUnicas)
        {
            var dif = now - dbHomework.FechaEntrega;

            if (dif.Days > -2 && dif.Days < 60)
            {
                var dbNotification = _context.Notificaciones.Where(n => n.CveUsuarios == idUsuario && n.IdUnica == dbHomework.IdUnica).FirstOrDefault();
                var dbMateria = _context.Materias.Where(m => m.IdMateria == dbHomework.IdMateria).First();

                string mensaje = string.Empty;
                
                if (dif.Days > 0)
                    mensaje = $"La tarea: {dbHomework.Nombre} de la materia {dbMateria.Nombre} debio de haber sido entregada hace {dif.Days} día(s)";
                else if (dbHomework.FechaEntrega.Day == now.Day)
                    mensaje = $"La tarea: {dbHomework.Nombre} de la materia {dbMateria.Nombre} debio de ser completada hoy";
                else
                    mensaje = $"La tarea: {dbHomework.Nombre} de la materia {dbMateria.Nombre} se debe de entregar en {Math.Abs(dif.Hours)} horas";
                

                if (dbNotification != null && dbNotification.FechaHora.Day != now.Day)
                {
                    dbNotification.Mensaje = mensaje;
                    dbNotification.FechaHora = now;

                    _context.Entry(dbNotification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges(); 
                }
                else if (dbNotification == null)
                {
                    _context.Notificaciones.Add(new()
                    {
                        CveUsuarios = idUsuario,
                        Mensaje = mensaje,
                        FechaHora = now,
                        IdUnica = dbHomework.IdUnica,
                        
                    });
                    
                    _context.SaveChanges();
                }         
            }
        } 

        return response;
    }

    public ServerResponse<NotificationResponse> DeleteNotification(int idUsuario, int idNotication)
    {
        ServerResponse<NotificationResponse> response = new();

        var dbNotification = _context.Notificaciones.Where(n => n.CveNotificaciones == idNotication && n.CveUsuarios == idUsuario).FirstOrDefault();

        if (dbNotification == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ninguna notificacion del usuario";

            return response;
        }

        _context.Notificaciones.Remove(dbNotification);
        _context.SaveChanges();

        return response;
    }

    public ServerResponse<List<NotificationResponse>> GetAllNotifications(int idUsuario)
    {
        ServerResponse<List<NotificationResponse>> response = new();
        response.Data = new();

        var now = DateTime.Now;
        int idDia = now.DayOfWeek != DayOfWeek.Sunday? ((int)now.DayOfWeek): 7;
        var dbHorarios = _context.HorarioMaterias.Where(h => h.IdUsuario == idUsuario && h.IdDia == idDia).ToList();

        foreach (var dbHorario in dbHorarios)
        {
            var materia = _context.Materias.Where(d => d.IdMateria == dbHorario.IdMateria).FirstOrDefault();
            
            if (materia != null)
            {
                if (dbHorario.Hora.Hour == now.Hour) 
                {
                    response.Data.Add(new()
                    {
                        IdNotication = 0,
                        IdUsuarios = idUsuario,
                        Mensaje = $"En este momento debes de estar en {materia.Nombre}",
                        Notificado = false,
                        FechaHora = now,
                    });
                }

                if (dbHorario.Hora.Hour - 1 == now.Hour)
                {
                    response.Data.Add(new()
                    {
                        IdNotication = 0,
                        IdUsuarios = idUsuario,
                        Mensaje = $"En la siguiente hora debes de estar en {materia.Nombre}",
                        Notificado = false,
                        FechaHora = now,
                    });
                }
            }
        }
        
        var dbNotifications = _context.Notificaciones.Where(n => n.CveUsuarios == idUsuario).ToList();

        foreach (var notification in dbNotifications)
        {
            response.Data.Add(new()
            {
                IdNotication = notification.CveNotificaciones,
                IdUsuarios = idUsuario,
                Mensaje = notification.Mensaje!,
                Notificado = notification.Notificado == 1? true: false,
                FechaHora = notification.FechaHora
            });
        }

        return response;
    }

    public ServerResponse<NotificationResponse> GetNotification(int idUsuario, int idNotication)
    {
       ServerResponse<NotificationResponse> response = new();
       
       var dbNotification = _context.Notificaciones.Where(n => n.CveNotificaciones == idNotication && n.CveUsuarios == idUsuario).FirstOrDefault();

       if (dbNotification == null)
       {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ninguna notificacion del usuario";

            return response;
       }

       response.Data = new()
       {
            IdNotication = dbNotification.CveNotificaciones,
            IdUsuarios = idUsuario,
            Mensaje = dbNotification.Mensaje!,
            FechaHora = dbNotification.FechaHora
       };

       return response;
    }

    public ServerResponse<NotificationResponse> PutNotification(int idUsuario, int idNotication, NotificationRequest request)
    {
        ServerResponse<NotificationResponse> response = new();

        var dbNotification = _context.Notificaciones.Where(n => n.CveNotificaciones == idNotication && n.CveUsuarios == idUsuario).FirstOrDefault();

        if (dbNotification == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ninguna notificacion del usuario";

            return response;
        }

        dbNotification.CveUsuarios = request.IdUsuarios;
        dbNotification.Mensaje = request.Mensaje;
        dbNotification.FechaHora = request.FechaHora;
        dbNotification.Notificado = request.Notificado? (ulong)(1): (ulong)(0);

        response.Data = new()
        {
            IdNotication = dbNotification.CveNotificaciones,
            IdUsuarios = idUsuario,
            Mensaje = dbNotification.Mensaje,
            Notificado = request.Notificado,
            FechaHora = dbNotification.FechaHora
        };

        return response;
    }

    public ServerResponse<NotificationResponse> PutNotified(int idNotication)
    {
        ServerResponse<NotificationResponse> response = new();

        var dbNotification = _context.Notificaciones.Where(n => n.CveNotificaciones == idNotication).FirstOrDefault();

        if (dbNotification == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ninguna notificacion del usuario";

            return response;
        }

        dbNotification.Notificado = (ulong)(1);
        _context.Entry(dbNotification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
        
        response.Data = new()
        {
            IdNotication = dbNotification.CveNotificaciones,
            IdUsuarios = dbNotification.CveUsuarios,
            Mensaje = dbNotification.Mensaje!,
            FechaHora = dbNotification.FechaHora,
            Notificado = true
        };

        return response;
    }
}