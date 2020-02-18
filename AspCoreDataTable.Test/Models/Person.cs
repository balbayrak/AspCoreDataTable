using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreDataTable.Test.Models
{
    public class Person
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public PersonAdress PersonAdress { get; set; }
    }
}
