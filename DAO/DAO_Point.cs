using Dapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_Point : DBConnect
    {
        public List<Point> GetOneSubjectMarkBySemester(int? IDSubject, int? IDStudent, int? IDSemester)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Point>($"select POINT_15,POINT_45,POINT_CK from POINT where SUBJECT_ID = '{IDSubject}' and STUDENT_ID = '{IDStudent}' and SEMESTER = '{IDSemester}'").ToList();
                return output;
            }
        }

        public bool DeletePointByStudentID(int ID)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var affectedRows = _dbConnection.Execute($"delete from POINT where STUDENT_ID = '{ID}'");
                if (affectedRows == 0) return false;
                else return true;
            }
        }

        public void InsertPointForStudent(Point _point)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var affectedRows = _dbConnection.Execute($"insert into POINT (STUDENT_ID,SUBJECT_ID,POINT_15,POINT_45,POINT_CK,AVG,SEMESTER) values (@Student_ID,@Subject_ID,@Point_15,@Point_45,@Point_CK,@AVG,@Semester)", new
                {
                    Student_ID = _point.Student_ID,
                    Subject_ID = _point.Subject_ID,
                    Point_15 = _point.Point_15,
                    Point_45 = _point.Point_45,
                    Point_CK = _point.Point_CK,
                    AVG = Math.Round((double)((_point.Point_15 + _point.Point_45 * 2 + _point.Point_CK * 3 ) / 6),2),
                    Semester = _point.Semester,
                });
            }
        }

        public List<Point> getAll()
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Point>($"select * from POINT").ToList();
                return output;
            }
        }

        public void updatePointForStudent(Point _point)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var _avg = Math.Round((double)((_point.Point_15 + _point.Point_45 * 2 + _point.Point_CK * 3) / 6), 2);
                var affectedRows = _dbConnection.Execute($"update POINT set POINT_15 = '{_point.Point_15}', POINT_45 = '{_point.Point_45}', POINT_CK = '{_point.Point_CK}', AVG = '{_avg}' where STUDENT_ID = '{_point.Student_ID}' AND SEMESTER = '{_point.Semester}' AND SUBJECT_ID = '{_point.Subject_ID}'");
            }
        }
    }
}
