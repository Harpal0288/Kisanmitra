using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Categories")]
public partial class TbCategory
{
    [Key]
    [Column("category_id")]
    [StringLength(255)]
    public string CategoryId { get; set; } = null!;

    [Column("category_name")]
    [StringLength(255)]
    public string CategoryName { get; set; } = null!;

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

    [InverseProperty("Category")]
    public virtual ICollection<TbQuery> TbQueries { get; set; } = new List<TbQuery>();
}
