using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Xunit.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Download.Testing
{
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
    }

    
}
