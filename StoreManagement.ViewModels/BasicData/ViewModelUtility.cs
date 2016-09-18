using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.ViewModels
{
    public static class ViewModelUtility<TViewModel>
    {
        public static Dictionary<string, string> ViewModelToDictionaryConverter(TViewModel viewModel)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var properties = viewModel.GetType().GetProperties();
            var db = Repository.Current;
            foreach (var prop in properties)
            {
                if(prop.PropertyType.IsValueType ? 
                    !(prop.Name == "Id" && (long)prop.GetValue(viewModel) == 0) 
                    : prop.GetValue(viewModel) != null)
                {
                    result.Add(prop.Name, prop.GetValue(viewModel).ToString());
                }
            }
            return result;
        } 
    }
}
