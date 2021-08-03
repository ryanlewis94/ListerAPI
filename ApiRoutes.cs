using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechAPITest
{
    public static class ApiRoutes
    {
        public const string Root = "lister-api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Patients
        {
            public const string Route = Base + "/patient";
            public const string Spell = Route + "/spell";
            public const string Active = Route + "/active-list";
        }
    }
}
