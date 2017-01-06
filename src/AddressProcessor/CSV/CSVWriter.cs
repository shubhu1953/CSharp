using System.IO;

namespace AddressProcessing.CSV
{
    public interface ICSVWriter
    {
        void Open(string fileName);
        void Write(string[] columns);
        void Close();
        TextWriter GetWriter();
        void SetWriter(TextWriter textWriter);
    }

    public class CSVWriter : ICSVWriter
    {
        private TextWriter _textWriter;
        public void Open(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            _textWriter = fileInfo.CreateText();
        }

        public void Write(string[] columns)
        {
            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += "\t";
                }
            }

            _textWriter.WriteLine(outPut);
        }

        public void Close()
        {
            _textWriter?.Close();
        }

        public TextWriter GetWriter()
        {
            return _textWriter;
        }

        public void SetWriter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
    }
}
