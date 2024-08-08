using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.Entities;

[Keyless]
public partial class VwGetAllAnswersWhereConsultantexperienceGreaterThan5
{
    [Column("answer_id")]
    [StringLength(255)]
    public string AnswerId { get; set; } = null!;

    [Column("Consultant Name")]
    [StringLength(255)]
    public string ConsultantName { get; set; } = null!;

    [StringLength(1000)]
    public string Answer { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;
}
