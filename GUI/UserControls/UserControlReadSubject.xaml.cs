using System;
using DTO;
using BUS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlReadSubject.xaml
    /// </summary>
    /// 

    public partial class UserControlReadSubject : UserControl
    {

        BUS_StudentPointSubject _busStudentPointSubject = new BUS_StudentPointSubject();
        List<StudentPointSubject> studentPointSubjectList = new List<StudentPointSubject>();

        BUS_Subject _busSubject = new BUS_Subject();
        List<Subject> subjectList = new List<Subject>();

        BUS_Class _busClass = new BUS_Class();
        List<Class> classList = new List<Class>();

		BUS_Student _busStudent = new BUS_Student();
		List<Student> studentList = new List<Student>();

        public UserControlReadSubject()
        {
            InitializeComponent();
        }

		private int getIdClass(string className)
		{
			foreach (var cl in classList)
			{
				if (cl.Class_Name.CompareTo(className) == 0)
				{
					return cl.Class_ID;
				}
			}
			return 0;
		}

		private int getIdSubject(string subjectName)
		{
			foreach (var subject in subjectList)
			{
				if (subject.Subject_Name.CompareTo(subjectName) == 0)
				{
					return subject.Subject_ID;
				}
			}
			return 0;
		}

		private int getIdStudent(string fullName)
		{
			foreach (var student in studentList)
			{
				if (student.FullName.CompareTo(fullName) == 0)
				{
					return student.Student_ID;
				}
			}
			return 0;
		}

		private int count() => studentPointSubjectList.Count();

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
			classList = _busClass.GetAllClass();
			subjectList = _busSubject.getAllSubject();
			studentList = _busStudent.GetAllStudent();
			comboBoxClass.ItemsSource = classList;
			comboBoxSubject.ItemsSource = subjectList;
		}

		private void buttonSearch_Click(object sender, RoutedEventArgs e)
		{
			int selectClass = comboBoxClass.SelectedIndex;
			int selectSubject = comboBoxSubject.SelectedIndex;
			int semester = comboBoxSemester.SelectedIndex;
			if(selectClass == -1)
			{
				MessageBox.Show("Vui Lòng Chọn Lớp!", "Notice");
			}
			else if (selectSubject == -1 )
			{
				MessageBox.Show("Vui Lòng Chọn Môn Học!", "Notice");
			}
			else if(semester == -1)
			{
				MessageBox.Show("Vui Lòng Chọn Học Kì!", "Notice");
			}
			else
			{
				int selectSemester = 0;
				if (semester == 0)
					selectSemester = 1;
				else if (semester == 1)
					selectSemester = 2;

				int idCl = getIdClass(classList[selectClass].Class_Name);
				int idSu = getIdSubject(subjectList[selectSubject].Subject_Name);
				studentPointSubjectList = _busStudentPointSubject.getStudentPointSubject(selectSemester, idCl, idSu);
				int i = 0;
				foreach(var StudentPointSubject in studentPointSubjectList)
				{
					i++;
					StudentPointSubject.Stt = i;
				}	
				ListViewPoint.ItemsSource = studentPointSubjectList;
			}
		}

		private void buttonInputMark_Click(object sender, RoutedEventArgs e)
		{
			if(ListViewPoint.SelectedIndex == -1)
			{
				MessageBox.Show("Vui Lòng Chọn Học Sinh Để Nhập Điểm!", "Notice");
			}
			else
			{
				int selectClass = comboBoxClass.SelectedIndex;
				int selectSubject = comboBoxSubject.SelectedIndex;
				int selectSemester = comboBoxSemester.SelectedIndex;

				int semester = 0;
				if (selectSemester == 0)
					semester = 1;
				else if (selectSemester == 1)
					semester = 2;

				int idSu = getIdSubject(subjectList[selectSubject].Subject_Name);
				int idStudent = getIdStudent(studentPointSubjectList[ListViewPoint.SelectedIndex].FullName);
				WindowInputMarkForStudent windowInputMarkForStudent = new WindowInputMarkForStudent(idStudent, semester, idSu);
				windowInputMarkForStudent.Show();
			}
		}

	}
}
