using BUS;
using DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlAddClass.xaml
    /// </summary>
    public partial class UserControlCreateClass : UserControl
    {
        List<Student> _students = new List<Student>();
        BUS_Class _busClass = new BUS_Class();
        BUS_Config _busConfig = new BUS_Config();
        Class _class = new Class();
        public UserControlCreateClass()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private bool ValidateInformation(string className, string classGroup)
        {
            if (className.IndexOf(classGroup) == -1)
            {
                MessageBox.Show("Lỗi! Tên lớp phải thuộc khối phù hợp");
                return false;
            }
            else if (_busConfig.GetMaxClass() <= _busClass.getNumberClass())
            {
                MessageBox.Show("Lỗi! Số lớp đã đầy.");
                return false;
            }
            else if (_busClass.checkExistClass(className) == true)
            {
                MessageBox.Show("Lỗi! Lớp đã tồn tại.");
                return false;
            }
                return true;
        }
        private void SaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            _class.Class_Name = ClassNameTextBox.Text.ToUpper();
            string class_group = ClassGroupCombobox.Text;
            
            if (ValidateInformation(_class.Class_Name, class_group)){
                _class.Class_Group = int.Parse(class_group.ToString());
                if (_busClass.InsertAClass(_class))
                {
                    MessageBox.Show("Thêm lớp học thành công");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, vui lòng xem lại các trường dữ liệu!");
                }
            } }
    }
}



