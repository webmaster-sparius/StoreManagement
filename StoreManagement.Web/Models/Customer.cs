using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Models
{
    public class Customer
    {
        #region Properties
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        #endregion

        #region NavigationProperties

        #endregion
    }
}