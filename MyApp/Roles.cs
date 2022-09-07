using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    internal class Roles
    {
        private string roleName;
        public string GetRoleName { get { return roleName; }}
        public Roles(string a)
        {
            roleName = a;
        }

    }
}
