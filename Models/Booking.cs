using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace TruckEasyWebSite.Models
{
    public class Booking
    {

        [Required(ErrorMessage = "Please enter first name.")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please enter a valid e-mail address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [StringLength(10)]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Please choose one type of service.")]
        [DataType(DataType.Text)]
        [Display(Name = "Service")]
        [StringLength(1)]
        public string orderServiceId { get; set; }

        [Required(ErrorMessage = "Please enter when the job should be done.")]
        [DataType(DataType.Date)]
        [Display(Name = "When the job should be done?")]
        [StringLength(10)]
        public string dateSchedule { get; set; }

        [Required(ErrorMessage = "Please enter when the job should start")]
        [DataType(DataType.Time)]
        [Display(Name = "Preferred time to start?")]
        public string timeSchedule { get; set; }

        [Required(ErrorMessage = "Please choose how you found about us.")]
        [DataType(DataType.Text)]
        [Display(Name = "How did you find us?")]
        [StringLength(1)]
        public string orderSourceId { get; set; }

        [Required(ErrorMessage = "Please enter Job Details.")]
        [DataType(DataType.Text)]
        [Display(Name = "Job Details")]
        [StringLength(1000)]
        public string notes { get; set; }




    }
}