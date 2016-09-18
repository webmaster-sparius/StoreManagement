using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    public static class ControllerHelper
    {
        public static Dictionary<string, string> QueryStringToDictionary(string queryString)
        {
            var collection = HttpUtility.ParseQueryString(queryString);
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var key in collection.AllKeys)
            {
                if (collection[key].Trim() != "")
                    result.Add(key, collection[key]);
            }
            return result;
        }
    }
}