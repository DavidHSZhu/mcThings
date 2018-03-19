using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustProject.Models
{
    //Define a class of Customer with required memebers
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //declare CustomerID is primary key with auto increament
        public int CustomerID { get; set; }


        [Required(ErrorMessage = "Customer name is required!")]//when submit CustomerName is  required
       
        public String CustomerName { get; set; }

        [Required(ErrorMessage = "Customer address is required!")]//when submit CustomerAddress is  required
      
        public String CustomerAddress { get; set; }
        [Required(ErrorMessage = "Customer remarks is required!")]//when submit Remarks is  required
       
        public String Remarks { get; set; }

    }
}