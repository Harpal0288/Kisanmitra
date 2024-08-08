using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Role")]
[Index("RoleName", Name = "UQ__tb_Role__783254B1C806BEB0", IsUnique = true)]
public partial class TbRole
{
    [Key]
    [Column("role_id")]
    [StringLength(255)]
    public string RoleId { get; set; } = null!;

    [Column("role_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

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

    [InverseProperty("Role")]
    public virtual ICollection<TbUser> TbUsers { get; set; } = new List<TbUser>();
}
