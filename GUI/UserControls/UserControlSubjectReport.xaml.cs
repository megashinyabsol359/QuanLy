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
using static BUS.BUS_Class;
using static DTO.Class;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlSubjectReport.xaml
    /// </summary>
    public partial class UserControlSubjectReport : UserControl
    {
        List<Class> lstClass = new List<Class>();
        List<Subject> lstSubject = new List<Subject>();
        BUS_Student _busStudent = new BUS_Student();
        BUS_Class _busClass = new BUS_Class();
        BUS_Subject busSubject = new BUS_Subject();
        List<Class> _classGroups = new List<Class>();
        public UserControlSubjectReport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load cái defualt cần có trong view Báo cáo môn học
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lstSubject = busSubject.GetAllSubject();
            for (int i = 0; i < lstSubject.ToList().Count(); i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = lstSubject[i].Subject_Name.ToString();
                item.Tag = lstSubject[i].Subject_ID;
                if (i == 0)
                {
                    cbbSubject.SelectedValue = item;
                }
                cbbSubject.Items.Add(item);
            }
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Học kỳ 1";
            item1.Tag = 1;
            cbbSemesis.Items.Add(item1);
            cbbSemesis.SelectedValue = item1;
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Học kỳ 2";
            item2.Tag = 2;
            cbbSemesis.Items.Add(item2);
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Học kỳ 1";
            item3.Tag = 1;
            cbbSemesisSummary.Items.Add(item3);
            cbbSemesisSummary.SelectedValue = item3;
            ComboBoxItem item4 = new ComboBoxItem();
            item4.Content = "Học kỳ 2";
            item4.Tag = 2;
            cbbSemesisSummary.Items.Add(item4);
            // Todo: list view
            this.LoadSubjectReportData();
            this.LoadSemesisReportData();
        }
        private void SubjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // todo: list view
            this.LoadSubjectReportData();

        }
        private void SemesisComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // todo: list view
            this.LoadSubjectReportData();
        }
        private void SemesisSummaryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // todo: list view
            this.LoadSemesisReportData();
        }
        /// <summary>
        /// View báo cáo môn học
        /// </summary>
        private void LoadSubjectReportData()
        {
            try
            {
                int subjectId = 0;
                if (cbbSubject.SelectedItem != null)
                {
                    subjectId = (int)((ComboBoxItem)cbbSubject.SelectedItem).Tag;
                }
                int semesisId = 0;
                if (cbbSemesis.SelectedItem != null)
                {
                    semesisId = (int)((ComboBoxItem)cbbSemesis.SelectedItem).Tag;
                }
                if (subjectId != 0 && semesisId != 0)
                {
                    var dataSource = _busClass.listDataSubjectReport(subjectId, semesisId);
                    PassList.ItemsSource = null;
                    PassList.ItemsSource = dataSource;
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
        private void LoadSemesisReportData()
        {
            try
            {
                int semesisId = 0;
                if (cbbSemesisSummary.SelectedItem != null)
                {
                    semesisId = (int)((ComboBoxItem)cbbSemesisSummary.SelectedItem).Tag;
                }
                if (semesisId != 0)
                {
                    var dataSource = _busClass.listDataSemesisReport(semesisId);
                    ReportSemesis.ItemsSource = null;
                    ReportSemesis.ItemsSource = dataSource;
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
    }
}
