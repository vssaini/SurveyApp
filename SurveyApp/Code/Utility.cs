/****************************** Module Utility ******************************\
* Module Name:  Utility.cs
* Project:      Survey App
* Date:         28 August, 2013
* Copyright (c) Vikram Singh Saini
* 
* Provide functions for creating and reading files (.xml,.sql & .txt) and error handling.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SurveyApp.Code
{
    /// <summary>
    /// Custom class for creating and reading xml file, implementing error handling and logging, for checking
    /// if form is already opened or not. For creating script file, saving query to script and running script to
    /// save record in database.
    /// </summary>
    class Utility
    {
        private static XDocument _xDoc;

        // Directory and file path
        private const string DirPath = @"C:\SurveyApp\";
        private const string ScriptFilePath = DirPath + @"Script.sql";
        private const string ErrorFilePath = DirPath + @"Error.txt";

        public static string ConfigFilePath { get; set; }
        public static string ConnectionString { get; set; }
        public static string UserConnectionString { get; set; }

        #region Create config file

        public static void CreateConfigFile()
        {
            ConfigFilePath = DirPath + @"Config.xml";
            try
            {
                if (!Directory.Exists(DirPath))
                {
                    Directory.CreateDirectory(DirPath);
                }

                if (File.Exists(ConfigFilePath)) return;
                File.Create(ConfigFilePath).Close();
                FileCreator(ConfigFilePath, "SurveyApp.Resources.Config.xml");
                WriteEntryToXml();
            }
            catch (Exception exc)
            {
                WriteError(exc);
            }
        }

        /// <summary>
        /// Create config.xml file at the path specified
        /// </summary>
        public static void FileCreator(string filePath, string resName)
        {
            try
            {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName))
                {
                    using (var fileStream = File.OpenWrite(filePath))
                    {
                        if (stream == null) return;
                        var bytes = ReadToEnd(stream);
                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (Exception exc)
            {
                WriteError(exc);
            }
        }

        private static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead != readBuffer.Length) continue;
                    int nextByte = stream.ReadByte();
                    if (nextByte == -1) continue;
                    var temp = new byte[readBuffer.Length * 2];
                    Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                    Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                    readBuffer = temp;
                    totalBytesRead++;
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        #endregion

        #region Read xml file

        public static void ReadXmlFile()
        {
            var elements = new[] { "connectionString", "scriptDirectory" };
            var xDoc = XElement.Load(ConfigFilePath);

            foreach (var elementName in elements)
            {
                var descendant = xDoc.Descendants(elementName);
                var entry = from element in descendant
                            select element;

                if (elementName != "connectionString") continue;
                foreach (var item in entry)
                {
                    ConnectionString = (string)item.Attribute("value");
                }
            }
        }

        #endregion

        #region Write to xml file

        public static void WriteEntryToXml()
        {
            if (_xDoc == null)
                _xDoc = XDocument.Load(ConfigFilePath);

            var rootElement = _xDoc.Element("config");
            if (rootElement == null) return;

            var xEle = new XElement("connectionString");
            xEle.SetAttributeValue("value", UserConnectionString);
            rootElement.Add(xEle);

            _xDoc.Save(ConfigFilePath);
        }


        #endregion

        #region Form exists already
        /// <summary>
        /// Checks whether the given Form already exists. If exists show it.
        /// </summary>
        /// <param name="formName">Parameter-string that contains form name.</param>
        /// <returns>True, when form doesn't exists otherwise false.</returns>
        public static bool FormExists(string formName)
        {
            var form = Application.OpenForms[formName];
            if (form != null)
            {
                form.Show();
                form.Activate();
                form.BringToFront();
                return false;
            }
            return true;
        }
        #endregion

        #region Script File Tasks : Create, Save & Run

        /// <summary>
        /// Create script file for saving records that doesn't get saved.
        /// </summary>
        public static void CreateScriptFile()
        {
            if (String.IsNullOrEmpty(DirPath)) return;

            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);
            }

            if (File.Exists(ScriptFilePath)) return;
            File.Create(ScriptFilePath).Close();
        }

        /// <summary>
        /// Save query to Script.sql file for later execution.
        /// </summary>
        /// <param name="query"></param>
        public static void SaveToScriptFile(string query)
        {
            using (var file = new StreamWriter(ScriptFilePath, true))
            {
                file.WriteLine(query);
            }
        }

        /// <summary>
        /// Run script file for saving records to database.
        /// </summary>
        public static void RunScriptFile()
        {
            DataAccess.ExecuteQuery(ScriptFilePath);
        }

        /// <summary>
        /// Clear entire script file
        /// </summary>
        public static void ClearScriptFile()
        {
            File.Create(ScriptFilePath).Close();
        }

        #endregion

        #region Error handling

        public static void WriteError(Exception ex)
        {
            // Create file if it doesn't exists
            if (!File.Exists(ErrorFilePath))
            {
                File.Create(ErrorFilePath).Close();
            }

            // Write to created file or append to it
            using (StreamWriter w = File.AppendText(ErrorFilePath))
            {
                w.WriteLine("Error logged on {0}", DateTime.Now);
                w.WriteLine("Message: " + ex.Message);
                w.WriteLine("Source: " + ex.Source);
                w.WriteLine("Target: " + ex.TargetSite.Name);
                w.WriteLine("Stack trace: " + ex.StackTrace);
                w.WriteLine(
                    "=============================================================================================================================\r\n");
                w.Flush();
            }
        }

        public static void WriteError(UnhandledExceptionEventArgs ex)
        {
            // Create file if it doesn't exists
            if (!File.Exists(ErrorFilePath))
            {
                File.Create(ErrorFilePath).Close();
            }

            // Write to created file or append to it
            using (StreamWriter w = File.AppendText(ErrorFilePath))
            {
                w.WriteLine("Error Category - Program");
                w.WriteLine("Error logged on {0}", DateTime.Now);
                w.WriteLine("Unhandled exception details: " + ex.ExceptionObject);
                w.WriteLine(
                    "=============================================================================================================================\r\n");
                w.Flush();
            }
        }

        public static void WriteError(ThreadExceptionEventArgs ex)
        {
            // Create file if it doesn't exists
            if (!File.Exists(ErrorFilePath))
            {
                File.Create(ErrorFilePath).Close();
            }

            // Write to created file or append to it
            using (StreamWriter w = File.AppendText(ErrorFilePath))
            {
                w.WriteLine("Error Category - Thread");
                w.WriteLine("Error logged on {0}", DateTime.Now);
                w.WriteLine("Message: " + ex.Exception.Message);
                w.WriteLine("Source: " + ex.Exception.Source);
                w.WriteLine("Target: " + ex.Exception.TargetSite.Name);
                w.WriteLine("Stack trace: " + ex.Exception.StackTrace);
                w.WriteLine(
                    "=============================================================================================================================\r\n");
                w.Flush();
            }
        }

        #endregion

    }
}



