using System.IO;
using UnityEngine;
using UnityEngine.Events;
using System.Xml.Serialization;

namespace SaveDataAssembly
{
    public class XMLArchivePreservation<T>
    {
        public static string FileName;
        private static string Extension = "xml";
        public static void FileInformation(string UsedFileName,string FileExtension = "xml")
        {
            FileName = UsedFileName;
            Extension = FileExtension;
        }

        /// <summary>
        /// Read Base Value
        /// </summary>
        public static string FilePath => $"{Application.persistentDataPath}/{FileName}.{Extension}";
        public static string DirectoryPath => Application.persistentDataPath;
        public static bool IsFilePath => File.Exists(FilePath);
        public static bool IsDirectoryPath => Directory.Exists(DirectoryPath);

        /// <summary>
        /// Level Data
        /// </summary>
        /// <param name="LevelNameKey"></param>
        /// <param name="DataStruct"></param>
        /// <param name="WriteAction"></param>
        public static void WriteLevelData(string LevelNameKey, T DataStruct, UnityAction WriteAction)
        {
            if (WriteAction != null)
            {
                WriteAction?.Invoke();
                WriteXMLData(DataStruct);
            }
        }
        public static void ReadLevelData(string LevelNameKey, UnityAction ReadAction)
        {
            if (ReadAction != null)
            {
                ReadAction?.Invoke();
            }
        }

        /// <summary>
        /// Initialzation
        /// </summary>
        /// <param name="InitialCondition"></param>
        /// <param name="DataStruct"></param>
        /// <param name="ErrorPathAction"></param>
        /// <param name="FileHasAction"></param>
        /// <param name="UpdateAction"></param>
        public static void Initialzation(bool InitialCondition, T DataStruct, UnityAction ErrorPathAction, UnityAction FileHasAction, UnityAction UpdateAction)
        {
            if (InitialCondition)
            {
                CheckDirectory: if (IsDirectoryPath)
                {
                    CheckFile: if (!IsFilePath)
                    {
                        WriteXMLData(DataStruct);
                        goto CheckFile;
                    }
                    else
                    {
                        if (FileHasAction != null)
                        {
                            FileHasAction?.Invoke();
                            UpdateAction?.Invoke();
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(DirectoryPath);//Create Directory
                    goto CheckDirectory;
                }
            }
            else
            {
                if (ErrorPathAction != null)
                {
                    ErrorPathAction?.Invoke();
                }
            }
        }

        /// <summary>
        /// Write + Read MainFunction
        /// </summary>
        /// <param name="DataStruct"></param>
        public static void WriteXMLData(T DataStruct)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(File.Open(FilePath,FileMode.Create), DataStruct);
        }
        public static T ReadXMLData
        {
            get
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(File.OpenRead(FilePath));
            }
        }
    }
}