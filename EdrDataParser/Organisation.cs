using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdrDataParser
{
    class Organisation
    {
        public string EdrpouCode { get; set; }
        public string OfficialName { get; set; }
        public string Occupation { get; set; }
        public string Status { get; set; }
        public string Head { get; set; }





        public override string ToString()
        {
            return base.ToString();
        }
    }
}
