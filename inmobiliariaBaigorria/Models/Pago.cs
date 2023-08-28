using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class Pago
{
    [Key]
    [Display(Name = "Nº Comprobante")]
    public int Id { get; set; }

    [Required, Display(Name = "Nº de Pago")]
    public int NumeroDePago { get; set; }

    [Required]
    public DateOnly FechaDePago { get; set; }

    [Required]
    public decimal Importe { get; set; }

    [Display(Name = "Contrato Nº")]
    public int ContratoId { get; set; }

    [ForeignKey(nameof(ContratoId))]
    public Contrato? Contrato { get; set; }

}