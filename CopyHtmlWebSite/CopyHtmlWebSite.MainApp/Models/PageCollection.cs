using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using CopyHtmlWebSite.Core.Extensions;
using CopyHtmlWebSite.MainApp.Properties;
using CopyHtmlWebSite.MainApp.ViewModels;

namespace CopyHtmlWebSite.MainApp.Models
{
    public class PageCollection : ObservableCollection<PageViewModel>
    {
        public PageCollection()
        {

        }

        public PageCollection(List<PageViewModel> pages) : base(pages)
        {

        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            OnChanged?.Invoke(e);
        }

        public Action<NotifyCollectionChangedEventArgs> OnChanged;

        public bool CheckExistByName(string name)
        {
            if (name.HasNoText()) return false;
            return this.Any(x => x.PageName.Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        public PageViewModel GetByName(string name)
        {
            if (name.HasNoText()) return null;
            return this.FirstOrDefault(x => x.PageName.Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        public void AddPage(PageViewModel model)
        {
            model.PageName = Regex.Replace(model.PageName, RegexResource.SpecialChar, string.Empty);
            this.Add(model);
        }
    }
}
