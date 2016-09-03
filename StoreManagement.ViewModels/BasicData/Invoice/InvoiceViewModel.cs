using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using StoreManagement.Common.Models;
using System.ComponentModel;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    [DisplayName("فاکتور")]
    public class InvoiceViewModel
    {
        [DisplayName("شماره")]
        [Required]
        [MaxLength(50)]
        public string Number { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        [DisplayName("مشتری")]
        public string Customer { get; set; }   // needs check

        ICollection<InvoiceItemViewModel> Items { get; set; }
    }
}