using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Equipment")]
public partial class TbEquipment
{
    [Key]
    [Column("equipment_id")]
    [StringLength(255)]
    public string EquipmentId { get; set; } = null!;

    [Column("equipment_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string EquipmentName { get; set; } = null!;

    [Column("equipment_type")]
    [StringLength(255)]
    [Unicode(false)]
    public string EquipmentType { get; set; } = null!;

    [Column("equipment_description")]
    [StringLength(1000)]
    public string? EquipmentDescription { get; set; }

    [Column("rent_price")]
    public int RentPrice { get; set; }

    [Column("availability_status")]
    public byte AvailabilityStatus { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

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

    [InverseProperty("Equipment")]
    public virtual ICollection<TbFarmerEquipment> TbFarmerEquipments { get; set; } = new List<TbFarmerEquipment>();

    [InverseProperty("Equipment")]
    public virtual ICollection<TbRental> TbRentals { get; set; } = new List<TbRental>();
}
