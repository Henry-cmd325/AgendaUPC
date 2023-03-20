using AgendaUpc.Context;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;

namespace AgendaUpc.Services;

public class SubjectService : ISubjectService
{
    private readonly AgendaUpcContext _context;
    public SubjectService(AgendaUpcContext context)
    {
        _context = context;
    }
    public bool Delete(int id)
    {
        var dbSubject = _context.Materias.Where(m => m.IdMateria == id).FirstOrDefault();

        if (dbSubject == null) return false;

        _context.Materias.Remove(dbSubject);
        _context.SaveChanges();

        return true;
    }

    public ServerResponse<SubjectResponse> Get(int id)
    {
        ServerResponse<SubjectResponse> response = new();

        var dbSubject = _context.Materias.Where(m => m.IdMateria == id).FirstOrDefault();

        if (dbSubject == null) 
        {
            response.Success = false;
            response.Error = "El id introducido no corresponde con ninguna materia";

            return response;
        }

        response.Data = new()
        {
            IdMateria = dbSubject.IdMateria,
            Nombre = dbSubject.Nombre,
            Grupo = dbSubject.Grupo,
            Generacion = dbSubject.Generacion
        };

        return response;
    }

    public ServerResponse<List<SubjectResponse>> GetAll()
    {
        ServerResponse<List<SubjectResponse>> response = new();

        var dbSubjects = _context.Materias.ToList();
        response.Data = new();

        foreach(var subject in dbSubjects)
        {
            response.Data.Add
            (
                new() 
                {
                    IdMateria = subject.IdMateria,
                    Nombre = subject.Nombre,
                    Grupo = subject.Grupo,
                    Generacion = subject.Generacion
                }
            );
        }

        return response;
    }

    public ServerResponse<List<SubjectResponse>> GetAll(int idUsuario)
    {
        ServerResponse<List<SubjectResponse>> response = new();

        var dbMateriasUsuarios = _context.MateriasUsuarios.Where(m => m.IdUsuario == idUsuario).ToList();
        response.Data = new();

        foreach(var materiaUsuario in dbMateriasUsuarios)
        {
            var dbMateria = _context.Materias.Where(m => m.IdMateria == materiaUsuario.IdMateria).FirstOrDefault();

            if (dbMateria != null && materiaUsuario != null)
            {
                response.Data.Add(new() 
                {
                    IdMateria = dbMateria.IdMateria,
                    Nombre = dbMateria.Nombre,
                    Grupo = dbMateria.Grupo,
                    Generacion = dbMateria.Generacion
                });
            }
        }

        return response;
    }

    public ServerResponse<int> GetIdByName(string nombre)
    {
        ServerResponse<int> response = new();

        var dbSubject = _context.Materias.Where(m => m.Nombre == nombre).FirstOrDefault();

        if (dbSubject == null)
        {
            response.Success = false;
            response.Error = "El nombre introducido no corresponde con ninguna materia registrada";

            return response;
        }

        response.Data = dbSubject.IdMateria;

        return response;
    }

    public ServerResponse<SubjectResponse> Post(int idUsuario, SubjectRequest request)
    {
        ServerResponse<SubjectResponse> response = new();

        var newSubject = new Materia()
        {
            Nombre = request.Nombre,
            Generacion = request.Generacion,
            Grupo = request.Grupo
        };

        _context.Materias.Add(newSubject);
        _context.SaveChanges();

        var newMateriaUsuario = new MateriasUsuario()
        {
            IdMateria = newSubject.IdMateria,
            IdUsuario = idUsuario
        };

        _context.MateriasUsuarios.Add(newMateriaUsuario);
        _context.SaveChanges();
        
        response.Data = new()
        {
            IdMateria = newSubject.IdMateria,
            Nombre = newSubject.Nombre,
            Generacion = newSubject.Generacion,
            Grupo = newSubject.Grupo
        };

        return response;
    }

    public ServerResponse<SubjectResponse> Post(SubjectRequest request)
    {
        throw new NotImplementedException();
    }

    public ServerResponse<SubjectResponse> Put(SubjectRequest request, int id)
    {
        ServerResponse<SubjectResponse> response = new();

        var dbSubject = _context.Materias.Where(m => m.IdMateria == id).FirstOrDefault();

        if (dbSubject == null)
        {
            response.Success = false;
            response.Error = "El id introducido no correspondía con ninguna materia";

            return response;
        }

        dbSubject.Nombre = request.Nombre;

        _context.Entry(dbSubject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        response.Data = new()
        {
            IdMateria = dbSubject.IdMateria,
            Nombre = dbSubject.Nombre,
            Generacion = dbSubject.Generacion,
            Grupo = dbSubject.Grupo
        };

        return response;
    }

    public ServerResponse<SubjectResponse> Put(SubjectResponse request)
    {
        ServerResponse<SubjectResponse> response = new();

        var dbSubject = _context.Materias.Where(m => m.IdMateria == request.IdMateria).FirstOrDefault();

        if (dbSubject == null)
        {
            response.Success = false;
            response.Error = "El id introducido no correspondía con ninguna materia";

            return response;
        }

        dbSubject.Nombre = request.Nombre;
        
        _context.Entry(dbSubject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        response.Data = new()
        {
            IdMateria = dbSubject.IdMateria,
            Nombre = dbSubject.Nombre,
            Generacion = dbSubject.Generacion,
            Grupo = dbSubject.Grupo
        };

        return response;
    }
}