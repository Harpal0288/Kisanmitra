using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_DiscussionForum")]
public partial class TbDiscussionForum
{
    [Key]
    [Column("forum_id")]
    [StringLength(255)]
    public string ForumId { get; set; } = null!;

    [Column("farmer_id")]
    [StringLength(255)]
    public string FarmerId { get; set; } = null!;

    [Column("forum_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string ForumName { get; set; } = null!;

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
    [InverseProperty("TbDiscussionForums")]
    public virtual TbFarmer Farmer { get; set; } = null!;

    [InverseProperty("Forum")]
    public virtual ICollection<TbForumPost> TbForumPosts { get; set; } = new List<TbForumPost>();
}
