using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class StudentPointSubject
    {
        //DoNotCallOverridableMethodsInConstructors
        public string FullName { get; set; }
        public double? Point_15 { get; set; }
        public double? Point_45 { get; set; }
        public double? Point_CK { get; set; }
        public int Stt { get; set; }
    }
}
