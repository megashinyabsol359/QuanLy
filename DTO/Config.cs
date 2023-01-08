using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class Config
    {
        public int Config_ID { get; set; }
        public int? Min_Age { get; set; }
        public int? Max_Age { get; set; }
        public double? Subject_Point_Standards { get; set; }
        public int? Max_Ratio { get; set; }
        public int? Max_Student_Class { get; set; }
        public int? Max_Class { get; set; }
        public int? Max_Subject { get; set; }
    }
}
