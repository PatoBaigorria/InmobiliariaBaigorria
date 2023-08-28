using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class Contrato
{
    [Key]
    [Display(Name = "Nº")]
    public int Id { get; set; }

    [Required]
    public DateOnly FechaInicio { get; set; }

    [Required]
    public DateOnly FechaFin { get; set; }

    [Required]
    public decimal MontoAlquiler { get; set; }

    [Display(Name = "Código Inquilino")]
    public int InquilinoId { get; set; }

    [ForeignKey(nameof(InquilinoId))]
    public Inquilino? Inquilino { get; set; }

    [Display(Name = "Código Inmueble")]
    public int InmuebleId { get; set; }

    [ForeignKey(nameof(InmuebleId))]
    public Inmueble? Inmueble { get; set; }
}