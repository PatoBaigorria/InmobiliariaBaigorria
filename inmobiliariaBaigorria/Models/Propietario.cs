using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class Propietario
{
    [Key]
    [Display(Name = "Código Interno")]
    public int Id { get; set; }

    [Required]
    public string Dni { get; set; } = "";

    [Required]
    public string Nombre { get; set; } = "";

    [Required]
    public string Apellido { get; set; } = "";

    [Required, Display(Name = "Dirección")]
    public string Direccion { get; set; } = "";

    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    public override string ToString()
    {
        //return $"{Nombre} {Apellido}";
        var res = $"{Nombre} {Apellido}";
        if (!String.IsNullOrEmpty(Dni))
        {
            res += $" ({Dni})";
        }
        return res;
    }

}
