using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class Inquilino
{
    [Key]
    [Display(Name = "Código Interno")]
    public int Id { get; set; }

    [Required]
    public string Dni { get; set; } = "";

    [Required]
    public string NombreCompleto { get; set; } = "";

    [Required, Display(Name = "Dirección")]
    public string Direccion { get; set; } = "";

    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }
    public override string ToString()
    {
        return $"{NombreCompleto}";
    }

}