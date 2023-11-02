using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("persona")]
public partial class Persona
{
    [Key]
    [Column("cc")]
    public int Cc { get; set; }

    [Column("nombre")]
    [StringLength(45)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("apellido")]
    [StringLength(45)]
    [Unicode(false)]
    public string Apellido { get; set; } = null!;

    [Column("genero")]
    [StringLength(1)]
    [Unicode(false)]
    public string Genero { get; set; } = null!;

    [Column("edad")]
    public int? Edad { get; set; }

    [InverseProperty("CcPerNavigation")]
    public virtual ICollection<Estudio> Estudios { get; set; } = new List<Estudio>();

    [InverseProperty("DuenioNavigation")]
    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
