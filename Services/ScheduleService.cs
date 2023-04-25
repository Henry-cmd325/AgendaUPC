using AgendaUpc.Context;
using AgendaUpc.Models.Responses;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.ViewModels;

namespace AgendaUpc.Services;

public class ScheduleService : IScheduleService
{
    private readonly AgendaUpcContext _context;
    public ScheduleService(AgendaUpcContext context)
    {
        _context = context;
    }

    public ServerResponse<ScheduleResponse> DeleteNameSchedule(DeleteRequest request)
    {
        ServerResponse<ScheduleResponse> response = new();

        var dbSchedule = _context.HorarioMaterias.Where(h => h.IdHorariosMaterias == request.Id).FirstOrDefault();

        if (dbSchedule == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ningun registro";

            return response;
        }

        dbSchedule.IdMateria = null;

        _context.Entry(dbSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        return response;
    }

    public bool DeleteSchedule(int id)
    {
        var dbSchedule = _context.HorarioMaterias.Where(h => h.IdHorariosMaterias == id).FirstOrDefault();

        if (dbSchedule == null) return false;

        _context.HorarioMaterias.Remove(dbSchedule);
        _context.SaveChanges();

        return true;
    }

    public ServerResponse<List<ScheduleResponse>> GetAllSchedule(int idUsuario)
    {
        ServerResponse<List<ScheduleResponse>> response = new();

        var dbUsuario = _context.Usuarios.Where(u => u.IdUsuario == idUsuario).FirstOrDefault();

        if (dbUsuario == null)
        {
            response.Success = false;
            response.Error = "El id no coincide con ningun registro";

            return response;
        }

        var listSchedules = _context.HorarioMaterias.Where(h => h.IdUsuario == idUsuario).ToList();
        response.Data = new();

        foreach(var schedule in listSchedules)
        {
            var dbDia = _context.Dias.Where(d => d.IdDia == schedule.IdDia).FirstOrDefault();
            var dbMateria = _context.Materias.Where(m => m.IdMateria == schedule.IdMateria).FirstOrDefault();

            response.Data.Add(new() 
            {
                IdSchedule = schedule.IdHorariosMaterias,
                Dia = dbDia!.Nombre,
                Materia = dbMateria == null? " ": dbMateria.Nombre,
                Hora = schedule.Hora
            });
        }
        return response;
    }

    public ServerResponse<ScheduleResponse> GetSchedule(int id)
    {
        ServerResponse<ScheduleResponse> response = new();

        var dbSchedule = _context.HorarioMaterias.Where(h => h.IdHorariosMaterias == id).FirstOrDefault();

        if (dbSchedule == null)
        {
            response.Success = false;
            response.Error = "El id introducido no coincide con ningun schedule";

            return response;
        }

        var dbDia = _context.Dias.Where(d => d.IdDia == dbSchedule.IdDia).FirstOrDefault();
        var dbMateria = _context.Materias.Where(m => m.IdMateria == dbSchedule.IdMateria).FirstOrDefault();

        if (dbDia == null || dbMateria == null)
        {
            response.Success = false;
            response.Error = "Algunas de las llaves foraneas no coinciden con ningun registro";

            return response;
        }

        response.Data = new()
        {
            IdSchedule = dbSchedule.IdHorariosMaterias,
            Dia = dbDia.Nombre,
            Materia = dbMateria.Nombre,
            Hora = dbSchedule.Hora
        };

        return response;
    }

    public ServerResponse<ScheduleResponse> PostSchedule(ScheduleRequest request)
    {
        ServerResponse<ScheduleResponse> response = new();

        var newSchedule = new HorarioMateria()
        {
            IdDia = request.IdDia,
            IdMateria = request.IdMateria,
            Hora = request.Hora
        };

        _context.HorarioMaterias.Add(newSchedule);
        _context.SaveChanges();

        var dbDia = _context.Dias.Where(d => d.IdDia == request.IdDia).FirstOrDefault();
        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbDia == null || dbMateria == null)
        {
            response.Success = false;
            response.Error = "Algunas de las llaves foraneas no coinciden con ningun registro";

            return response;
        }

        response.Data = new()
        {
            IdSchedule = newSchedule.IdHorariosMaterias,
            Dia = dbDia.Nombre,
            Materia = dbMateria.Nombre,
            Hora = newSchedule.Hora
        };

        return response;
    }

    public ServerResponse<ScheduleResponse> UpdateSchedule(int id, ScheduleRequest request)
    {
        ServerResponse<ScheduleResponse> response = new();

        var dbSchedule = _context.HorarioMaterias.Where(h => h.IdHorariosMaterias == id).FirstOrDefault();

        if (dbSchedule == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresonde con ningun registro";

            return response;
        }

        dbSchedule.IdDia = request.IdDia;
        dbSchedule.IdMateria = request.IdMateria;
        dbSchedule.Hora = request.Hora;

        _context.Entry(dbSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        var dbDia = _context.Dias.Where(d => d.IdDia == request.IdDia).FirstOrDefault();
        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbDia == null || dbMateria == null)
        {
            response.Success = false;
            response.Error = "Algunas de las llaves foraneas no coinciden con ningun registro";

            return response;
        }

        response.Data = new()
        {
            IdSchedule = dbSchedule.IdHorariosMaterias,
            Dia = _context.Dias.Where(d => d.IdDia == dbSchedule.IdDia).FirstOrDefault()!.Nombre,
            Materia = _context.Materias.Where(m => m.IdMateria == dbSchedule.IdMateria).FirstOrDefault()!.Nombre,
            Hora = dbSchedule.Hora
        };
       
        return response;
    }

    public ServerResponse<ScheduleResponse> UpdateSchedule(ScheduleResponse request)
    {
        ServerResponse<ScheduleResponse> response = new();

        var dbSchedule = _context.HorarioMaterias.Where(h => h.IdHorariosMaterias == request.IdSchedule).FirstOrDefault();

        if (dbSchedule == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresonde con ningun registro";

            return response;
        }

        var dbDia = _context.Dias.Where(d => d.Nombre == request.Dia).FirstOrDefault();

        if (dbDia == null)
        {
            response.Success = false;
            response.Error = "El id introducido para el dia no corresponde con ningun registro";

            return response;
        }

        var dbMateria = _context.Materias.Where(m => m.Nombre == request.Materia).FirstOrDefault();

        if (dbMateria == null)
        {
            response.Success = false;
            response.Error = "El id introducio para la materia no corresponde con ningun registro";

            return response;
        }

        dbSchedule.IdDia = dbDia.IdDia;
        dbSchedule.IdMateria = dbMateria.IdMateria;
        dbSchedule.Hora = request.Hora;

        _context.Entry(dbSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        response.Data = new()
        {
            IdSchedule = dbSchedule.IdHorariosMaterias,
            Dia = dbDia.Nombre,
            Materia = dbMateria.Nombre,
            Hora = dbSchedule.Hora
        };
       
        return response;
    }

    public ServerResponse<ScheduleResponse> UpdateSchedule(UpdateSchedule request)
    {
        ServerResponse<ScheduleResponse> response = new();

        var dbSchedule = _context.HorarioMaterias.Where(h => h.IdHorariosMaterias == request.IdSchedule).FirstOrDefault();

        if (dbSchedule == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresonde con ningun registro";

            return response;
        }

        dbSchedule.IdMateria = request.IdMateria;

        _context.Entry(dbSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        var dbDia = _context.Dias.Where(d => d.IdDia == dbSchedule.IdDia).FirstOrDefault();

        if (dbDia == null)
        {
            response.Success = false;
            response.Error = "El id introducido para el dia no corresponde con ningun registro";

            return response;
        }

        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        response.Data = new()
        {
            IdSchedule = dbSchedule.IdHorariosMaterias,
            Dia = dbDia.Nombre,
            Materia = dbMateria == null? "": dbMateria.Nombre,
            Hora = dbSchedule.Hora
        };
       
        return response;
    }
}