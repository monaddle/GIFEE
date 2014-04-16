using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Multisian
{
    [Serializable()]
    public class SeriesFile
    {
        StreamReader sr;
        public int cols;
        public int len;
        public char delimiter = ',';
        public DateTime LastModified;
        public string filePath;
        public SeriesFile(string FilePath)
        {
            this.filePath = FilePath;
            sr = new StreamReader(filePath);
            string[] line;

            try
            {
                line = sr.ReadLine().Split(delimiter);
            }
            catch (Exception e)
            {
                throw e;
            }

            cols = line.Length;
            len = 1;
            while (sr.ReadLine() != null)
            {
                len++;
            }

            sr.BaseStream.Position = 0;
            sr.DiscardBufferedData();
            FileInfo fi = new FileInfo(filePath);
            LastModified = fi.LastWriteTime;
        }
    }
}
