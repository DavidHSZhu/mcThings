using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustProject.Models
{   //CustomerDContext is used to get corresponding records from database
    public class CustomerDbContext :DbContext
    {
        public DbSet<Customer> Customers { get; set; }//Dbset is kind of collection to get Customer object from customer table
    }
}