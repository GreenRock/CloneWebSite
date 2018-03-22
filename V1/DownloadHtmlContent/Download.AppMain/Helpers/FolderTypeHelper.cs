using System.Collections.Generic;
using System.Linq;
using Download.Common.Extensions;
using Download.Models;

namespace Download.AppMain.Helpers
{
    public class FolderTypeHelper
    {
        private static IEnumerable<string> _folderTypeList;
        public static IEnumerable<string> FolderTypeList
        {
            get
            {
                if (_folderTypeList != null)
                    return _folderTypeList;

                var folderTypeList = EnumExtension.GetItems<FolderType>().Select(s => s.GetCustomAttributeDescription());
                _folderTypeList = folderTypeList;
                return _folderTypeList;
            }
        }
    }
}
