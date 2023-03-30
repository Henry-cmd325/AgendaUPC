using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AgendaUpc.Context;
using AgendaUpc.Models.Requests;
using AgendaUpc.Models.Responses;
using AgendaUpc.Tools;
using AgendaUpc.Models.ViewModels;

namespace AgendaUpc.Services;

public class UserService : IUserService
{
    private readonly AgendaUpcContext _context;
    private IConfiguration config;
    public UserService(AgendaUpcContext context, IConfiguration config)
    {
        _context = context;
        this.config = config;
    }
    public ServerResponse<UserResponse> CheckLogin(LoginRequest request)
    {
        ServerResponse<UserResponse> response = new();

        var pwd = Encrypt.GetSha256(request.Password);

        var dbUser = _context.Usuarios.Where(u => u.Nombre == request.Nombre && (u.ContraSiupc == pwd || u.ContraUpdc == pwd)).FirstOrDefault();

        if (dbUser == null)
        {
            response.Success = false;
            response.Error = "El usuario o la contraseña son incorrrectos";
            return response;
        }

        response.Data = new()
        {
            IdUsuario = dbUser.IdUsuario,
            Nombre = dbUser.Nombre,
            ContraSiupc = dbUser.ContraSiupc!,
            ContraUpdc = dbUser.ContraUpdc!
        };

        return response;
    }

    public bool Delete(int id)
    {
        var dbUser = _context.Usuarios.Where(u => u.IdUsuario == id).FirstOrDefault();

        if (dbUser == null)
        {
            return false;
        }

        _context.Remove(dbUser);
        _context.SaveChanges();

        return true;
    }
    
    public ServerResponse<UserResponse> Get(int id)
    {
        ServerResponse<UserResponse> response = new();

        var usuario = _context.Usuarios.Where(u => u.IdUsuario == id).FirstOrDefault();

        if (usuario == null)
        {
            response.Success = false;
            response.Error = "El id no coincide del usuario no coincide con ningun registro";

            return response;
        }

        response.Data = new UserResponse()
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = usuario.Nombre,
            Matricula = usuario.Matricula,
            ContraSiupc = usuario.ContraSiupc,
            ContraUpdc = usuario.ContraUpdc
        };

        return response;
    }

    public ServerResponse<List<UserResponse>> GetAll()
    {
        ServerResponse<List<UserResponse>> response = new();
        response.Data = new();

        var users = _context.Usuarios.ToList();
        
        foreach(var user in users)
        {
            response.Data.Add
            (
                new()
                {
                    IdUsuario = user.IdUsuario,
                    Nombre = user.Nombre,
                    Matricula = user.Matricula,
                    ContraSiupc = user.ContraSiupc,
                    ContraUpdc = user.ContraUpdc
                }
            );
        }

        return response;
    }

    public ServerResponse<UserResponse> Post(UserRequest request)
    {
        ServerResponse<UserResponse> response = new();

        var user = _context.Usuarios.Where(u => u.Nombre == request.Nombre).FirstOrDefault();

        if (user != null)
        {
            response.Success = false;
            response.Error = "El nombre de usuario introducido ya existe";

            return response;
        }

        var newUser = new Usuario()
        {
            Nombre = request.Nombre,
            Matricula = request.Matricula,
            ContraSiupc = Encrypt.GetSha256(request.ContraSiupc),
            ContraUpdc = Encrypt.GetSha256(request.ContraUpdc)
        };

        _context.Usuarios.Add(newUser);
        _context.SaveChanges();

        response.Data = new()
        {
            IdUsuario = newUser.IdUsuario,
            Nombre = newUser.Nombre,
            Matricula = newUser.Matricula,
            ContraSiupc = newUser.ContraSiupc,
            ContraUpdc = newUser.ContraUpdc
        };

        return response;
    }

    public ServerResponse<UserResponse> Put(UserRequest request, int id)
    {
        ServerResponse<UserResponse> response = new();

        var dbUser = _context.Usuarios.Where(u => u.IdUsuario == id).FirstOrDefault();

        if (dbUser == null)
        {
            response.Success = false;
            response.Error = "El id no coincide con ningun registro";

            return response;
        }

        dbUser.Nombre = request.Nombre;
        dbUser.ContraSiupc = Encrypt.GetSha256(request.ContraSiupc);
        dbUser.ContraUpdc = Encrypt.GetSha256(request.ContraUpdc);

        _context.Entry(dbUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        response.Data = new()
        {
            IdUsuario = id,
            Nombre = dbUser.Nombre,
            ContraSiupc = dbUser.ContraSiupc,
            ContraUpdc = dbUser.ContraUpdc
        };

        return response;
    }

    public ServerResponse<UserResponse> Post(SigninOutMoodle request)
    {
        ServerResponse<UserResponse> response = new();

        var user = _context.Usuarios.Where(u => u.Nombre == request.Nombre).FirstOrDefault();

        if (user != null)
        {
            response.Success = false;
            response.Error = "El nombre de usuario introducido ya existe";

            return response;
        }

        var newUser = new Usuario()
        {
            Nombre = request.Nombre,
            Matricula = request.Matricula,
            ContraSiupc = Encrypt.GetSha256(request.Contraseña),
        };

        _context.Usuarios.Add(newUser);
        _context.SaveChanges();

        //Creación de horario
        for (int i = 1; i <= 7; i++)
        {
            for (int j = 4; j <= 22; j++)
            {
                _context.HorarioMaterias.Add(new()
                {
                    IdUsuario = newUser.IdUsuario,
                    IdDia = i,
                    Hora = new TimeOnly(j, 0, 0)
                });

                _context.SaveChanges();
            }
        }

        response.Data = new()
        {
            IdUsuario = newUser.IdUsuario,
            Nombre = newUser.Nombre,
            Matricula = newUser.Matricula,
            ContraSiupc = newUser.ContraSiupc,
        };

        return response;
    }
}