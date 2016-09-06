using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Framework.Common;

namespace StoreManagement.Common.EntityServices
{
    public interface IInvoiceService : IEntityService<Invoice>
    {
        bool CheckNumberExist(string number, long? id);
        void SaveInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
    }
}
