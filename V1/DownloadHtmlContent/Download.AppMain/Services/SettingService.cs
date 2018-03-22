using System;
using System.Diagnostics;
using System.IO;
using Download.AppMain.Models;
using Download.Models;
using Newtonsoft.Json;

namespace Download.AppMain.Services
{
    public class SettingService
    {
        public SettingService()
        {
            Trace.Listeners.Add(new TextWriterTraceListener("SettingLog.txt"));
            Trace.AutoFlush = true;
        }

        public SettingModel Init(string filePath)
        {
            SettingModel setting = LoadSetting(filePath);
            if (setting == null)
            {
                var settingModel = new SettingModel
                {
                    CssFolder = FolderType.Css.GetCustomAttributeDescription(),
                    FontFolder = FolderType.Fonts.GetCustomAttributeDescription(),
                    ImageFolder = FolderType.Images.GetCustomAttributeDescription(),
                    ScriptFolder = FolderType.Scripts.GetCustomAttributeDescription()
                };

                Trace.TraceError($"{DateTime.Now} - Method: {nameof(Init)}");
                Trace.TraceError($"{DateTime.Now} - Load default setting");

                SaveSetting(settingModel, filePath);

                return settingModel;
            }
            return setting;
        }

        public SettingModel LoadSetting(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    Trace.TraceError($"{DateTime.Now} - Method: {nameof(LoadSetting)} - {nameof(filePath)}: is Null or Empty");
                    return null;
                }

                FileInfo fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists)
                {
                    Trace.TraceError($"{DateTime.Now} - Method: {nameof(LoadSetting)} - {nameof(filePath)}: do not found");
                    return null;
                }

                string fileContents;

                using (FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                using (StreamReader stringReader = new StreamReader(stream))
                {
                    fileContents = stringReader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<SettingModel>(fileContents);
            }
            catch (Exception e)
            {

                Trace.TraceError("{0} - LoadSetting have been to throw exception", DateTime.Now);
                Trace.WriteLine(e);
                return null;
            }
           
        }

        public bool SaveSetting(SettingModel settingModel, string filePath)
        {
            try
            {
                if (settingModel == null)
                {
                    Trace.TraceError($"{DateTime.Now} - Method: {nameof(SaveSetting)} - {nameof(settingModel)}: is Null");
                    return false;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    Trace.TraceError($"{DateTime.Now} - Method: {nameof(SaveSetting)} - {nameof(filePath)}: is Null or Empty");
                    return false;
                }

                string jsonString = JsonConvert.SerializeObject(settingModel);

                FileInfo fileInfo = new FileInfo(filePath);
                using (FileStream stream = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.Write(jsonString);
                }
                return true;
            }
            catch (Exception e)
            {
                Trace.TraceError($"{DateTime.Now} - Method: {nameof(SaveSetting)} - have been to throw exception");
                Trace.WriteLine(e);
                return false;
            }
        }

    }
}
