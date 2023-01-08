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
    /// Interaction logic for UserControlReadStudent.xaml
    /// </summary>
    public partial class UserControlReadStudent : UserControl
    {
        List<Student> _students = new List<Student>();
        BUS_Student _busStudent = new BUS_Student();
        BUS_Class _busClass = new BUS_Class();
        List<Class> _classNames = new List<Class>();
        BUS_Point _busPoint = new BUS_Point();
        public UserControlReadStudent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _students = _busStudent.ReadAllStudent();
            NullListCheck(_students);
            ListViewStudent.ItemsSource = _students;
            _classNames = _busClass.GetAllClass();
            ClassComboBox.ItemsSource = _classNames;
        }

        private void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClearFilterButton.Visibility == Visibility.Collapsed)
                ClearFilterButton.Visibility = Visibility.Visible;
            DisplayStudent();
        }

        private void ClearFilterButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClassComboBox.SelectedItem = null;
            ClearFilterButton.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DisplayStudent();
            }
        }
        private void DisplayStudent()
        {
            if (SearchTextBox.Text == null)
            {
                if (ClassComboBox.SelectedItem == null)
                {
                    _students = _busStudent.ReadAllStudent();
                    NullListCheck(_students);
                    ListViewStudent.ItemsSource = _students;
                }

                else
                {
                    int IDClass = _classNames[ClassComboBox.SelectedIndex].Class_ID;
                    _students = _busStudent.ReadStudentByClassID(IDClass);
                    NullListCheck(_students);
                    ListViewStudent.ItemsSource = _students;
                }
            }
            else
            {
                if (ClassComboBox.SelectedItem == null)
                {
                    string NameStudent = SearchTextBox.Text;
                    _students = _busStudent.ReadStudentByName(NameStudent);
                    NullListCheck(_students);
                    ListViewStudent.ItemsSource = _students;
                }

                else
                {
                    string NameStudent = SearchTextBox.Text;
                    int IDClass = _classNames[ClassComboBox.SelectedIndex].Class_ID;
                    _students = _busStudent.ReadStudentByNameAndClassID(NameStudent, IDClass);
                    NullListCheck(_students);
                    ListViewStudent.ItemsSource = _students;
                }
            }
        }
        private void NullListCheck(List<Student> Students)
        {
            if(Students.Count == 0)
            {
                NullListMessageTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                NullListMessageTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void ListViewStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DeleteRecordButton.Visibility == Visibility.Hidden)
            {
                DeleteRecordButton.Visibility = Visibility.Visible;
            }
            if (ReadRecordButton.Visibility == Visibility.Hidden)
            {
                ReadRecordButton.Visibility = Visibility.Visible;
            }
            if (ListViewStudent.SelectedItem == null)
            {
                if (DeleteRecordButton.Visibility == Visibility.Visible)
                {
                    DeleteRecordButton.Visibility = Visibility.Hidden;
                }
                if (ReadRecordButton.Visibility == Visibility.Visible)
                {
                    ReadRecordButton.Visibility = Visibility.Hidden;
                }
            }
        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn xóa hồ sơ học sinh đã chọn?\nWarning: Mọi dữ liệu về điểm của học sinh này sẽ bị xóa chung", "Thông Báo",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                int selectedListViewIndex = ListViewStudent.SelectedIndex;
                int selectedStudentID = _students[selectedListViewIndex].Student_ID;
                if (_busStudent.DeleteStudentByID(selectedStudentID))
                {
                    _busPoint.DeletePointByStudentID(selectedStudentID);
                    MessageBox.Show("Xóa thành công!", "Thông Báo",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DisplayStudent();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Không có dữ liệu bị xóa", "Thông Báo",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                //Do nothing
            }
        }

        private void ReadRecordButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedListViewIndex = ListViewStudent.SelectedIndex;
            Student selectedStudent = _students[selectedListViewIndex];
            TabPanel.Children.Clear();
            TabPanel.Children.Add(new UserControlReadStudentDetails(selectedStudent));
        }
        //getSelectedStudent
        public Student getSelectedStudent()
        {

            if (ListViewStudent.SelectedIndex !=- 1)
            {
                return _students[ListViewStudent.SelectedIndex];
            } else return null;
        }
    }
}
