using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Query")]
public partial class TbQuery
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

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime InsertedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string UpdatedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [Column("time_stamp", TypeName = "datetime")]
    public DateTime? TimeStamp { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TbQueries")]
    public virtual TbCategory? Category { get; set; }

    [ForeignKey("FarmerId")]
    [InverseProperty("TbQueries")]
    public virtual TbFarmer? Farmer { get; set; } = null!;

    [InverseProperty("Query")]
    public virtual ICollection<TbAnswer>? TbAnswers { get; set; } = new List<TbAnswer>();

    [InverseProperty("Query")]
    public virtual ICollection<TbRating>? TbRatings { get; set; } = new List<TbRating>();
}
