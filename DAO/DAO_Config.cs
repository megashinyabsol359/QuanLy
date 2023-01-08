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
    public class DAO_Config : DBConnect
    {
        public List<Config> GetAll()
        {
            DBConnect _dbContext = new DBConnect();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                var output = _dbConnection.Query<Config>($"select * from CONFIG").ToList();
                return output;
            }
        }
        public Config GetConfig()
        {
            DBConnect _dbContext = new DBConnect();
            var output = new Config();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                try
                {
                    output = _dbConnection.Query<Config>($"select * from CONFIG").FirstOrDefault();
                    return output;
                }
                catch (Exception ex)
                {
                    var mess = ex.Message;
                    return null;
                }
            }
        }
        public int updateConfig(Config config)
        {
            DBConnect _dbContext = new DBConnect();
            var parameters = new DynamicParameters();
            using (IDbConnection _dbConnection = _dbContext.CreateConnection())
            {
                try
                {
                    var updateString = @"UPDATE [dbo].[CONFIG]
                                   SET [MIN_AGE] = @MIN_AGE
                                      ,[MAX_AGE] = @MAX_AGE
                                      ,[SUBJECT_POINT_STANDARDS] = @SUBJECT_POINT_STANDARDS
                                      ,[MAX_RATIO] = @MAX_RATIO
                                      ,[MAX_STUDENT_CLASS] = @MAX_STUDENT_CLASS
                                      ,[MAX_CLASS] = @MAX_CLASS
                                      ,[MAX_SUBJECT] = @MAX_SUBJECT
                                 WHERE CONFIG_ID = @CONFIG_ID";
                    parameters.Add("@MIN_AGE", config.Min_Age);
                    parameters.Add("@MAX_AGE", config.Max_Age);
                    parameters.Add("@SUBJECT_POINT_STANDARDS", config.Subject_Point_Standards);
                    parameters.Add("@MAX_RATIO", config.Max_Ratio);
                    parameters.Add("@MAX_STUDENT_CLASS", config.Max_Student_Class);
                    parameters.Add("@MAX_CLASS", config.Max_Class);
                    parameters.Add("@MAX_SUBJECT", config.Max_Subject);
                    parameters.Add("@CONFIG_ID", config.Config_ID);
                    var output = _dbConnection.Execute(updateString,parameters);
                    return output;
                }
                catch (Exception ex)
                {
                    var mess = ex.Message;
                    return 0;
                }
            }
        }
    }
}
