using StoreManagement.Framework.Common;
using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Common.EntityServices
{
    public interface ICategoryService : IEntityService<Category>
    {
        bool CheckTitleExist(string title, long? id);
		IEnumerable<Category> FetchByTitle(string title);
    }
}
