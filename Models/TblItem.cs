using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BasicSystem.Models
{
    [Table("tblItem")]
    public partial class TblItem
    {
        public int Id { get; set; }
        [Column("name")]
        [StringLength(256)]
        public string name { get; set; }
        [Column("code")]
        public int? code { get; set; }
        [Column("descriptions")]
        [StringLength(512)]
        public string descriptions { get; set; }
        [Column("defaultCost", TypeName = "numeric(25, 5)")]
        public decimal? defaultCost { get; set; }
        [Column("defaultPrice", TypeName = "numeric(25, 5)")]
        public decimal? defaultPrice { get; set; }
        
    }
}
