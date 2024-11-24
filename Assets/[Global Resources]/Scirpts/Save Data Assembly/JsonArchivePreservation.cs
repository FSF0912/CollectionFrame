using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace SaveDataAssembly
{
    public sealed class JsonArchivePreservation<T>
    {
        public static string FileName;
        private static string Extension = "json";
        public static void FileInformation(string UsedFileName, string FileExtension = "json")
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
        /// <param name="CrownSaver"></param>
        /// <param name="DiamondSaver"></param>
        /// <param name="PercentageSaver"></param>
        public static void WriteLevelData(string LevelNameKey, T DataStruct, UnityAction WriteAction)
        {
            if (WriteAction != null)
            {
                WriteAction?.Invoke();
                WriteJsonData(DataStruct);
            }
        }
        public static void ReadLevelData(string LevelNameKey,UnityAction ReadAction)
        {
            if (ReadAction != null)
            {
                ReadAction?.Invoke();
            }
        }

        /// <summary>
        /// Function
        /// </summary>
        public static void Initialzation(bool InitialCondition,T DataStruct,UnityAction ErrorPathAction,UnityAction FileHasAction,UnityAction UpdateAction)
        {
            if (InitialCondition)
            {
                CheckDirectory: if (IsDirectoryPath)
                {
                    CheckFile: if (!IsFilePath)
                    {
                        //Info & StreamWriter
                        StreamWriter MainStructure;
                        FileInfo Main_FileInfo = new FileInfo(FilePath);

                        //Create
                        MainStructure = Main_FileInfo.CreateText();
                        MainStructure.Close();
                        MainStructure.Dispose();

                        //Disconnected
                        string NewData_string = JsonUtility.ToJson(DataStruct);
                        T NewData = JsonUtility.FromJson<T>(NewData_string);

                        //Write
                        File.WriteAllText(FilePath, JsonUtility.ToJson(NewData), Encoding.UTF8);
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
        /// <param name="GameData"></param>
        public static void WriteJsonData(T DataStruct)
        {
            if (IsFilePath && DataStruct != null)
            {
                string EncryptContents = JsonUtility.ToJson(DataStruct);
                File.WriteAllText(FilePath, EncryptContents, Encoding.UTF8);
            }
        }
        public static T ReadJsonData
        {
            get
            {
                if (IsFilePath)
                {
                    string JsonContent = File.ReadAllText(FilePath);
                    return JsonUtility.FromJson<T>(JsonContent);
                }
                else
                    return default(T);
            }
        }
    }
}