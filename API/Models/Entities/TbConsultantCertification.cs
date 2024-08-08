using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_ConsultantCertification")]
public partial class TbConsultantCertification
{
    [Column("consultant_id")]
    [StringLength(255)]
    public string? ConsultantId { get; set; }

    [Key]
    [Column("certification_number")]
    [StringLength(255)]
    public string CertificationNumber { get; set; } = null!;

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
    [InverseProperty("TbConsultantCertifications")]
    public virtual TbConsultant? Consultant { get; set; }
}
