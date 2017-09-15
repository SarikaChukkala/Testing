using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class Product
    {
        public int ProductID
        {
            get;
            set;
        }

        //[Required(ErrorMessage = "Product name is required")]
        //[StringLength(15, ErrorMessage = "Name can be no larger than 15 characters")]
        public string ProductName
        {
            get;
            set;
        }

        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ProductQuantity
        {
            get;
            set;
        }

        public int ProductTypeID { get; set; }
        public int ProductSubTypeID { get; set; }

        public string Description
        { get; set; }

      }
}