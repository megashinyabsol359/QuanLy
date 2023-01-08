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
    /// Interaction logic for UserControlReadClass.xaml
    /// </summary>
    public partial class UserControlReadClass : UserControl
    {
        List<Class> _classes = new List<Class>();
        List<Student> _students = new List<Student>();
        BUS_Student _busStudent = new BUS_Student();
        BUS_Class _busClass = new BUS_Class();
        BUS_Config _busConfig = new BUS_Config();
        List<Class> _classGroups = new List<Class>();
        public UserControlReadClass()
        {
            InitializeComponent();
        }
        private void NullListCheck(List<Class> Classes)
        {
            if (Classes.Count == 0)
            {
                NullListMessageTextBlock.Visibility = Visibility.Visible;
                
            }
            else
            {
                NullListMessageTextBlock.Visibility = Visibility.Hidden;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _classes = _busClass.ReadAllClass();
            NullListCheck(_classes);
            ListViewClass.ItemsSource = _classes;
            _classGroups = _busClass.GetAllClassGroup();
            ClassGroupComboBox.ItemsSource = _classGroups;
        }
        private void ClearFilterButtonClass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClassGroupComboBox.SelectedItem = null;
            ClearFilterButtonClassGroup.Visibility = Visibility.Collapsed;
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DisplayClass();
            }
        }
        private void ClassGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClearFilterButtonClassGroup.Visibility == Visibility.Collapsed)
                ClearFilterButtonClassGroup.Visibility = Visibility.Visible;
            DisplayClass();
        }
        private void DisplayClass()
        {
           
            if (SearchTextBox.Text == null)
            {
                if (ClassGroupComboBox.SelectedItem == null)
                {
                    _classes = _busClass.ReadAllClass();
                    NullListCheck(_classes);
                    ListViewClass.ItemsSource = _classes;
                }

                else
                {
                    int classGroup = _classGroups[ClassGroupComboBox.SelectedIndex].Class_Group;
                    _classes = _busClass.ReadClassByClassGroup(classGroup);
                    NullListCheck(_classes);
                    ListViewClass.ItemsSource = _classes;
                }
            }
            else
            {
                if (ClassGroupComboBox.SelectedItem == null)
                {
                    string NameClass = SearchTextBox.Text;
                    _classes = _busClass.ReadClassByName(NameClass);
                    NullListCheck(_classes);
                    ListViewClass.ItemsSource = _classes;
                }

                else
                {
                    string NameClass = SearchTextBox.Text;
                    int Class_Group = _classGroups[ClassGroupComboBox.SelectedIndex].Class_Group;
                    _classes = _busClass.ReadClassByNameAndClassGroup(NameClass, Class_Group);
                    NullListCheck(_classes);
                    ListViewClass.ItemsSource = _classes;
                }
            }
        }


        private void ListViewClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewClass.SelectedIndex != -1)
            {    
                int index = ListViewClass.SelectedIndex;
                _students = _busStudent.ReadStudentByClassID(_classes[index].Class_ID);
                var output = _students;
                ListViewStudentList.ItemsSource = output;
            }
        }

        private void DeleteClass_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewClass.SelectedIndex != -1)
            {
                if (MessageBox.Show( "Xác nhận xóa lớp "+_classes[ListViewClass.SelectedIndex].Class_Name+" ?", "Xóa lớp", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    //get function delete class
                    _busClass.DeleteClass(_classes[ListViewClass.SelectedIndex].Class_ID);
                    MessageBox.Show("Xóa lớp thành công!");
                    
                    _classGroups = _busClass.GetAllClassGroup();
                    ClassGroupComboBox.ItemsSource = _classGroups;
                    DisplayClass();
                }
            }
        }

        private void ChangeClass_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewStudentList.SelectedIndex != -1 && ListViewClass.SelectedIndex != -1)
            {
               int indexClass = ListViewClass.SelectedIndex;
                int indexStudent = ListViewStudentList.SelectedIndex;

                WindowChangeClassForStudent objChange = new WindowChangeClassForStudent(_students[indexStudent],_classes[indexClass]);
                objChange.ShowDialog();
                DisplayClass();
                ListViewClass.SelectedIndex = 0;
            }
        }

        private void AddStudentsToClass_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewClass.SelectedIndex != -1)
            {
                if (_classes[ListViewClass.SelectedIndex].NumberMember < _busConfig.GetMaxStudentClass())
                {
                    WindowAddStudentsToClass objAdd = new WindowAddStudentsToClass(_classes[ListViewClass.SelectedIndex]);
                    objAdd.Show();
                    DisplayClass();
                }
                else MessageBox.Show("Số lượng lớp đã đầy!");
            }
        }
    }
}
