using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Student
    {
        DAO_Student _daoStudent = new DAO_Student();
        DAO_Class _daoClassroom = new DAO_Class();
        BUS_Point _busMark = new BUS_Point();
        BUS_Config _busConfig = new BUS_Config();
        
        public bool InsertAStudent(Student _student)
        {
            var _fullNameRegex = @"^[A-Za-zÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠ-ỹ]+(?:\s[A-Za-zÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠ-ỹ]+)+$";
            var _isEmailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            int _maxAge = _busConfig.GetMaxAge();
            int _minAge = _busConfig.GetMinAge();
            if (!Regex.IsMatch(_student.FullName,_fullNameRegex,RegexOptions.IgnoreCase)) return false;
            if (DateTime.Now.Year - _student.Birthday.Year < _minAge || DateTime.Now.Year - _student.Birthday.Year > _maxAge) return false;
            if (_student.Address == "") return false;
            if (!Regex.IsMatch(_student.Email,_isEmailRegex,RegexOptions.IgnoreCase)) return false;
            return _daoStudent.InsertAStudent(_student);
        }

        public bool DeleteStudentByID(int ID) => _daoStudent.DeleteStudentByID(ID);

        public List<Student> GetAllStudent() => _daoStudent.GetAll();
        public List<Student> GetStudentByID(int? IDClass) => _daoStudent.GetStudentByClassID(IDClass);
        public List<Student> GetStudentByName(string NameStudent) => _daoStudent.GetStudentByName(NameStudent);
        public List<Student> GetStudentByNameAndClassID(string NameStudent, int? IDClass) => _daoStudent.GetStudentByNameAndClassID(NameStudent, IDClass);

        public List<Student> GetStudentByID(int IDClass) => _daoStudent.GetStudentByClassID(IDClass);
        /// <summary>
        /// Tra cứu học sinh
        /// </summary>
        /// <returns>Danh sách học sinh theo BM3</returns>
        public List<Student> ReadAllStudent()
        {
            var output = GetAllStudent();
            foreach (var _student in output)
            {
                _student.NameClass = _daoClassroom.GetNameClassByID(_student.Class_ID);
                _student.Score1stSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 1),2);
                _student.Score2ndSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 2),2);

            }
            return output;
        }

        public List<Student> ReadStudentByClassID(int? IDClass)
        {
            var output = GetStudentByID(IDClass);
            foreach (var _student in output)
            {
                _student.NameClass = _daoClassroom.GetNameClassByID(_student.Class_ID);
                _student.Score1stSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 1), 2);
                _student.Score2ndSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 2), 2);

            }
            return output;
        }

        public List<Student> ReadStudentByName(string NameStudent)
        {
            var output = GetStudentByName(NameStudent);
            foreach (var _student in output)
            {
                _student.NameClass = _daoClassroom.GetNameClassByID(_student.Class_ID);
                _student.Score1stSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 1), 2);
                _student.Score2ndSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 2), 2);
            }
            return output;
        }

        public List<Student> ReadStudentByNameAndClassID(string NameStudent,int? IDClass)
        {
            var output = GetStudentByNameAndClassID(NameStudent,IDClass);
            foreach (var _student in output)
            {
                _student.NameClass = _daoClassroom.GetNameClassByID(_student.Class_ID);
                _student.Score1stSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 1), 2);
                _student.Score2ndSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 2), 2);
            }
            return output;
        }

        public List<Student> ReadStudentByClassID(int IDClass)
        {
            var output = GetStudentByID(IDClass);
            foreach (var _student in output)
            {
                _student.NameClass = _daoClassroom.GetNameClassByID(_student.Class_ID);
                _student.Score1stSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 1), 2);
                _student.Score2ndSem = Math.Round((double)_busMark.CalAverageAllSubjectMarkBySemester(_student.Student_ID, 2), 2);

            }
            return output;
        }
        //update class id for student
        public void UpdateClassForStudent(int IDStudent, int IDClass)
        {
            _daoStudent.UpdateClassForStudent(IDStudent, IDClass);
        }
    }
}
