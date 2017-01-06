using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        private readonly ICSVReader _csvReader;
        private readonly ICSVWriter _csvWriter;

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public CSVReaderWriter(ICSVReader csvReader, ICSVWriter csvWriter)
        {
            _csvReader = csvReader;
            _csvWriter = csvWriter;
        }       

        public void Open(string fileName, Mode mode)
        {
            switch (mode)
            {
                case Mode.Read:
                    _csvReader.Open(fileName);
                    break;
                case Mode.Write:
                    _csvWriter.Open(fileName);
                    break;
                default:
                    throw new Exception("Unknown file mode for " + fileName);
            }
        }

        public void Write(params string[] columns)
        {
            _csvWriter.Write(columns);
        }

        //This method is not required
        public bool Read(string column1, string column2)
        {
            return true;
        }

        public bool Read(out string column1, out string column2)
        {
            return _csvReader.Read(out column1, out column2);
        }

        public void Close()
        {
            _csvReader.Close();
            _csvWriter.Close();
        }
    }
}
