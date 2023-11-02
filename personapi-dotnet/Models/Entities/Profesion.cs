using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("profesion")]
public partial class Profesion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(90)]
    [Unicode(false)]
    public string Nom { get; set; } = null!;

    [Column("des", TypeName = "text")]
    public string? Des { get; set; }

    [InverseProperty("IdProfNavigation")]
    public virtual ICollection<Estudio> Estudios { get; set; } = new List<Estudio>();
}
