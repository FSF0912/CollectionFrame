/*using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace FSF.Encryption{
    public class JsonSaver{
        public string FileName = "Default";
        public string FileFolder = "";
        /// <summary>
        /// Format:
        /// {FileFolder}/{FileName}.{Suffix}
        /// 
        /// <para>example:</para>
        /// <para>AnyFolder/AnyFile.AnySuffix</para>
        /// <para>param"FileName" = AnyFile,</para>
        /// <para>param"FileFolder" = AnyFolder,</para>
        /// param"Suffix" = AnySuffix.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="FileFolder"></param>
        /// <param name="Suffix"></param>
        public JsonSaver(string FileName, string FileFolder, string Suffix){
            this.FileName = $"{FileFolder}/{FileName}.{Suffix}";
            this.FileFolder = $"{FileFolder}/{FileName}";
        }

        public virtual void Save(object target, EncryptMode encryptMode = EncryptMode.None){
            StreamWriter writer;
            if (!File.Exists(FileFolder))
            {
                Directory.CreateDirectory($"{Application.persistentDataPath}/{FileFolder}");
            }
            string filePath = FileName;
            if (!File.Exists(filePath))
            {
                writer = File.CreateText(filePath);
            }
            else
            {
                File.Delete(filePath);
                writer = File.CreateText(filePath);
            }
            writer.Flush();
            writer.Dispose();
            writer.Close();
            string result = JsonConvert.SerializeObject(target);
            switch(encryptMode){
                case EncryptMode.XOR:
                result = Confusor.EncryptByXOR(result);
                break;
                case EncryptMode.FlipChar:

                break;
                default:break;
            }
            File.WriteAllText(filePath, result);
            
        }

        public virtual T Load<T>(EncryptMode encryptMode = EncryptMode.None){
            if (!File.Exists(FileFolder))
            {
                throw new System.NullReferenceException("File not found.");
            }
            string filePath = FileName;
            string text = new StreamReader(filePath).ReadToEnd();
            switch(encryptMode){
                case EncryptMode.XOR:
                text = Confusor.EncryptByXOR(text);
                break;
                case EncryptMode.FlipChar:

                break;
                default:break;
            }
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}*/