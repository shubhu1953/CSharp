using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressProcessing.CSV
{
    public interface ICSVReader
    {
        void Open(string fileName);
        bool Read(out string column1, out string column2);
        void Close();
        TextReader GetStream();
    }

    public class CSVReader : ICSVReader
    {
        protected TextReader _textReader;

        public void Open(string fileName)
        {
            try
            {
                _textReader = File.OpenText(fileName);
            }
            catch(Exception ex)
            {
                throw new Exception("Unknown file mode for " + fileName + "Excception:" + ex.StackTrace);
            }
        }

        public bool Read(out string column1, out string column2)
        {
            string line = _textReader.ReadLine();

            if (line == null)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            var columns = line.Split('\t');
            if (columns.Length == 1)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            column1 = columns[0];
            column2 = columns[1];

            return true;
        }

        public void Close()
        {
            _textReader?.Close();
        }
       
        public TextReader GetStream()
        {
            return _textReader;
        }
    }
}
