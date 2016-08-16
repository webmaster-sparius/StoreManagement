using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Models
{
    public class Category : BaseEntity
    {
        #region Properties
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }


        #endregion

        #region NavigationProperties
        public virtual ICollection<Product> Products { get; set; }
        #endregion
    }
}