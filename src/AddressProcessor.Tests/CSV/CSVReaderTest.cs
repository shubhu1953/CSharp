using AddressProcessing.CSV;
using Moq;
using NUnit.Framework;
using System.IO;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVReaderTest
    {
        private ICSVReader _csvReader;
        private const string Filename = "C:\\test\\testfile.txt";

        [SetUp]
        public void SetUp()
        {
            _csvReader = new CSVReader();
        }

        [Test]
        public void ShouldOpenFileAndReadStream()
        {
            var mockstream = new Mock<Stream>();
            mockstream.Setup(x => x.CanRead).Returns(true);
            var streamreader = new StreamReader(mockstream.Object);
            
            _csvReader.Open(Filename);

            var stream = _csvReader.GetStream();

            Assert.That(stream, Is.EqualTo(streamreader));
        }

        [Test]
        public void ShouldReadColumnsFromTextFile() 
        {
            var textReader = new Mock<TextReader>();
            textReader.Setup(x => x.ReadLine()).Returns("new1\tnew2");

            _csvReader.Open(Filename);

            string newcolumn1;
            string newcolumn2;
            var columns = _csvReader.Read(out newcolumn1, out newcolumn2);

            Assert.That(columns, Is.True);
            Assert.That(newcolumn1, Is.EqualTo("new1"));
            Assert.That(newcolumn2, Is.EqualTo("new2"));
        }


        [Test]
        public void ShouldNotReadNoColumnsTextFile()
        {
            _csvReader.Open(Filename);

            string newcolumn1;
            string newcolumn2;
            var columns = _csvReader.Read(out newcolumn1, out newcolumn2);

            Assert.That(columns, Is.False);
            Assert.That(newcolumn1, Is.Null);
            Assert.That(newcolumn2, Is.Null);
        }
    }
}
