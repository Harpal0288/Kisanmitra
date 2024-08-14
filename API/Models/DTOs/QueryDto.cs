using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models.DTOs
{
    public class QueryDto
    {
        [Key]
        [Column("query_id")]
        [StringLength(255)]
        public string QueryId { get; set; } 

        [Column("category_id")]
        [StringLength(255)]
        public string? CategoryId { get; set; }

        [Column("farmer_id")]
        [StringLength(255)]
        public string FarmerId { get; set; }

        [Column("query_title")]
        [StringLength(255)]
        [Unicode(false)]
        public string QueryTitle { get; set; }

        [Column("query_description")]
        [StringLength(255)]
        public string? QueryDescription { get; set; }

        [Column("inserted_by")]
        [StringLength(255)]
        public string InsertedBy { get; set; } 

        [Column("inserted_date", TypeName = "datetime")]
        public DateTime InsertedDate { get; set; }

        [Column("updated_by")]
        [StringLength(255)]
        public string UpdatedBy { get; set; } 

        [Column("updated_date", TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }

        [Column("time_stamp", TypeName = "datetime")]
        public DateTime? TimeStamp { get; set; }
    }
}
