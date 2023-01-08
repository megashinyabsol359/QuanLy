using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_StudentPointSubject
    {
        DAO_StudenPointSubject _daoStudenPointSubject = new DAO_StudenPointSubject();
        public List<StudentPointSubject> getStudentPointSubject(int semester, int idClass, int idSubject) => _daoStudenPointSubject.getStudentPointSubject(semester, idClass, idSubject);
    }
}
