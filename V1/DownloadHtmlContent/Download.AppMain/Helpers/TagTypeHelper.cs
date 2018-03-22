using System.Collections.Generic;
using System.Linq;
using Download.Common.Extensions;
using Download.Models;

namespace Download.AppMain.Helpers
{
    public class TagTypeHelper
    {
        private static IEnumerable<string> _tagList;

        public static IEnumerable<string> TagList
        {
            get
            {
                if (_tagList != null)
                    return _tagList;

                _tagList = TagTypeList.Select(c => c.GetCustomAttributeDescription().ToLower());

                return _tagList;
            }
        }

        private static IEnumerable<TagType> _tagTypeList;

        public static IEnumerable<TagType> TagTypeList
        {
            get
            {
                if (_tagList != null)
                    return _tagTypeList;

                _tagTypeList = EnumExtension.GetItems<TagType>();

                return _tagTypeList;
            }
        }
    }
}
