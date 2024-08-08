using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_TempQuery")]
public partial class TbTempQuery
{
    [Key]
    [Column("query_id")]
    [StringLength(255)]
    public string QueryId { get; set; } = null!;

    [Column("category_id")]
    [StringLength(255)]
    public string? CategoryId { get; set; }

    [Column("farmer_id")]
    [StringLength(255)]
    public string FarmerId { get; set; } = null!;

    [Column("query_title")]
    [StringLength(255)]
    [Unicode(false)]
    public string QueryTitle { get; set; } = null!;

    [Column("query_description")]
    [StringLength(255)]
    public string? QueryDescription { get; set; }

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
