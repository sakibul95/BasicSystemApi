using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicSystem.Models
{
    public class Login
    {
        public Guid ID { get; set; }
        public string User_id { get; set; }
        public string Name { get; set; }
        public string pass { get; set; }
        public string role { get; set; }
        public string token { get; set; }

        public List<TblUserAccess> TblUserAccess { get; set; }
    }
}
