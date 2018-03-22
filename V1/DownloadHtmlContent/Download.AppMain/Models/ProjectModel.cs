using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Download.AppMain.Annotations;

namespace Download.AppMain.Models
{
    public sealed class ProjectModel : INotifyPropertyChanged
    {
        private string _location = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public string Location
        {
            get { return _location; }
            set
            {
                if (EqualityComparer<string>.Default.Equals(_location, value)) return;

                _location = value;
                NotifyPropertyChanged(nameof(Location));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
