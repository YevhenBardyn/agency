using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agency.Models
{
    public class Vacancy
    {
        public Vacancy(int iD = 0, string name = "NONE", string discription="NONE", int salary = 0)
        {
            ID = iD;
            Name = name;
            Discription = discription;
            Salary = salary;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int Salary { get; set; }
    }
}
