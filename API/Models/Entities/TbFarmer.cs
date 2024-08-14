using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Table("tb_Farmers")]
public partial class TbFarmer
{
    [Key]
    [Column("farmer_id")]
    [StringLength(255)]
    public string? FarmerId { get; set; } = null!;

    [Column("user_id")]
    [StringLength(255)]
    public string? UserId { get; set; }

    [Column("farm_size")]
    [StringLength(250)]
    public string? FarmSize { get; set; } = null!;

    [Column("farm_location")]
    [StringLength(255)]
    public string? FarmLocation { get; set; } = null!;

    [Column("pin_code")]
    [StringLength(255)]
    public string? PinCode { get; set; } = null!;

    [Column("irrigation_method")]
    [StringLength(255)]
    [Unicode(false)]
    public string? IrrigationMethod { get; set; }

    [Column("soil_type")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SoilType { get; set; }

    [Column("farming_experience")]
    public int? FarmingExperience { get; set; }

    [Column("membership_status")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MembershipStatus { get; set; } = null!;

    [Column("membership_expiry", TypeName = "datetime")]
    public DateTime? MembershipExpiry { get; set; }

    [Column("language_preference")]
    [StringLength(255)]
    public string? LanguagePreference { get; set; } = null!;

    [Column("inserted_date", TypeName = "datetime")]
    public DateTime? InsertedDate { get; set; }

    [Column("inserted_by")]
    [StringLength(255)]
    public string? InsertedBy { get; set; } = null!;

    [Column("updated_date", TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string? UpdatedBy { get; set; } = null!;

    [InverseProperty("Farmer")]
    public virtual ICollection<TbDiscussionForum> TbDiscussionForums { get; set; } = new List<TbDiscussionForum>();

    [InverseProperty("Farmar")]
    public virtual ICollection<TbFarmerCrop> TbFarmerCrops { get; set; } = new List<TbFarmerCrop>();

    [InverseProperty("Farmer")]
    public virtual ICollection<TbFarmerEquipment> TbFarmerEquipments { get; set; } = new List<TbFarmerEquipment>();

    [InverseProperty("Farmer")]
    public virtual ICollection<TbFarmerLibraryResource> TbFarmerLibraryResources { get; set; } = new List<TbFarmerLibraryResource>();

    [InverseProperty("Farmer")]
    public virtual ICollection<TbForumPost> TbForumPosts { get; set; } = new List<TbForumPost>();

    [InverseProperty("Farmer")]
    public virtual ICollection<TbQuery> TbQueries { get; set; } = new List<TbQuery>();

    [ForeignKey("UserId")]
    [InverseProperty("TbFarmers")]
    public virtual TbUser? User { get; set; }
}
