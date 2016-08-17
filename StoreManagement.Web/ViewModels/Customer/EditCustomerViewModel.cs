using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.ViewModels.Customer
{
    public class EditCustomerViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Version { get; set; }
    }
}