using StoreManagement.Framework.Common;
using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.Web.Areas.BasicData.ViewModels;

namespace StoreManagement.Common.EntityServices
{
    public interface ICategoryService : IEntityService<Category>
    {
        bool CheckTitleExist(string title, long? id);
        IEnumerable<Category> FetchAll();
		EditCategoryViewModel FetchEditViewModel(long? id);
        void EditByViewModel(EditCategoryViewModel viewModel);
		IEnumerable<Category> FetchByTitle(string title);
        void DeleteById(long id);
    }
}
