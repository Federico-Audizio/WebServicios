using System;
using System.Collections.Generic;

namespace WebServicios.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Contraseña { get; set; }
}
