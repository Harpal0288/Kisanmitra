using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Payment")]
public partial class TbPayment
{
    [Key]
    [Column("payment_id")]
    [StringLength(255)]
    public string PaymentId { get; set; } = null!;

    [Column("user_id")]
    [StringLength(255)]
    public string UserId { get; set; } = null!;

    [Column("amount")]
    public int Amount { get; set; }

    [Column("gateway_id")]
    [StringLength(255)]
    public string GatewayId { get; set; } = null!;

    [Column("payment_date", TypeName = "datetime")]
    public DateTime PaymentDate { get; set; }

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
    [InverseProperty("TbPayments")]
    public virtual TbUser User { get; set; } = null!;
}
