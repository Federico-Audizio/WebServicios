using WebServicios.Models;
using Microsoft.EntityFrameworkCore;
using WebServicios.Servicios.Contrato;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebServicios.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly WebServiciosContext _webServiciosContext;

        public UsuarioService(WebServiciosContext webServiciosContext)
        {
            _webServiciosContext = webServiciosContext;
        }
        public async Task<Usuario> GetUsuario(string correo, string contreaseña)
        {
            Usuario usuario_encontrado = await _webServiciosContext.Usuarios.Where(u => u.Correo == correo && u.Contraseña == contreaseña)
                .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {

            _webServiciosContext.Usuarios.Add(modelo);
            await _webServiciosContext.SaveChangesAsync();
            return modelo;
        }
    }
}
