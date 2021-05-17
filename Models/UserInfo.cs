using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BasicSystem.Models
{
    [Table("User_info")]
    public partial class UserInfo
    {
        [Key]
        [Column("ID")]
        public Guid ID { get; set; }
        [Required]
        [Column("User_id")]
        [StringLength(255)]
        public string User_id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Column("pass")]
        [StringLength(255)]
        public string pass { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string role { get; set; }
    }
}
