using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressProcessing.CSV;
using Moq;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        private const string Filename = "C:\\test\\testfile.txt";
        private CSVReaderWriter _csvReaderWriter;
        private Mock<ICSVWriter> _csvWriter;
        private Mock<ICSVReader> _csvReader;

        [SetUp]
        public void SetUp()
        {
            _csvWriter = new Mock<ICSVWriter>();
            _csvReader = new Mock<ICSVReader>();
            _csvReaderWriter = new CSVReaderWriter(_csvReader.Object, _csvWriter.Object);
        }

        [Test]
        public void ShouldOpenCSVReader()
        {
            _csvReaderWriter.Open(Filename, CSVReaderWriter.Mode.Read);
            _csvReader.Verify(x => x.Open(Filename));
        }


        [Test]
        public void ShouldOpenCSVWriter()
        {
            _csvReaderWriter.Open(Filename, CSVReaderWriter.Mode.Write);
            _csvWriter.Verify(x => x.Open(Filename));
        }

        [Test]
        public void ShouldThrowUnknowModeException()
        {
            var unknownFileMode = (CSVReaderWriter.Mode)8;
            
            var exception = Assert.Throws<Exception>(() => _csvReaderWriter.Open(Filename, unknownFileMode));
            Assert.That(exception.Message, Is.EqualTo("Unknown file mode for C:\\test\\testfile.txt"));
        }

        [Test]
        public void ShouldCloseReaderWriter()
        {
            _csvReaderWriter.Close();
            
            _csvWriter.Verify(x => x.Close());
            _csvReader.Verify(x => x.Close());
        }

        [Test]
        public void ShouldReadFromCSVReader()
        {
            string column1;
            string column2;
            
            _csvReader.Setup(x => x.Read(out column1, out column2)).Returns(true);
            var result = _csvReaderWriter.Read(out column1, out column2);
            
            _csvReader.Verify(x => x.Read(out column1, out column2));
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_write_with_the_CSVWriter()
        {
            string[] columns = { };

            _csvReaderWriter.Write(columns);
            
            _csvWriter.Verify(x => x.Write(columns));
        }

    }
}
