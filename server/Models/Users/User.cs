using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_GM_IMP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            var rhs = obj as User;
            return Id == rhs.Id
                && name == rhs.name
                && email == rhs.name;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            throw new NotImplementedException();
            return base.GetHashCode();
        }
    }
}
