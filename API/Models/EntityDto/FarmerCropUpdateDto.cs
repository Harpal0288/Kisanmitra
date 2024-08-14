using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.EntityDto
{
    public class FarmerCropUpdateDto
    {
        [Required]
        public string FarmerId { get; set; }

        [Required]
        public string Crop { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public string NewData { get; set; }
    }
}
