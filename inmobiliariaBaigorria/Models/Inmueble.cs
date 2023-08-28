using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class Inmueble
{
    [Key]
    [Display(Name = "Nº")]
    public int Id { get; set; }

    [Required]
    public string Tipo { get; set; } = "";

    [Required]
    public string Uso { get; set; } = "";

    [Required]
    public int CantidadDeAmbientes { get; set; }

    public decimal? Longitud { get; set; }

    public decimal? Latitud { get; set; }

    [Required]
    public decimal Precio { get; set; }

    [Display(Name = "Dueño")]
    public int PropietarioId { get; set; }

    [ForeignKey(nameof(PropietarioId))]
    public Propietario? Duenio { get; set; }

}