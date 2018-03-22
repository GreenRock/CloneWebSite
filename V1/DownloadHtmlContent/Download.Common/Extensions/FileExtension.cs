using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Download.Common.Resources;
using Download.Models.ImageModels;

namespace Download.Common.Extensions
{
    public  class FileExtension
    {
        private readonly DirectoryExtension _directoryExtension;
        private readonly ImageExtension _imageExtension;
        public FileExtension(DirectoryExtension directoryExtension, ImageExtension imageExtension)
        {
            _directoryExtension = directoryExtension;
            _imageExtension = imageExtension;
        }
        public bool BlackLink(string fileName)
        {
            var listExtensionImage = new[] { ".xml", ".php" };

            return listExtensionImage.Any(extension => fileName.IndexOf(extension, StringComparison.Ordinal) != -1);
        }
        public  bool IsImageBase64(string input)
        {
            var regex = new Regex(RegexResource.ImageBase64, RegexOptions.Compiled);
            return regex.IsMatch(input);
        }
        public  bool IsImage(string fileName)
        {
            fileName = fileName.ToLower();
            var listExtensionImage = new[] { ".bmp", ".gif", ".jpg", ".png", ".psd", ".psp", ".thm", ".tif", ".yuv", ".jpeg", ".ico" };

            return listExtensionImage.Any(extension => fileName.IndexOf(extension, StringComparison.Ordinal) != -1);
        }

        public  bool IsFont(string fileName)
        {
            return fileName.IndexOf(".eot", StringComparison.Ordinal) != -1 ||
                   fileName.IndexOf(".eot2", StringComparison.Ordinal) != -1 ||
                   fileName.IndexOf(".woff", StringComparison.Ordinal) != -1 ||
                   fileName.IndexOf(".woff2", StringComparison.Ordinal) != -1 ||
                   fileName.IndexOf(".ttf", StringComparison.Ordinal) != -1 ||
                   fileName.IndexOf(".svg", StringComparison.Ordinal) != -1;
        }

        public  async Task WriteAsync(string path, string str)
        {
            using (var outfile = new StreamWriter(path, true, Encoding.UTF8))
            {
                await outfile.WriteAsync(str);
            }
        }

        public  void WriteAllText(string path, string str)
        {
            try
            {
                File.WriteAllText(path, str);
            }
            catch (Exception e)
            {
              
                Trace.WriteLine($"{str} {path} {e}");

            }
        }

        public  async Task<string> ReadTextAsync(string path)
        {
            using (var sourceStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                var sb = new StringBuilder();

                var buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var text = Encoding.UTF8.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }

        public  bool Exists(string file)
        {
            var fileInfor = new FileInfo(file);
            return fileInfor.Exists;
        }

        public  string IsFileExitsAndRename(string folderLink, string fileName)
        {
            var indexFolderInLink = fileName.IndexOf(folderLink, StringComparison.Ordinal);

            if (indexFolderInLink == -1)
                fileName = Path.Combine(folderLink, fileName);

            if (!Exists(fileName))
                return fileName;

            var filePaths = _directoryExtension.GetFiles(folderLink);

            var countFile = filePaths.Count(c => Path.GetFileName(c) == fileName);

            var indexPoint = fileName.IndexOf(".", StringComparison.Ordinal);

            return fileName.Insert(indexPoint - 1, (countFile + 1).ToString(CultureInfo.InvariantCulture));
        }
        public ImageBase64Model ImageBase64(string input)
        {
            var regex = new Regex(RegexResource.ImageBase64, RegexOptions.Compiled);
            var match = regex.Match(input);
            return new ImageBase64Model
            {
                ImageSource = match.Groups["data"].Value,
                ImageType = match.Groups["mime"].Value
            };
        }
        public string SaveImageBase64(string imageString, string fileDirectory, out string imageExtension)
        {
            var imageModel = ImageBase64(imageString);

            var imageObject = _imageExtension.Base64ToImage(imageModel.ImageSource);

            imageExtension = string.Empty;

            if (imageObject == null) return string.Empty;

            var imageType = imageModel.ImageType.Split('/');

            imageExtension = ".png";

            if (imageType.Length == 2)
            {
                imageExtension = string.Format(".{0}", imageType[1]);
            }

            var filename = fileDirectory + imageExtension;

            imageObject.Save(filename);

            return filename;
        }
        public  string Strip(string text, string sChar = "-")
        {
            return Regex.Replace(text, @"<(.|\n)*?>|\s|\W", sChar);
        }

        public string GetUrlSeo(string str, string sChar = "-")
        {
            str = str.ToLower();

            str = Strip(RemoveSignString(str));

            var strArray = str.Split(' ');

            if (!strArray.Any()) return str;

            var strList = strArray.Where(c => !string.IsNullOrEmpty(c)).ToList();

            str = string.Join(sChar, strList);

            return  str;
        }

        public string RemoveSignString( string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (var i = 1; i < VietnameseSigns.Length; i++)
            {
                for (var j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        #region Private Method
        private string ReadThousand(int baso)
        {

            var tram = (baso / 100);
            var chuc = ((baso % 100) / 10);
            var donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0))
                return "";

            var ketQua = new StringBuilder();

            if (tram != 0)
            {
                ketQua.Append(ChuSo[tram] + " trăm");
                if ((chuc == 0) && (donvi != 0))
                    ketQua.Append(" linh");
            }
            if ((chuc != 0) && (chuc != 1))
            {
                ketQua.Append(ChuSo[chuc] + " mươi");
                if ((chuc == 0) && (donvi != 0))
                    ketQua.Append(ketQua + " linh");
            }
            if (chuc == 1)
                ketQua.Append(" mười");
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        ketQua.Append(" mốt");
                    }
                    else
                    {
                        ketQua.Append(ChuSo[donvi]);
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        ketQua.Append(ChuSo[donvi]);
                    }
                    else
                    {
                        ketQua.Append(" lăm");
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        ketQua.Append(ChuSo[donvi]);
                    }
                    break;
            }
            return ketQua.ToString();
        }

        private readonly string[] VietnameseSigns =
                                            {
                                                "aAeEoOuUiIdDyY",
                                                "áàạảãâấầậẩẫăắằặẳẵ",
                                                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                                                "éèẹẻẽêếềệểễ",
                                                "ÉÈẸẺẼÊẾỀỆỂỄ",
                                                "óòọỏõôốồộổỗơớờợởỡ",
                                                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                                                "úùụủũưứừựửữ",
                                                "ÚÙỤỦŨƯỨỪỰỬỮ",
                                                "íìịỉĩ",
                                                "ÍÌỊỈĨ",
                                                "đ",
                                                "Đ",
                                                "ýỳỵỷỹ",
                                                "ÝỲỴỶỸ"
                                            };
        private static readonly string[] ChuSo = { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bẩy", " tám", " chín" };
        private static readonly string[] Tien = { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
        #endregion

       
    }
}
