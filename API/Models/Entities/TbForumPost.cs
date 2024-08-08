using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_ForumPost")]
public partial class TbForumPost
{
    [Key]
    [Column("post_id")]
    [StringLength(255)]
    public string PostId { get; set; } = null!;

    [Column("forum_id")]
    [StringLength(255)]
    public string ForumId { get; set; } = null!;

    [Column("farmer_id")]
    [StringLength(255)]
    public string FarmerId { get; set; } = null!;

    [Column("post_text")]
    [StringLength(1500)]
    public string PostText { get; set; } = null!;

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

    [ForeignKey("FarmerId")]
    [InverseProperty("TbForumPosts")]
    public virtual TbFarmer Farmer { get; set; } = null!;

    [ForeignKey("ForumId")]
    [InverseProperty("TbForumPosts")]
    public virtual TbDiscussionForum Forum { get; set; } = null!;
}
