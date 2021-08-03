using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechAPITest.Models
{
    public class Ward
    {
        public int WardID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int SpacesLeft { get; set; }
    }
}
