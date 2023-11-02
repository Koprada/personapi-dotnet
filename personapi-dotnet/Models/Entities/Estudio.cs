using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[PrimaryKey("IdProf", "CcPer")]
[Table("estudios")]
public partial class Estudio
{
    [Key]
    [Column("id_prof")]
    public int IdProf { get; set; }

    [Key]
    [Column("cc_per")]
    public int CcPer { get; set; }

    [Column("fecha", TypeName = "date")]
    public DateTime? Fecha { get; set; }

    [Column("univer")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Univer { get; set; }

    [ForeignKey("CcPer")]
    [InverseProperty("Estudios")]
    public virtual Persona CcPerNavigation { get; set; } = null!;

    [ForeignKey("IdProf")]
    [InverseProperty("Estudios")]
    public virtual Profesion IdProfNavigation { get; set; } = null!;
}
