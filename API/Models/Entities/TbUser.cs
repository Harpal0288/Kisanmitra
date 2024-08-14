using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Users")]
[Index("AadharNumber", Name = "UQ__tb_Users__9CF469850DF86CA9", IsUnique = true)]
[Index("Email", Name = "UQ__tb_Users__AB6E616402FD6D4D", IsUnique = true)]
public partial class TbUser
{
    [Key]
    [Column("user_id")]
    [StringLength(255)]
    public string? UserId { get; set; } = null!;

    [Column("user_name")]
    [StringLength(255)]
    public string? UserName { get; set; } = null!;

    [Column("aadhar_number")]
    [StringLength(255)]
    public string? AadharNumber { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(255)]
    public string? PhoneNumber { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; } = null!;

    [Column("role_id")]
    [StringLength(255)]
    public string? RoleId { get; set; } = null!;

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime InsertedDate { get; set; }

    [Column("inserted_by")]
    [StringLength(255)]
    public string? InsertedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string? UpdatedBy { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("TbUsers")]
    public virtual TbRole? Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<TbAdmin> TbAdmins { get; set; } = new List<TbAdmin>();

    [InverseProperty("User")]
    public virtual ICollection<TbConsultant> TbConsultants { get; set; } = new List<TbConsultant>();

    [InverseProperty("User")]
    public virtual ICollection<TbFarmer> TbFarmers { get; set; } = new List<TbFarmer>();

    [InverseProperty("User")]
    public virtual ICollection<TbPayment> TbPayments { get; set; } = new List<TbPayment>();

    [InverseProperty("User")]
    public virtual ICollection<TbRating> TbRatings { get; set; } = new List<TbRating>();
}
