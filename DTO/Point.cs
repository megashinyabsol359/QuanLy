using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class Point
    {
		//DoNotCallOverridableMethodsInConstructors
		public Point()
		{
			this.Students = new HashSet<Student>();
		}

		public Point(int? subject_ID, int? student_ID, int? semester, double? point_15, double? point_45, double? point_CK)
		{
			Subject_ID = subject_ID;
			Student_ID = student_ID;
			Semester = semester;
			Point_15 = point_15;
			Point_45 = point_45;
			Point_CK = point_CK;
		}

		public int Point_ID { get; set; }
        public int? Subject_ID { get; set; }
        public int? Student_ID { get; set; }
        public int? Semester { get; set; }
        public double? Point_15 { get; set; }
        public double? Point_45 { get; set; }
        public double? Point_CK { get; set; }

        public virtual Subject Subject { get; set; }
        //CollectionPropertiesShouldBeReadOnly
        public virtual ICollection<Student> Students { get; set; }


    }
}
