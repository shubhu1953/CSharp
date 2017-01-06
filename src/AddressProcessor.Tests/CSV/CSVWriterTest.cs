using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressProcessing.CSV;
using Moq;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    public class CSVWriterTest
    {
        private ICSVWriter _csvWriter;
        private const string Filename = "C:\\test\\testfile.txt";

        [SetUp]
        public void SetUp()
        {
            _csvWriter = new CSVWriter();
        }

        [Test]
        public void ShouldOpenFileAndReadStream()
        {
            var mockstream = new Mock<Stream>();
            mockstream.Setup(x => x.CanRead).Returns(true);
            var streamreader = new StreamReader(mockstream.Object);

            _csvWriter.Open(Filename);

            var stream = _csvWriter.GetWriter();

            Assert.That(stream, Is.EqualTo(streamreader));
            _csvWriter.Close();
        }

        [Test]
        public void ShouldWriteToTextWriter()
        {
            var textWriter = new Mock<TextWriter>();
            _csvWriter.SetWriter(textWriter.Object);


            _csvWriter.Open(Filename);
            _csvWriter.Write(new String[] {"newColumn1", "newColumn2"});

            textWriter.Verify(x => x.WriteLine("newColumn1\tnewColumn2"));
        }

        [Test]
        public void ShouldCloseWriterOnClose()
        {
            _csvWriter.Open(Filename);
            var textWriter = new Mock<TextWriter>();
            _csvWriter.SetWriter(textWriter.Object);
            
            _csvWriter.Close();
            
            textWriter.Verify(x => x.Close());
        }
    }
}
