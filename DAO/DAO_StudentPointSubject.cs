using Dapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_StudenPointSubject : DBConnect
    {
        public List<StudentPointSubject> getStudentPointSubject(int semester, int idClass, int idSubject)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<StudentPointSubject>($"select st.FULLNAME, p.POINT_15, p.POINT_45, p.POINT_CK from STUDENT AS st left join POINT AS p on p.STUDENT_ID = st.STUDENT_ID AND p.SEMESTER = '{semester}' AND p.SUBJECT_ID = '{idSubject}' where st.CLASS_ID = '{idClass}'").ToList();
                return output;
            }
        }
    }
}
