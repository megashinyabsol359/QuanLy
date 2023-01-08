using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserControlReadConfig.xaml
    /// </summary>
    public partial class UserControlReadConfig : UserControl
    {
        BUS_Config busConfig = new BUS_Config();
        Config dtoConfig = new Config();
        public UserControlReadConfig()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dtoConfig = busConfig.GetConfig();
            this.MaxAge.Text = dtoConfig.Max_Age.ToString();
            this.MinAge.Text = dtoConfig.Min_Age.ToString();
            this.MaxClass.Text = dtoConfig.Max_Class.ToString();
            this.MaxSubject.Text = dtoConfig.Max_Subject.ToString();
            this.SubjectPointStandards.Text = dtoConfig.Subject_Point_Standards.ToString();
            this.MaxStudentClass.Text = dtoConfig.Max_Student_Class.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex _isInt = new Regex("[^0-9.-]+");
            var _isFloat = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (_isInt.IsMatch(this.MinAge.Text.ToString()) || Int32.Parse(this.MinAge.Text.ToString()) < 0)
            {
                MessageBox.Show("Tuổi tối thiểu phải là số nguyên dương !");
                this.MinAge.Focus();
                return;
            }
            if (_isInt.IsMatch(this.MaxAge.Text.ToString()) || Int32.Parse(this.MaxAge.Text.ToString()) < 0)
            {
                MessageBox.Show("Tuổi tối đa phải là số nguyên dương !");
                this.MaxAge.Focus();
                return;
            }
            if (_isInt.IsMatch(this.MaxClass.Text.ToString()) || Int32.Parse(this.MaxClass.Text.ToString()) < 0)
            {
                MessageBox.Show("Số lớp tối đa phải là số nguyên dương !");
                this.MaxClass.Focus();
                return;
            }
            if (_isInt.IsMatch(this.MaxSubject.Text.ToString()) || Int32.Parse(this.MaxSubject.Text.ToString()) < 0)
            {
                MessageBox.Show("Số môn tối đa phải là số nguyên dương !");
                this.MaxSubject.Focus();
                return;
            }
            if (_isInt.IsMatch(this.MaxStudentClass.Text.ToString()) || Int32.Parse(this.MaxStudentClass.Text.ToString()) < 0)
            {
                MessageBox.Show("Sỉ số tối đa phải là số nguyên dương !");
                this.MaxStudentClass.Focus();
                return;
            }
            if (_isFloat.IsMatch(this.SubjectPointStandards.Text.ToString().Replace('.', ',')) || Double.Parse(this.SubjectPointStandards.Text.ToString()) < 0)
            {
                MessageBox.Show("Điểm đạt môn học phải là số nguyên dương hoặc số thực dương !");
                this.SubjectPointStandards.Focus();
                return;
            }
            if (Int32.Parse(this.MaxAge.Text.ToString()) < Int32.Parse(this.MinAge.Text.ToString()))
            {
                MessageBox.Show("Tuổi tối đa phải lớn hơn tuổi tối thiểu");
                this.MinAge.Focus();
                return;
            }
            dtoConfig.Max_Age = Int32.Parse(this.MaxAge.Text.ToString());
            dtoConfig.Min_Age = Int32.Parse(this.MinAge.Text.ToString());
            dtoConfig.Max_Class = Int32.Parse(this.MaxClass.Text.ToString());
            dtoConfig.Max_Subject = Int32.Parse(this.MaxSubject.Text.ToString());
            dtoConfig.Subject_Point_Standards = Double.Parse(this.SubjectPointStandards.Text.ToString().Replace(',', '.'));
            dtoConfig.Max_Student_Class = Int32.Parse(this.MaxStudentClass.Text.ToString());
            var result = busConfig.updateConfig(dtoConfig);
            if (result > 0)
            {
                MessageBox.Show("Cập nhật thành công");
                UserControl_Loaded(sender, e);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại");
            }
        }
    }
}
