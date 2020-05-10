using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_GM_IMP.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string oauthSubject { get; set; }
        public string oauthIssuer { get; set; }
    }
}
