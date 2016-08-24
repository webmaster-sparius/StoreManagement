using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StoreManagement.Common.Models
{
    public class Product: BaseEntity
    {
        public Product()
        {
            CreatedOn = DateTime.Now;
        }
        #region Properties

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        #endregion

        #region NavigationProperties
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        #endregion
    }
}