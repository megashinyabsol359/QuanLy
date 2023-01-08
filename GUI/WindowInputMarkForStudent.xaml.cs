using System;
using BUS;
using DTO;
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
using Point = DTO.Point;

namespace GUI
{
	/// <summary>
	/// Interaction logic for WindowInputMarkForStudent.xaml
	/// </summary>
	public partial class WindowInputMarkForStudent : Window
	{
		BUS_Point _busPoint = new BUS_Point();
		private int idStudent { get; set; }
		private int semester { get; set; }
		private int idSubject { get; set; }
		private List<Point> pointList;
		public WindowInputMarkForStudent(int idStudentInput, int semeterInput, int idSubjectInput)
		{
			idStudent = idStudentInput;
			semester = semeterInput;
			idSubject = idSubjectInput;
			pointList = _busPoint.getAll();
			InitializeComponent();
		}

		private bool checkContain(Point _point)
		{
			for (int i = 0; i < pointList.Count; i++)
			{
				Point temp = pointList[i];
				if (temp.Student_ID == _point.Student_ID && temp.Semester == _point.Semester && temp.Subject_ID == _point.Subject_ID)
				{
					return true;
				}
			}
			return false;
		}

		private bool checkParse(string str)
		{
			double value;
			if (double.TryParse(str, out value))
			{
				return true;
			}
			else return false;
		}

		private void buttonInputMark_Click(object sender, RoutedEventArgs e)
		{
			string mark15Input = textBoxMark15.Text;
			string mark45Input = textBoxMark45.Text;
			string markCkInput = textBoxMarkCk.Text;
			if(checkParse(mark15Input) == false)
			{
				MessageBox.Show("Vui lòng kiểm tra điểm 15 phút đã nhập", "Notice");
			}
			else if (checkParse(mark45Input) == false)
			{
				MessageBox.Show("Vui lòng kiểm tra điểm 1 tiết đã nhập", "Notice");
			}
			else if (checkParse(markCkInput) == false)
			{
				MessageBox.Show("Vui lòng kiểm tra điểm Cuối Kì đã nhập", "Notice");
			}
			else
			{
				double mark15 = double.Parse(mark15Input);
				double mark45 = double.Parse(mark45Input);
				double markCk = double.Parse(markCkInput);

				if (mark15 < 0 || mark15 > 10)
				{
					MessageBox.Show("Điểm 15 phút không được 0 < và > 10", "Notice");
				}
				else if (mark45 < 0 || mark45 > 10)
				{
					MessageBox.Show("Điểm 1 tiết không được 0 < và > 10", "Notice");
				}
				else if (markCk < 0 || markCk > 10)
				{
					MessageBox.Show("Điểm Cuối Kì không được 0 < và > 10", "Notice");
				}
				else
				{					
					Point _point = new Point(idSubject, idStudent, semester, mark15, mark45, markCk);

					if (checkContain(_point))
					{
						bool update = _busPoint.updatePointForStudent(_point);
						if (update == true)
						{
							MessageBox.Show("Done!", "Notice");
							this.Close();
						}
					}
					else
					{
						bool add = _busPoint.InsertPointForStudent(_point);
						if (add == true)
						{
							MessageBox.Show("Done!", "Notice");
							this.Close();
						}
					}
				}
			}
		}
	}
}
