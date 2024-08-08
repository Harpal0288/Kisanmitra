using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_PaymentGateway")]
public partial class TbPaymentGateway
{
    [Key]
    [Column("gateway_id")]
    [StringLength(255)]
    public string GatewayId { get; set; } = null!;

    [Column("gateway_name")]
    [StringLength(255)]
    public string GatewayName { get; set; } = null!;

    [Column("gateway_details")]
    [StringLength(255)]
    public string GatewayDetails { get; set; } = null!;

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
}
