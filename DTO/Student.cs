using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class Student
    {
        public int Student_ID { get; set; }
        public int? Class_ID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string NameClass { get; set; }
        public double? Score1stSem { get; set; }
        public double? Score2ndSem { get; set; }

        public virtual Class Classroom { get; set; }
        public virtual Point Point { get; set; }
    }
}
