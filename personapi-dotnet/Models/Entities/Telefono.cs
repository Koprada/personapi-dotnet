using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("telefono")]
public partial class Telefono
{
    [Key]
    [Column("num")]
    [StringLength(15)]
    [Unicode(false)]
    public string Num { get; set; } = null!;

    [Column("oper")]
    [StringLength(45)]
    [Unicode(false)]
    public string Oper { get; set; } = null!;

    [Column("duenio")]
    public int Duenio { get; set; }

    [ForeignKey("Duenio")]
    [InverseProperty("Telefonos")]
    public virtual Persona DuenioNavigation { get; set; } = null!;
}
