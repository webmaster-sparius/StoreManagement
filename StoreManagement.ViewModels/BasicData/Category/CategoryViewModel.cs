using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    [DisplayName("گروه")]
    public class CategoryViewModel
    {
        [DisplayName("عنوان")]
        public string Title { get; set; }
        public long Id { get; set; }

        public byte[] Version { get; set; }

        public static CategoryViewModel FromModel(Category category)
        {
            return new ViewModels.CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Version = category.Version
            };
        }
    }
}