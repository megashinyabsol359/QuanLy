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
namespace GUI
{
    /// <summary>
    /// Interaction logic for ChangeClassForStudent.xaml
    /// </summary>
    public partial class WindowChangeClassForStudent : Window
    {
        BUS_Class _busClass = new BUS_Class();
        BUS_Student _busStudent = new BUS_Student();
        BUS_Config _busConfig = new BUS_Config();
        private Student studentSelected;
        private Class classSelected;
        List<Class> _classes = new List<Class>();
        
        public WindowChangeClassForStudent(Student student,Class infoClass)
        {
            studentSelected = student;
            classSelected = infoClass;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            idStudent.Text = studentSelected.Student_ID.ToString();
            nameStudent.Text = studentSelected.FullName;
            nameClassBefore.Text = classSelected.Class_Name;
            _classes = _busClass.GetAllClass();
            ClassListComboBox.ItemsSource = _classes;
        }


        private void ChangeClass_Click(object sender, RoutedEventArgs e)
        {
            if (ClassListComboBox.SelectedIndex != -1)
            {
                if (MessageBox.Show("Xác nhận chuyển học sinh sang lớp " + _classes[ClassListComboBox.SelectedIndex].Class_Name + " ?", "Chuyển lớp", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {     if(_busConfig.GetMaxStudentClass()>classSelected.NumberMember)
                    {
                        //call function change class
                        _busStudent.UpdateClassForStudent(studentSelected.Student_ID, _classes[ClassListComboBox.SelectedIndex].Class_ID);
                        MessageBox.Show("Chuyển lớp thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi, số lượng lớp muốn chuyển đã đầy!");
                    }
                }
            }
            else { MessageBox.Show("Chưa chọn lớp!"); }
        }
    }
}
