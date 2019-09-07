using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostCard.Models
{
    public class UploadedDataViewModel
    {
        /// <summary>
        /// We will use this model to validate the user's information 
        /// and pass the base64 string a long with the other data like subject, etc. to the Home controller
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "The name cannot exceed 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z",
        ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "EmailBody")]
        public string EmailBody { get; set; }

        [Required]
        public string Base64 { get; set; }      

    }
}
