using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.ViewModels.ViewModelBases;

namespace CopyHtmlWebSite.MainApp.ViewModels
{
    public class SelectedItemViewModel : ViewModelBase, ISelectedItem
    {
        public string Id { get; set; }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _value;
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
