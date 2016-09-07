using StoreManagement.Web.Areas.BasicData.ViewModels;
using StoreManagement.Common.EntityServices;
using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Web.Areas.BasicData.Controllers;
using StoreManagement.Common.Models;

namespace StoreManagement.Web.Controllers
{
    public class CometController : Controller
    {
        // GET: Comet
        public ActionResult Index()
        {
            return View();
        }

        #region Shared

        private static object obj;

        static CometController()
        {
            check_init_containers();
        }

        #endregion


        #region Client_id

        private static int counter;

        private static void check_init_containers()
        {
            if (obj == null)
            {
                obj = new object();
            }

            if (lastAddNotif == null)
            {
                lock (obj)
                {
                    lastAddNotif = new Dictionary<int, DateTime>();
                }
            }
            if(lastEditNotif == null)
            {
                lock (obj)
                {
                    lastEditNotif = new Dictionary<int, DateTime>();
                }
            }
            if(lastDeleteNotif == null)
            {
                lock (obj)
                {
                    lastDeleteNotif = new Dictionary<int, DateTime>();
                }
            }
            if(addNotifList == null)
            {
                lock (obj)
                {
                    addNotifList = new Dictionary<string, List<Tuple<long, DateTime>>>();
                }
            }
        }

        public int SendClientId()
        {
            lock (obj)
            {
                if (counter > 1000000000)
                {
                    counter = 1;
                }
                counter++;
                lastAddNotif[counter] = DateTime.Now;
                lastEditNotif[counter] = DateTime.Now;
                lastDeleteNotif[counter] = DateTime.Now;
                return counter;
            }
        }

        #endregion


        #region Add_Notifications

        private static Dictionary<int, DateTime> lastAddNotif;      // int is the client_id

        private static Dictionary<string, List<Tuple<long, DateTime>>> addNotifList; // string for entity name 
                                                                                     // and long for entity id

        public static void SetAddNotif(string ename, long id)       // this should be called from controllers
        {
            // cannot call PartialView because that's not a static method
            // so just keep the ids.
            lock (obj)
            {
                if (!addNotifList.ContainsKey(ename))
                {
                    addNotifList[ename] = new List<Tuple<long, DateTime>>();
                }
                addNotifList[ename].Add(new Tuple<long, DateTime>(id, DateTime.Now));
            }
        }

        public PartialViewResult NotifyOnAdd(int client_id, string entity_name)
        {
            // iterate the addNotifList, fetch notifications with time larger than lastAddNotif[client_id]
            List<long> id_list = new List<long>();

            if (!addNotifList.ContainsKey(entity_name))
            {
                addNotifList[entity_name] = new List<Tuple<long, DateTime>>();
            }

            while (true) {

                Thread.Sleep(200);
                foreach (var elem in addNotifList[entity_name])
                {
                    if (elem.Item2 > lastAddNotif[client_id])
                    {
                        id_list.Add(elem.Item1);
                    }
                }

                if (id_list.Count > 0)
                {
                    break;
                }

                if((int)(DateTime.Now - lastAddNotif[client_id]).TotalMilliseconds > 10000)     // 10 seconds timeout
                {
                    break;
                }
            }

            // now produce viewmodels from ids

            //List<PartialViewResult> pvs = new List<PartialViewResult>();
            PartialViewResult res = new PartialViewResult();

            List<ProductViewModel>  productVms  = new List<ProductViewModel>();
            
            List<CustomerViewModel> customerVms = new List<CustomerViewModel>();
            List<InvoiceViewModel>  invms       = new List<InvoiceViewModel>();

            // Note: entity_name is one of our arguments and it never changes in this request
            // AND we have already collected ids of only one entity into our id_list
            // so everything in our id_list is of type 'entity_name'

            if (entity_name == "Product")
            {
                foreach(long mid in id_list)
                {
                    var temp = ServiceFactory.Create<IProductService>().
                                FetchByIdAndProject(mid, p => new ProductViewModel
                    {
                        Code = p.Code,
                        Name = p.Name,
                        Price = p.Price,
                        Category = p.Category.Title,
                        Id = p.Id,
                        Description = p.Description
                    });
                    ViewBag.Type = typeof(Product);
                    ViewBag.Entity = temp;
                    pvs.Add(PartialView("_CometPartial",temp));  // make a partial view with the view model and add it to pvs
                }
            }


            else if (entity_name == "Category")
            {
                List<CategoryViewModel> categoryVms = new List<CategoryViewModel>();

                foreach (long mid in id_list)
                {
                    var model = ServiceFactory.Create<ICategoryService>().FetchById(mid);
                    var temp = CategoryViewModel.FromModel(model);
                    //ViewBag.Type = typeof(Category);
                    //ViewBag.Entity = temp;
                    //pvs.Add(PartialView("_CometPartial", temp));
                    categoryVms.Add(temp);
                }

                ViewBag.Type = typeof(Category);
                ViewBag.EntityList = categoryVms;

            }


            else if (entity_name == "Customer")
            {
                foreach (long mid in id_list)
                {
                    var temp = ServiceFactory.Create<ICustomerService>().
                                FetchByIdAndProject(mid, c => new CustomerViewModel
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber
                    });
                    ViewBag.Type = typeof(Customer);
                    ViewBag.Entity = temp;
                    pvs.Add(PartialView("_CometPartial", temp));
                }
            }


            else if (entity_name == "Invoice")
            {
                foreach(long mid in id_list)
                {
                    var temp = ServiceFactory.Create<IInvoiceService>().
                                FetchByIdAndProject(mid, new InvoiceController().
                                GetInvoiceToInvoiceViewModelExpression());
                    ViewBag.Type = typeof(Invoice); 
                    ViewBag.Entity = temp;
                    pvs.Add(PartialView("_CometPartial", temp));
                }
            }

            ViewBag.EntityList = pvs;
            var res = PartialView("_CometAddResult");

            return res;
        }

        #endregion

        private static Dictionary<int, DateTime> lastEditNotif;
        private static Dictionary<int, DateTime> lastDeleteNotif;
    }
}