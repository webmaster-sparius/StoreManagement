using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        [DisplayName("عنوان")]
        public string Title { get; set; }
        public long Id { get; set; }

        public byte[] Version { get; set; }
    }
}