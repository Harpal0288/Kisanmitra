using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Rental")]
public partial class TbRental
{
    [Key]
    [Column("rental_id")]
    [StringLength(255)]
    public string RentalId { get; set; } = null!;

    [Column("equipment_id")]
    [StringLength(255)]
    public string EquipmentId { get; set; } = null!;

    [Column("rental_start_date")]
    public DateOnly RentalStartDate { get; set; }

    [Column("rental_end_date")]
    public DateOnly RentalEndDate { get; set; }

    [Column("total_cost")]
    public int TotalCost { get; set; }

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

    [ForeignKey("EquipmentId")]
    [InverseProperty("TbRentals")]
    public virtual TbEquipment Equipment { get; set; } = null!;
}
