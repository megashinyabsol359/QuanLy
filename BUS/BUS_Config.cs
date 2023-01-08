using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Config
    {
        DAO_Config _daoConfig = new DAO_Config();
        public List<Config> GetAllConfig() => _daoConfig.GetAll();
        public Config GetConfig() => _daoConfig.GetConfig();
        public int GetMinAge()
        {
            return (int)_daoConfig.GetAll()[0].Min_Age;
        }
        public int GetMaxAge()
        {
            return (int)_daoConfig.GetAll()[0].Max_Age;
        }
        public float GetScorePass()
        {
            return (float)_daoConfig.GetConfig().Subject_Point_Standards;
        }
        public int GetMaxRatio()
        {
            return (int)_daoConfig.GetConfig().Max_Ratio;
        }
        public int GetMaxStudentClass()
        {
            return (int)_daoConfig.GetConfig().Max_Student_Class;
        }
        public int GetMaxSubject()
        {
            return (int)_daoConfig.GetConfig().Max_Subject;
        }
        public int GetMaxClass()
        {
            return (int)_daoConfig.GetConfig().Max_Class;
        }
        public int updateConfig(Config config)
        {
            try
            {
                return _daoConfig.updateConfig(config);
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

    }
}
