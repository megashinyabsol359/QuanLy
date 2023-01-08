using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class UserControlCreateClassVM : INotifyPropertyChanged
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
        private string _className;
        
        public string ClassName
        {
            get { return _className; }
            set
            {
                if (value.Equals(_className))
                    return;
                _className = value;
                OnPropertyChanged("Class_Name");
            }
        }

    }
}
