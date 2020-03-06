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

        public int status { get; set; }

        public PersonAdress PersonAdress { get; set; }

        public Person()
        {
            id = Guid.NewGuid();
            name = string.Empty;
            surname = string.Empty;
            PersonAdress = new PersonAdress();
            PersonAdress.Id = Guid.NewGuid();
            PersonAdress.city = string.Empty;
            PersonAdress.country = string.Empty;
        }
    }
}
