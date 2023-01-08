using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.Class;

namespace BUS
{
    public class BUS_Class
    {
        DAO_Class _daoClass = new DAO_Class();
        DAO_Student _daoStudent = new DAO_Student();
        BUS_Config _busConfig = new BUS_Config();
        public List<Class> GetAllClass() => _daoClass.GetAll();

        public bool InsertAClass(Class _class){
            if (_class.Class_Name == "" || _class.Class_Name==" ") return false;
                _daoClass.InsertAClass(_class);
                return true;
        }
        public List<Class> ReadClassByClassGroup(int class_group)
        {
            var output= _daoClass.ReadClassByClassGroup(class_group);
            getNumberMember(output);
            return output;
        }
        public List<Class> ReadClassByID(int class_id)
        {
            var output= _daoClass.ReadClassByID(class_id);
            getNumberMember(output);
            return output;
        }
        public List<Class> ReadClassByNameAndClassGroup(string NameClass, int? Class_Group)
        {
           var output= _daoClass.ReadClassByByNameAndClassGroup(NameClass, Class_Group);
            getNumberMember(output);
            return output;
        }
        public List<Class> ReadClassByName(string NameClass)
        {
            var output=_daoClass.ReadClassByName(NameClass);
            getNumberMember(output);
            return output;
        }
        public List<Class> GetAllClassGroup() => _daoClass.GetAllClassGroup();
        public List<Class> ReadAllClass()
        {
            var output = GetAllClass();
            foreach (var _class in output)
            {
                int count = 0;
                List<Student> listStudent = new List<Student>();
                listStudent = _daoStudent.GetStudentByClassID(_class.Class_ID);
                foreach(var student in listStudent)
                {
                    count++;
                }
                _class.NumberMember = count;
            }
            return output;
        }
        public void getNumberMember(List<Class> output)
        {
            foreach (var _class in output)
            {
                int count = 0;
                List<Student> listStudent = new List<Student>();
                listStudent = _daoStudent.GetStudentByClassID(_class.Class_ID);
                foreach (var student in listStudent)
                {
                    count++;
                }
                _class.NumberMember = count;
            }
            
        }
        public int getNumberClass()
        {
            int count = 0;
            var output = GetAllClass();
            foreach (var _class in output)
            {
                
                    count++;
                
               
            }
            return count;

        }
        public bool checkExistClass(string className)
        {
            if (ReadClassByName(className).Count == 1) return true;
            else return false;
        }
        //Xóa lớp 
        public void DeleteClass(int idClass)
        {
            _daoClass.DeleteClass(idClass);
        }
        /// <summary>
        /// Xử lý lý dữ liệu trả về của báo cáo tổng kết theo môn học
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="semesisId"></param>
        /// <returns></returns>
        public List<DTO_Subject_Report> listDataSubjectReport(int subjectId, int semesisId)
        {
            var getScorePass = _busConfig.GetScorePass();
            var lstSubjectReport = _daoClass.listDataSubjectReport(subjectId, semesisId, getScorePass);
            var lstGridView = new List<DTO_Subject_Report>();
            int i = 1;
            foreach (var item in lstSubjectReport)
            {
                var temp = new DTO_Subject_Report();
                temp.Stt = i++;
                temp.Class_Name = item.Class_Name;
                temp.SiSo = item.SiSo;
                temp.Pass = item.Pass;
                var tempRate = ((float)item.Pass / item.SiSo) * 100;
                temp.Rate = Math.Round(tempRate, 2) + " %";
                lstGridView.Add(temp);
            }
            return lstGridView;
        }
        /// <summary>
        /// Xử lý dữ liệu danh sách báo cáo theo học kỳ
        /// </summary>
        /// <param name="semesisId"></param>
        /// <returns></returns>
        public List<DTO_Subject_Report> listDataSemesisReport(int semesisId)
        {
            var getScorePass = _busConfig.GetScorePass();
            var lstSubjectReport = _daoClass.listDataSemesisReport(semesisId, getScorePass);
            var lstGridView = new List<DTO_Subject_Report>();
            int i = 1;
            foreach (var item in lstSubjectReport)
            {
                var temp = new DTO_Subject_Report();
                temp.Stt = i++;
                temp.Class_Name = item.Class_Name;
                temp.SiSo = item.SiSo;
                temp.Pass = item.Pass;
                var tempRate = ((float)item.Pass / item.SiSo) * 100;
                temp.Rate = Math.Round(tempRate, 2) + " %";
                lstGridView.Add(temp);
            }
            return lstGridView;
        }
    }
}
