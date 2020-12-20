using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ChemiCleanFiles.Models
{
    [Table("tblProduct")]
    public partial class TblProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string ProductName { get; set; }
        [StringLength(250)]
        public string SupplierName { get; set; }
        [Required]
        [StringLength(300)]
        public string Url { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public string ContentHash { get; set; }
        public DateTime? Updated { get; set; }
        public string Uri { get; set; }
    }
}
