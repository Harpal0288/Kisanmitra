using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Admin")]
public partial class TbAdmin
{
    [Key]
    [Column("admin_id")]
    [StringLength(255)]
    public string AdminId { get; set; } = null!;

    [Column("user_id")]
    [StringLength(255)]
    public string? UserId { get; set; }

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

    [ForeignKey("UserId")]
    [InverseProperty("TbAdmins")]
    public virtual TbUser? User { get; set; }
}
