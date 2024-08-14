using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[PrimaryKey("FarmerId", "FarmerResource")]
[Table("tb_FarmerLibraryResource")]
public partial class TbFarmerLibraryResource
{
    [Key]
    [Required]
    [Column("farmer_id")]
    [StringLength(255)]
    public string? FarmerId { get; set; } = null!;

    [Key]
    [Required]
    [Column("farmer_resource")]
    [StringLength(255)]
    public string ?FarmerResource { get; set; } = null!;

    [Column("inserted_by")]
    [StringLength(255)]
    [Required]
    public string? InsertedBy { get; set; } = null!;

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime ?InsertedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    [Required]
    public string ?UpdatedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime ?UpdatedDate { get; set; }

    [ForeignKey("FarmerId")]
    [InverseProperty("TbFarmerLibraryResources")]
    [JsonIgnore]
    public virtual TbFarmer? Farmer { get; set; } = null!;
}
