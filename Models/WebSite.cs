using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace TruckEasyWebSite.Models
{
    public class WebSite
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
        [RegularExpression("([0-9]+)")]
        [Display(Name = "Phone Number")]
        [StringLength(10)]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Please choose one type of service.")]
        [DataType(DataType.Text)]
        [Display(Name = "Service")]
        [StringLength(3)]
        public string orderServiceId { get; set; }

        [Required(ErrorMessage = "Please enter when the job should be done.")]
        [DataType(DataType.Date)]
        [Display(Name = "When the job should be done?")]
        [StringLength(10)]
        public string dateSchedule { get; set; }

        [Required(ErrorMessage = "Please choose how you found about us.")]
        [DataType(DataType.Text)]
        [Display(Name = "How did you find us?")]
        [StringLength(3)]
        public string orderSourceId { get; set; }

        [Required(ErrorMessage = "Please enter Project Details.")]
        [DataType(DataType.Text)]
        [Display(Name = "Project Details")]
        [StringLength(1000)]
        public string notes { get; set; }

        public SelectList comboServices { get; set; }
        public SelectList comboSources { get; set; }
        public IEnumerable<OrderReviews> reviews { get; set; }

        public IEnumerable<OrderAddresses> addresses { get; set; }

    }

    [Table("order_addresses")]
    public class OrderAddresses
    {
        [Key]
        public int addressId { get; set; }
        public string orderId { get; set; }
        public string address { get; set; }
        public string addressComp { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string order { get; set; }
        public string notes { get; set; }

    }

    [Table("order_services")]
    public class OrderServices
    {
        [Key]
        public int orderServiceId { get; set; }
        public string service { get; set; }
        public int active { get; set; }

    }

    [Table("order_sources")]
    public class OrderSources
    {
        [Key]
        public int orderSourceId { get; set; }
        public string name { get; set; }

    }

    [Table("web_reviews")]
    public class OrderReviews { 
    
        [Key]
        public int orderId { get; set; }
        public string customerName { get; set; }
        public string source { get; set; }
        public string service { get; set; }
        public int stars { get; set; }
        public string review { get; set; }
        public string created_at { get; set; }
   }

    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class WebSiteDBContext : DbContext
    {
        public WebSiteDBContext()
            : base("DbContext") { }

        public DbSet<OrderServices> OrderServices { get; set; }
        public DbSet<OrderSources> OrderSources { get; set; }
        public DbSet<OrderReviews> OrderReviews { get; set; }
        public DbSet<OrderAddresses> OrderAddresses { get; set; }

        public void CreateOrder(WebSite form) {

            MySqlParameter firstNameParam = new MySqlParameter("p_firstName", MySqlDbType.VarChar);
            firstNameParam.Direction = System.Data.ParameterDirection.Input;
            firstNameParam.Value = form.firstName;

            MySqlParameter lastNameParam = new MySqlParameter("p_lastName", MySqlDbType.VarChar);
            lastNameParam.Direction = System.Data.ParameterDirection.Input;
            lastNameParam.Value = form.lastName;

            MySqlParameter emailParam = new MySqlParameter("p_email", MySqlDbType.VarChar);
            emailParam.Direction = System.Data.ParameterDirection.Input;
            emailParam.Value = form.email;

            MySqlParameter phoneNumberParam = new MySqlParameter("p_phoneNumber", MySqlDbType.VarChar);
            phoneNumberParam.Direction = System.Data.ParameterDirection.Input;
            phoneNumberParam.Value = form.phoneNumber;

            MySqlParameter dateScheduleParam = new MySqlParameter("p_dateSchedule", MySqlDbType.Date);
            dateScheduleParam.Direction = System.Data.ParameterDirection.Input;
            dateScheduleParam.Value = form.dateSchedule;

            MySqlParameter serviceParam = new MySqlParameter("p_service", MySqlDbType.Int32);
            serviceParam.Direction = System.Data.ParameterDirection.Input;
            serviceParam.Value = form.orderServiceId;

            MySqlParameter sourceParam = new MySqlParameter("p_source", MySqlDbType.Int32);
            sourceParam.Direction = System.Data.ParameterDirection.Input;
            sourceParam.Value = form.orderSourceId;

            MySqlParameter notesParam = new MySqlParameter("p_notes", MySqlDbType.VarChar);
            notesParam.Direction = System.Data.ParameterDirection.Input;
            notesParam.Value = form.notes;

            MySqlParameter[] spParams = new MySqlParameter[] {
                firstNameParam, lastNameParam, emailParam, phoneNumberParam, dateScheduleParam, serviceParam, sourceParam, notesParam
            };

            StringBuilder sb = new StringBuilder();
            sb.Append("CALL spAddOrderWebSite(@p_firstName, @p_lastName, @p_email, @p_phoneNumber, @p_dateSchedule, @p_service, @p_source, @p_notes)");
            string commandText = sb.ToString();

            base.Database.ExecuteSqlCommand(commandText, spParams);

        }

    }

}