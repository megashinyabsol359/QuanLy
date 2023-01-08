using Dapper;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.Class;

namespace DAO
{
    public class DAO_Class : DBConnect
    {
        public string GetNameClassByID(int? ID)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var outputObj = _dbConnection.Query<Class>($"select CLASS_NAME from CLASS where CLASS_ID ='{ID}'").ToList();
                if (outputObj.Count == 0) return "";
                var output = outputObj[0].Class_Name;
                return output;
            }
        }
        public void InsertAClass(Class _class)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var affectedRows = _dbConnection.Execute($"insert into CLASS (CLASS_NAME,CLASS_GROUP)  values (@ClassName,@ClassGroup)", new
                {

                    ClassName = _class.Class_Name,
                    ClassGroup = _class.Class_Group,
                }) ;
               ;
            }
        }
        public List<Class> GetAll()
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Class>($"select * from CLASS order by CLASS_NAME").ToList();
                return output;
            }
        }
        public List<Class> GetAllClassGroup()
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Class>($"select CLASS_GROUP from CLASS GROUP BY CLASS_GROUP").ToList();
                return output;
            }
        }
        public List<Class> ReadClassByClassGroup(int class_group)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Class>($"select * from CLASS where CLASS_GROUP = '{class_group}'").ToList();
                return output;
            }
        }
        public List<Class> ReadClassByID( int  class_id)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Class>($"select * from CLASS where CLASS_ID = '{class_id}'").ToList();
                return output;
            }
        }
        public List<Class> ReadClassByByNameAndClassGroup(string NameClass,int? Class_Group)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Class>($"select * from CLASS where CLASS_NAME like N'%{NameClass}%' AND CLASS_GROUP='{ Class_Group}'").ToList();
                return output;
            }
        }
        public List<Class> ReadClassByName(string NameClass)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Class>($"select * from CLASS where CLASS_NAME like N'%{NameClass}%'").ToList();
                return output;
            }
        }
        //Xóa lớp
        public void DeleteClass(int idClass)
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                 _dbConnection.Query<Class>($"DELETE from CLASS where CLASS_ID ='{idClass}'");    
            }
        }
        /// <summary>
        /// Lấy ds báo cáo tổng kết theo môn học / học kỳ
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="semesisId"></param>
        /// <param name="scorePass"></param>
        /// <returns></returns>
        public List<DTO_Subject_Report> listDataSubjectReport(int subjectId = 0, int semesisId = 0, float scorePass = 0)
        {
            try
            {
                DBConnect _dbContext = new DBConnect();
                var parameters = new DynamicParameters();
                using (IDbConnection _dbConnection = _dbContext.CreateConnection())
                {
                    var queryString = $@"SELECT CLASS.CLASS_NAME AS Class_Name,
		                                    count(STUDENT.STUDENT_ID) AS SiSo,
		                                    count(CASE WHEN POINT.[AVG] >= {scorePass} THEN POINT.[AVG] END) AS Pass
                                    FROM CLASS INNER JOIN STUDENT ON CLASS.CLASS_ID = STUDENT.CLASS_ID
	                                    LEFT JOIN POINT ON POINT.STUDENT_ID = STUDENT.STUDENT_ID
                                    WHERE 1=1 ";
                    if (subjectId != 0)
                    {
                        queryString += " AND POINT.SUBJECT_ID = @SUBJECT_ID ";
                        parameters.Add("@SUBJECT_ID", subjectId);
                    }
                    if (semesisId != 0)
                    {
                        queryString += " AND POINT.SEMESTER = @SEMESTER ";
                        parameters.Add("@SEMESTER", semesisId);
                    }
                    queryString += "GROUP BY CLASS.CLASS_ID,CLASS.CLASS_NAME";
                    var output =  _dbConnection.Query<DTO_Subject_Report>(queryString, parameters);
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// Lấy danh sách báo cáo tổng kết học kỳ
        /// </summary>
        /// <param name="semesisId"></param>
        /// <param name="scorePass"></param>
        /// <returns></returns>
        public List<DTO_Subject_Report> listDataSemesisReport(int semesisId = 0, float scorePass = 0)
        {
            try
            {
                DBConnect _dbContext = new DBConnect();
                var parameters = new DynamicParameters();
                using (IDbConnection _dbConnection = _dbContext.CreateConnection())
                {
                    var queryString = $@"SELECT CLASS.CLASS_NAME AS Class_Name,
		                                    count(STUDENT.STUDENT_ID) AS SiSo,
		                                    count(CASE WHEN {semesisId} = 1 and STUDENT.[AVG_S1] >= {scorePass} THEN STUDENT.[AVG_S1] 
                                                       WHEN {semesisId} = 2 and STUDENT.[AVG_S2] >= {scorePass} THEN STUDENT.[AVG_S2]
                                                        END) AS Pass
                                    FROM CLASS INNER JOIN STUDENT ON CLASS.CLASS_ID = STUDENT.CLASS_ID
                                    WHERE 1=1 ";
                    //if (semesisId != 0)
                    //{
                    //    queryString += " AND POINT.SEMESTER = @SEMESTER ";
                    //    parameters.Add("@SEMESTER", semesisId);
                    //}
                    queryString += "GROUP BY CLASS.CLASS_ID,CLASS.CLASS_NAME";
                    var output =  _dbConnection.Query<DTO_Subject_Report>(queryString, parameters);
                    return output.ToList();
                }
            }
            catch(Exception ex)
            {
                return null;
            }

        }
    }
}
