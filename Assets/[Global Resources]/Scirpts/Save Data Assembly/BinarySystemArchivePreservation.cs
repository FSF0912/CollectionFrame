using System.IO;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveDataAssembly
{
    public sealed class BinarySystemArchivePreservation<T>
    {
        public static string FileName;
        private static string Extension = "bin";
        public static void FileInformation(string UsedFileName, string FileExtension = "bin")
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
                CheckDirectory: if(IsDirectoryPath)
                {
                    CheckFile: if(!IsFilePath)
                    {
                        WriteBinarySystemData(DataStruct);
                        goto CheckFile;
                    }
                    else
                    {
                        FileHasAction?.Invoke();
                        UpdateAction?.Invoke();
                    }
                }
                else
                {
                    Directory.CreateDirectory(DirectoryPath);
                    goto CheckDirectory;
                }
            }
            else
            {
                ErrorPathAction?.Invoke();
            }
        }

        /// <summary>
        /// Level Data
        /// </summary>
        /// <param name="LevelNameKey"></param>
        /// <param name="WriteAction"></param>
        public static void WriteLevelData(string LevelNameKey, T DataStruct, UnityAction WriteAction)
        {
            if (WriteAction != null)
            {
                WriteAction?.Invoke();
                WriteBinarySystemData(DataStruct);
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
        /// Write_Read MainFunction
        /// </summary>
        /// <param name="DataStruct"></param>
        public static void WriteBinarySystemData(T DataStruct)
        {
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Create)){
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, DataStruct);
            }
        }
        public static T ReadeBinarySystemData
        {
            get
            {
                if (IsFilePath)
                {
                    using (FileStream fileStream = new FileStream(FilePath, FileMode.Open)){
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (T)formatter.Deserialize(fileStream);
                    }
                }
                else
                    return default;
            }
        }
    }
}