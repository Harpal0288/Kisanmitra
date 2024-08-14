using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[PrimaryKey("EquipmentId", "FarmerId")]
[Table("tb_FarmerEquipment")]
public partial class TbFarmerEquipment
{
    [Key]
    [Column("equipment_id")]
    [StringLength(255)]
    public string EquipmentId { get; set; } = null!;

    [Key]
    [Column("farmer_id")]
    [StringLength(255)]
    public string FarmerId { get; set; } = null!;

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime InsertedDate { get; set; }

    [Column("inserted_by")]
    [StringLength(255)]
    public string? InsertedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string? UpdatedBy { get; set; } = null!;

    [ForeignKey("EquipmentId")]
    [InverseProperty("TbFarmerEquipments")]
    [JsonIgnore]
    public virtual TbEquipment? Equipment { get; set; } = null!;

    [ForeignKey("FarmerId")]
    [InverseProperty("TbFarmerEquipments")]
    [JsonIgnore]
    public virtual TbFarmer? Farmer { get; set; } = null!;
}
