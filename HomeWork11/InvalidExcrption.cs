using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork11
{
    public class InvalidExcrption:Exception
    {
        public InvalidExcrption(string str):base(str) { }
    }
}
