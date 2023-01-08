using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class UserControlCreateStudentVM : INotifyPropertyChanged
    {
        #region event PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        private string _fullname;
        private string _address;
        private string _email;
        private DateTime _birthday;
        public UserControlCreateStudentVM()
        {
            Birthday = DateTime.Now;
        }
        public string FullName
        {
            get { return _fullname; }
            set
            {
                if (value.Equals(_fullname))
                    return;
                _fullname = value;
                OnPropertyChanged("FullName");
            }
        }
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                if (value.Equals(_birthday))
                    return;
                _birthday = value;
                OnPropertyChanged("Birthday");
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                if (value.Equals(_address))
                    return;
                _address = value;
                OnPropertyChanged("Address");
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (value.Equals(_email))
                    return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }
    }
}
