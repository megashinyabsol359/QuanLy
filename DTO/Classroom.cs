using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class Classroom
    {
        //DoNotCallOverridableMethodsInConstructors
        public Classroom()
        {
            this.SemesterClassrooms = new HashSet<SemesterClassroom>();
            this.Students = new HashSet<Student>();
        }

        public int IDClassroom { get; set; }
        public string NameClass { get; set; }

        //CollectionPropertiesShouldBeReadOnly
        public virtual ICollection<SemesterClassroom> SemesterClassrooms { get; set; }
        //CollectionPropertiesShouldBeReadOnly
        public virtual ICollection<Student> Students { get; set; }
    }
}
