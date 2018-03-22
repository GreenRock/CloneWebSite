using System.ComponentModel.DataAnnotations;
using Download.Common.Resources;

namespace Download.AppMain.Models
{
    public class SettingModel
    {
        [Required(ErrorMessageResourceName = "InputRequied", ErrorMessageResourceType = typeof(ContentStatic))]
        public string CssFolder { get; set; }
        [Required(ErrorMessageResourceName = "InputRequied", ErrorMessageResourceType = typeof(ContentStatic))]
        public string ScriptFolder { get; set; }
        [Required(ErrorMessageResourceName = "InputRequied", ErrorMessageResourceType = typeof(ContentStatic))]
        public string ImageFolder { get; set; }
        [Required(ErrorMessageResourceName = "InputRequied", ErrorMessageResourceType = typeof(ContentStatic))]
        public string FontFolder { get; set; } 
    }
}