using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Answer")]
public partial class TbAnswer
{
    [Key]
    [Column("answer_id")]
    [StringLength(255)]
    public string AnswerId { get; set; } = null!;

    [Column("query_id")]
    [StringLength(255)]
    public string? QueryId { get; set; }

    [Column("answer_text")]
    [StringLength(1000)]
    public string AnswerText { get; set; } = null!;

    [Column("consultant_id")]
    [StringLength(255)]
    public string? ConsultantId { get; set; }

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

    [Column("time_stamp", TypeName = "datetime")]
    public DateTime TimeStamp { get; set; }

    [ForeignKey("ConsultantId")]
    [InverseProperty("TbAnswers")]
    public virtual TbConsultant? Consultant { get; set; }

    [ForeignKey("QueryId")]
    [InverseProperty("TbAnswers")]
    public virtual TbQuery? Query { get; set; }
}
