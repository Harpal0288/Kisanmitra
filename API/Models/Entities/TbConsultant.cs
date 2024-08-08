using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Consultants")]
public partial class TbConsultant
{
    [Column("user_id")]
    [StringLength(255)]
    public string? UserId { get; set; }

    [Key]
    [Column("consultant_id")]
    [StringLength(255)]
    public string ConsultantId { get; set; } = null!;

    [Column("expertise")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Expertise { get; set; }

    [Column("experience")]
    public int? Experience { get; set; }

    [Column("subscription_status")]
    [StringLength(100)]
    [Unicode(false)]
    public string SubscriptionStatus { get; set; } = null!;

    [Column("subscription_expiry", TypeName = "datetime")]
    public DateTime? SubscriptionExpiry { get; set; }

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime InsertedDate { get; set; }

    [Column("inserted_by")]
    [StringLength(255)]
    public string InsertedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string UpdatedBy { get; set; } = null!;

    [InverseProperty("Consultant")]
    public virtual ICollection<TbAnswer> TbAnswers { get; set; } = new List<TbAnswer>();

    [InverseProperty("Consultant")]
    public virtual ICollection<TbConsultantCertification> TbConsultantCertifications { get; set; } = new List<TbConsultantCertification>();

    [InverseProperty("Consultant")]
    public virtual ICollection<TbConsultantLanguage> TbConsultantLanguages { get; set; } = new List<TbConsultantLanguage>();

    [ForeignKey("UserId")]
    [InverseProperty("TbConsultants")]
    public virtual TbUser? User { get; set; }
}
