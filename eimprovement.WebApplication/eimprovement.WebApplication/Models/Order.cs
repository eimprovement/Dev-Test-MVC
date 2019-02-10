using eimprovement.WebApplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class Order
    {
        #region Properties
        public long Id { get; set; }
        public long PetId { get; set; }
        public int Quantity { get; set; }
        public DateTime ShipDate { get; set; }
        //public OrderStatus Status { get; set; }
        public bool Complete { get; set; }
        #endregion
    }
}