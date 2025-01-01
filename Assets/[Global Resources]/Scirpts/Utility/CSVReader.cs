using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FSF.CollectionFrame
{
    public static class CSVReader
    {
        public static List<string[]> ReadCSV(string filePath, Encoding encoding)
        {
            List<string[]> tempData = new List<string[]>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath, encoding))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] row = line.Split(',');
                        tempData.Add(row);
                    }
                }
            }
            catch (System.Exception){}

            return tempData;
        }
    }
}