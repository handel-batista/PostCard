using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCard.Models
{
    public class UploadedData
    {
        /// <summary>
        /// Serves as a template for our database migration and pases the 
        /// data to our IUploadedDataRespository 
        /// </summary>
        [Key]
        public int DataId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public DateTime SentDate { get; set; }

        public string ImageName { get; set; }

        public string ImagePath { get; set; }
    }
}
