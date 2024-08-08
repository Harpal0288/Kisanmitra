using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Keyless]
public partial class VwGetSpecificFarmerQuery
{
    [Column("Farmer Name")]
    [StringLength(255)]
    public string FarmerName { get; set; } = null!;

    [Column("Queries Submitted")]
    [StringLength(255)]
    [Unicode(false)]
    public string QueriesSubmitted { get; set; } = null!;
}
