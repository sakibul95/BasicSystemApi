using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BasicSystem.Models
{
    [Table("tblUserAccess")]
    public partial class TblUserAccess
    {
        public int Id { get; set; }
        public string user_id { get; set; }
        public string Menu { get; set; }
        public Boolean? Read { get; set; }
        public Boolean? Write { get; set; }
        public Boolean? Delete { get; set; }
    }
}
