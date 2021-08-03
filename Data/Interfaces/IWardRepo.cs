using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAPITest.Models;

namespace TechAPITest.Data.Interfaces
{
    public interface IWardRepo
    {
        Ward GetWardById(int Id);
    }
}
