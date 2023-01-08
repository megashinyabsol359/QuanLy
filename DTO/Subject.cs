using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class Subject
    {
        //DoNotCallOverridableMethodsInConstructors
        public Subject()
        {
            this.Points = new HashSet<Point>();
        }

        public int Subject_ID { get; set; }
        public string Subject_Name { get; set; }

        //CollectionPropertiesShouldBeReadOnly
        public virtual ICollection<Point> Points { get; set; }
    }
}
