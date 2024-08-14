using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[PrimaryKey("FarmarId", "Crop")]
[Table("tb_FarmerCrop")]
public partial class TbFarmerCrop
{
    [Key]
    [Column("farmer_id")]
    [StringLength(255)]
    public string FarmarId { get; set; } = null!;

    [Key]
    [Column("crop")]
    [StringLength(255)]
    public string Crop { get; set; } = null!;

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

    [ForeignKey("FarmarId")]
    [InverseProperty("TbFarmerCrops")]
    public virtual TbFarmer Farmar { get; set; } = null!;
}
