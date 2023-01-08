using BUS;
using DTO;
using GUI.UserControls;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BUS_Student _busStudent = new BUS_Student();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlReadStudent();
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ToUCStudentButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlReadStudent();
        }

        private void ToUCClassButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlReadClass();
        }

        private void ToUCSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlReadSubject();
        }

        private void ToUCMarkButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlReadMark();
        }
        private void ToUCSubjectReportButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlSubjectReport();
        }

        private void ToUCConfigButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UserControlReadConfig();
        }

        private void AddButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ToUCStudentButton.IsChecked == true)
            {
                DataContext = new UserControlCreateStudent();
            }
            else if (ToUCClassButton.IsChecked == true)
            {
                DataContext = new UserControlCreateClass();
            }
            else
            {
                //do nothing
            }
        }
        public void MovePageFromClassToStudent(object sender, MouseButtonEventArgs e)
        {
            ToUCClassButton.IsChecked = false;
            ToUCStudentButton.IsChecked = true;
        }
    }
}
