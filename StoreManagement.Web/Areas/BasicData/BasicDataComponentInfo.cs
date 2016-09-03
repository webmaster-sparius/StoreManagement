using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Framework.UI;
using StoreManagement.Common.Models;
using StoreManagement.Common.EntityServices;
using StoreManagement.Web.Areas.BasicData.ViewModels;

namespace StoreManagement.Web.Areas.BasicData
{
    public class BasicDataComponentInfo : ComponentInfo
    {
        public override string AreaName
        {
            get
            {
                return "BasicData";
            }
        }

        public override IEnumerable<ShellLink> GetComponentShellLinks()
        {
            return new[]
            {
                new ShellLink("گروه", "Category", "List"),
                new ShellLink("کالا", "Product", "List"),
                new ShellLink("مشتری", "Customer", "List"),
               // new ShellLink("ثبت فاکتور","Invoice","Create"),
                new ShellLink("فاکتور","Invoice","List")
            };
        }

        public override IEnumerable<EntityInfo> GetEntityInfos()
        {
            return new[]
            {
                new EntityInfo(typeof(Category), typeof(CategoryViewModel), typeof(ICategoryService)),
                new EntityInfo(typeof(Customer), typeof(CustomerViewModel), typeof(ICustomerService)),
                new EntityInfo(typeof(Product), typeof(ProductViewModel), typeof(IProductService)),
                new EntityInfo(typeof(Invoice), typeof(InvoiceViewModel),typeof(IInvoiceService))
            };
        }
    }
}