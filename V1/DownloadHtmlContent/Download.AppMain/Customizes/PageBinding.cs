using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Download.Common.Resources;
using Download.Models.PageModels;

namespace Download.AppMain.Customizes
{
    public class PageBinding : BindingList<PageModel>
    {
        protected override void InsertItem(int index, PageModel item)
        {
            if(item == null || string.IsNullOrEmpty(item.PageName))
                return;

            base.InsertItem(index, item);
        }

        protected override void OnListChanged(ListChangedEventArgs eventArgs)
        {
            if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
            {
                var listObject = this;

                var newIndex = eventArgs.NewIndex;
                var dataByIndex = listObject[newIndex];

                var linkModelCount = listObject.Count(c => c.PageName == dataByIndex.PageName.Trim());
                if (linkModelCount > 1)
                {
                    MessageBox.Show(ContentStatic.PageNameReallyExist, ContentStatic.DuplicateData,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                        listObject.RemoveAt(newIndex);
                }
            }
            base.OnListChanged(eventArgs);
        }

        public void ChangeStatus(int index, PageStatus pageStatus)
        {
            this[index].PageStatus = pageStatus;
        }
    }
}