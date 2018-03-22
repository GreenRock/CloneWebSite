using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Download.Testing
{
    using Services.WebClientServices;

    [TestClass()]
    public class GetContentTest
    {
        [TestMethod()]
        public void TestDownload()
        {
            var isImage = IsImage("images/bg-top-content.jpg");
            Assert.IsTrue(isImage);
        }

        [TestMethod()]
        public void HandleFile()
        {
            
        }


        public bool IsImage(string fileName)
        {
            fileName = fileName.ToLower();
            var listExtensionImage = new[] { ".bmp", ".gif", ".jpg", ".png", ".psd", ".psp", ".thm", ".tif", ".yuv", ".jpeg", ".ico" };

            return listExtensionImage.Any(extension => fileName.IndexOf(extension, StringComparison.Ordinal) != -1);
        }

        [TestMethod()]
        public void TestCheckIsUrl()
        {
            IWebClientService webClientService = new WebClientService();
            var s = webClientService.DownloadFileAsync(
                 "http://htmlcoder.me/preview/idea/v.1.6/html/fonts/fontello/css/fontello.css", "sss" ,"http://htmlcoder.me/preview/idea/v.1.6/html/index.html");

            s.Wait();
        } 
    }

    
}
