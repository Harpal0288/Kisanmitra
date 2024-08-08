using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[PrimaryKey("ConsultantId", "ConsultantLanguage")]
[Table("tb_ConsultantLanguage")]
public partial class TbConsultantLanguage
{
    [Key]
    [Column("consultant_id")]
    [StringLength(255)]
    public string ConsultantId { get; set; } = null!;

    [Key]
    [Column("consultant_language")]
    [StringLength(50)]
    public string ConsultantLanguage { get; set; } = null!;

    [Column("inserted_by")]
    [StringLength(255)]
    public string InsertedBy { get; set; } = null!;

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime InsertedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string UpdatedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [ForeignKey("ConsultantId")]
    [InverseProperty("TbConsultantLanguages")]
    public virtual TbConsultant Consultant { get; set; } = null!;
}
