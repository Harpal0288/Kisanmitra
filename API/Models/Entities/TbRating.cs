using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Rating")]
public partial class TbRating
{
    [Key]
    [Column("rating_id")]
    [StringLength(255)]
    public string RatingId { get; set; } = null!;

    [Column("query_id")]
    [StringLength(255)]
    public string QueryId { get; set; } = null!;

    [Column("user_id")]
    [StringLength(255)]
    public string UserId { get; set; } = null!;

    [Column("rating_value")]
    public int? RatingValue { get; set; }

    [Column("rating_comment")]
    [StringLength(255)]
    public string? RatingComment { get; set; }

    [ForeignKey("QueryId")]
    [InverseProperty("TbRatings")]
    public virtual TbQuery Query { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TbRatings")]
    public virtual TbUser User { get; set; } = null!;
}
