using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class Class
    {
        //DoNotCallOverridableMethodsInConstructors
        public Class()
        {
            this.Students = new HashSet<Student>();
        }
        public class DTO_Subject_Report
        {
            public int Stt { get; set; }
            public string Class_Name { get; set; }
            public int SiSo { get; set; }
            public int Pass { get; set; }
            public string Rate { get; set; }

        }
        public int Class_ID { get; set; }
        public string Class_Name { get; set; }

        public int Class_Group { get; set; }
        public int NumberMember { get; set; }
        int _selectedItem { get; set; }

        //CollectionPropertiesShouldBeReadOnly
        public virtual ICollection<Student> Students { get; set; }
    }
}
