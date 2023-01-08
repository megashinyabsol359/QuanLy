using System;
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
using System.Windows.Shapes;
using BUS;
using DTO;
using GUI.UserControls;
namespace GUI
{
    /// <summary>
    /// Interaction logic for WindowAddStudentsToClass.xaml
    /// </summary>
    public partial class WindowAddStudentsToClass : Window
    {
        BUS_Student _busStudent = new BUS_Student();
        BUS_Config _busConfig = new BUS_Config();
        UserControlReadStudent UCStudent = new UserControlReadStudent();
        List<Student> _students = new List<Student>();
        private Class classSelected;
        public WindowAddStudentsToClass(Class infoClass)
        {

            classSelected = infoClass;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = UCStudent;

        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
          

            if (UCStudent.ListViewStudent.SelectedIndex!=-1 )
            {
                Student selectedStudent = UCStudent.getSelectedStudent();
                if (MessageBox.Show("Xác nhận thêm học sinh " + selectedStudent.FullName + " vào lớp " + classSelected.Class_Name + " ?", "Thêm học sinh vào lớp", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    _busStudent.UpdateClassForStudent(selectedStudent.Student_ID, classSelected.Class_ID);
                    MessageBox.Show("Thêm học sinh thành công!");
                    UCStudent = new UserControlReadStudent();
                    DataContext = UCStudent;
                }

            }
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            
                UCStudent = new UserControlReadStudent();
                DataContext = UCStudent;
            
        }
    }
}
