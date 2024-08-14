using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Models.EntityDto
{
    public class FarmerCropInserDto
    {
        [Required]
        public string FarmerId { get; set; }

        [Required]
        public string Crop { get; set; }

        [Required]
        public string InsertedBy { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}
