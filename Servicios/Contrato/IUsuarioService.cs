using WebServicios.Models;
using Microsoft.EntityFrameworkCore;

namespace WebServicios.Servicios.Contrato
{
    public interface IUsuarioService
    {

        Task<Usuario> GetUsuario(string correo, string clave);

        Task<Usuario> SaveUsuario(Usuario modelo);
    }
}
