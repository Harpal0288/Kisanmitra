using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_TempAnswer")]
public partial class TbTempAnswer
{
    [Key]
    [Column("answer_id")]
    [StringLength(255)]
    public string AnswerId { get; set; } = null!;

    [Column("query_id")]
    [StringLength(255)]
    public string? QueryId { get; set; }

    [Column("consultant_id")]
    [StringLength(255)]
    public string? ConsultantId { get; set; }

    [Column("answer_text")]
    [StringLength(1000)]
    public string? AnswerText { get; set; }

    [Column("inserted_by")]
    [StringLength(255)]
    public string InsertedBy { get; set; } = null!;

    [Column("updated_by")]
    [StringLength(255)]
    public string UpdatedBy { get; set; } = null!;

    [Column("time_stamp")]
    [StringLength(255)]
    public string TimeStamp { get; set; } = null!;
}
