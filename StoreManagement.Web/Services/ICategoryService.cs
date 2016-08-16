using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Web.Services
{
    public interface ICategoryService
    {
        bool CheckTitleExist(string title, long? id);
    }
}
