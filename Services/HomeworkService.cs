using AgendaUpc.Context;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;
using AgendaUpc.Models.ViewModels;

namespace AgendaUpc.Services;

public class HomeworkService : IHomeworkService
{
    private readonly AgendaUpcContext _context;
    public HomeworkService(AgendaUpcContext context)
    {
       _context = context; 
    }

    public ServerResponse<HomeworkResponse> CompletarTarea(int idTarea)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == idTarea).FirstOrDefault();

        if (dbHomework == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ningun registro";

            return response;
        }

        dbHomework.Terminada = 1;

        _context.Entry(dbHomework).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        return response;
    }

    public bool Delete(int id)
    {
        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == id).FirstOrDefault();

        if (dbHomework == null)
            return false;
        
        _context.TareasUnicas.Remove(dbHomework);
        _context.SaveChanges();

        return true;
    }

    public ServerResponse<HomeworkResponse> DesmarcarTarea(int idTarea)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == idTarea).FirstOrDefault();

        if (dbHomework == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ningun registro";

            return response;
        }

        dbHomework.Terminada = 0;

        _context.Entry(dbHomework).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        return response;
    }

    public ServerResponse<HomeworkResponse> Get(int id)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == id).FirstOrDefault();

        if (dbHomework == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ningun registro";

            return response;
        }

        HomeworkResponse newHomework = new()
        {
            IdTarea = id,
            Materia = _context.Materias.Where(m => m.IdMateria == dbHomework.IdMateria).FirstOrDefault()!.Nombre,
            Nombre = dbHomework.Nombre,
            Descripcion = dbHomework.Descripcion,
            FechaLimite = dbHomework.FechaEntrega
        };

        return response;
    }

    public ServerResponse<HomeworkResponse> Get(int idTarea, int idUsuario)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == idTarea && t.IdUsuario == idUsuario).FirstOrDefault();

        if (dbHomework == null)
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ningun registro o no pertenece al usuario";

            return response;
        }

        HomeworkResponse newHomework = new()
        {
            IdTarea = dbHomework.IdUnica,
            Materia = _context.Materias.Where(m => m.IdMateria == dbHomework.IdMateria).FirstOrDefault()!.Nombre,
            Nombre = dbHomework.Nombre,
            Descripcion = dbHomework.Descripcion,
            FechaLimite = dbHomework.FechaEntrega
        };

        response.Data = newHomework;
        
        return response;
    }

    public ServerResponse<List<HomeworkResponse>> GetAll()
    {
        ServerResponse<List<HomeworkResponse>> response = new();

        var dbList = _context.TareasUnicas.ToList();

        var Homeworks = new List<HomeworkResponse>();

        foreach(var homework in dbList)
        {
            Homeworks.Add(new()
            {
                IdTarea = homework.IdUnica,
                Materia = _context.Materias.Where(m => m.IdMateria == homework.IdMateria).FirstOrDefault()!.Nombre,
                Nombre = homework.Nombre,
                Descripcion = homework.Descripcion,
                FechaLimite = homework.FechaEntrega
            });
        }

        response.Data = Homeworks;

        return response;
    }

    public ServerResponse<List<HomeworkResponse>> GetAll(int idUsuario)
    {
        ServerResponse<List<HomeworkResponse>> response = new();

        var dbList = _context.TareasUnicas.Where(t => t.IdUsuario == idUsuario && t.Terminada == 0).ToList();

        var homeworks = new List<HomeworkResponse>();

        foreach(var homework in dbList)
        {
            if (homework != null)
            {
                var dbMateria = _context.Materias.Where(m => m.IdMateria == homework.IdMateria).FirstOrDefault();

                if (dbMateria != null)
                {
                    homeworks.Add(new() 
                    {
                        IdTarea = homework.IdUnica,
                        Materia = dbMateria.Nombre,
                        Nombre = homework.Nombre,
                        Descripcion = homework.Descripcion,
                        FechaLimite = homework.FechaEntrega
                    });
                } 
            }
        }

        homeworks = SortList(homeworks);

        response.Data = homeworks;

        return response;
    }

    private List<HomeworkResponse> SortList(List<HomeworkResponse> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int k = i + 1; k < list.Count; k++)
            {
                if (list[i].FechaLimite > list[k].FechaLimite)
                {
                    var homework = list[i];
                    list[i] = list[k];
                    list[k] = homework;
                }
            }
        }

        return list;
    }

    public ServerResponse<List<HomeworkResponse>> GetAllCompletadas(int idUsuario)
    {
        ServerResponse<List<HomeworkResponse>> response = new();

        var dbList = _context.TareasUnicas.Where(t => t.IdUsuario == idUsuario && t.Terminada == 1).ToList();
        response.Data = new();

        foreach(var homework in dbList)
        {
            if (homework != null)
            {
                var dbMateria = _context.Materias.Where(m => m.IdMateria == homework.IdMateria).FirstOrDefault();

                if (dbMateria != null)
                {
                    response.Data.Add(new() 
                    {
                        IdTarea = homework.IdUnica,
                        Materia = dbMateria.Nombre,
                        Nombre = homework.Nombre,
                        Descripcion = homework.Descripcion,
                        FechaLimite = homework.FechaEntrega
                    });
                }
            }
        }

        return response;    
    }

    public ServerResponse<HomeworkResponse> Post(HomeworkRequest request)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbMateria == null)
        {
            response.Success = false;
            response.Error = "El id materia introducido no corresponde con ningun registro";

            return response;
        }

        var newHomework = new TareasUnica() 
        {
            Nombre = request.Nombre,
            IdMateria = request.IdMateria,
            Descripcion = request.Descripcion,
            FechaEntrega = request.FechaLimite
        };

        _context.TareasUnicas.Add(newHomework);
        _context.SaveChanges(); 

        var homeworkResponse = new HomeworkResponse() 
        {
            IdTarea = newHomework.IdUnica,
            Materia = dbMateria.Nombre,
            Nombre = newHomework.Nombre,
            Descripcion = newHomework.Descripcion,
            FechaLimite = newHomework.FechaEntrega
        };

        response.Data = homeworkResponse;

        return response;
    }

    public ServerResponse<HomeworkResponse> Post(int idUsuario, HomeworkRequest request)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbMateria == null)
        {
            response.Success = false;
            response.Error = "El id materia introducido no corresponde con ningun registro";

            return response;
        }

        var newHomework = new TareasUnica() 
        {
            Nombre = request.Nombre,
            IdMateria = request.IdMateria,
            IdUsuario = idUsuario,
            Descripcion = request.Descripcion,
            FechaEntrega = request.FechaLimite
        };

        _context.TareasUnicas.Add(newHomework);
        _context.SaveChanges(); 

        var homeworkResponse = new HomeworkResponse() 
        {
            IdTarea = newHomework.IdUnica,
            Materia = dbMateria.Nombre,
            Nombre = newHomework.Nombre,
            Descripcion = newHomework.Descripcion,
            FechaLimite = newHomework.FechaEntrega
        };

        response.Data = homeworkResponse;

        return response;
    }

    public ServerResponse<HomeworkResponse> Put(HomeworkRequest request, int id)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == id).FirstOrDefault();

        if (dbHomework == null)
        {
            response.Success = false;
            response.Error = "El id tarea introducido no corresponde con ningun registro";

            return response;
        }

        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbMateria == null)
        {
            response.Success = false;
            response.Error = "El id materia introducido no corresponde con ningun registro";

            return response;
        }

        dbHomework.Nombre = request.Nombre;
        dbHomework.IdMateria = request.IdMateria;
        dbHomework.Descripcion = request.Descripcion;
        dbHomework.FechaEntrega = request.FechaLimite;

        _context.Entry(dbHomework).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();


        var homeworkResponse = new HomeworkResponse()
        {
            IdTarea = dbHomework.IdUnica,
            Materia = dbMateria.Nombre,
            Nombre = dbHomework.Nombre,
            Descripcion = dbHomework.Descripcion,
            FechaLimite = dbHomework.FechaEntrega
        };

        response.Data = homeworkResponse;
        
        return response;
    }

    public ServerResponse<HomeworkResponse> Put(UpdateHomework request)
    {
        ServerResponse<HomeworkResponse> response = new();

        var dbHomework = _context.TareasUnicas.Where(t => t.IdUnica == request.IdTarea).FirstOrDefault();

        if (dbHomework == null)
        {
            response.Success = false;
            response.Error = "El id tarea introducido no corresponde con ningun registro";

            return response;
        }

        var dbMateria = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbMateria == null)
        {
            response.Success = false;
            response.Error = "El id materia introducido no corresponde con ningun registro";

            return response;
        }

        dbHomework.Nombre = request.Nombre;
        dbHomework.IdMateria = request.IdMateria;
        dbHomework.Descripcion = request.Descripcion;
        dbHomework.FechaEntrega = request.FechaLimite;

        _context.Entry(dbHomework).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();


        var homeworkResponse = new HomeworkResponse()
        {
            IdTarea = dbHomework.IdUnica,
            Materia = dbMateria.Nombre,
            Nombre = dbHomework.Nombre,
            Descripcion = dbHomework.Descripcion,
            FechaLimite = dbHomework.FechaEntrega
        };

        response.Data = homeworkResponse;
        
        return response;
    }

    public ServerResponse<List<HomeworkResponse>> GetAll(int idUsuario, int idMateria)
    {
        ServerResponse<List<HomeworkResponse>> response = new();

        var dbList = _context.TareasUnicas.Where(t => t.IdUsuario == idUsuario && t.IdMateria == idMateria).ToList();

        var homeworks = new List<HomeworkResponse>();

        foreach(var homework in dbList)
        {
            if (homework != null)
            {
                var dbMateria = _context.Materias.Where(m => m.IdMateria == homework.IdMateria).FirstOrDefault();

                if (dbMateria != null)
                {
                    homeworks.Add(new() 
                    {
                        IdTarea = homework.IdUnica,
                        Materia = dbMateria.Nombre,
                        Nombre = homework.Nombre,
                        Descripcion = homework.Descripcion,
                        FechaLimite = homework.FechaEntrega
                    });
                } 
            }
        }

        return response;
    }
}

