using ApplicationL.Common.Interfaces;
using System.Text;

namespace Infrastructure
{
    public class WriteDataFile : IWriteDataFile
    {
        private string dataFileName;

        public WriteDataFile()
        {
            var accessDataFile = new AccessDataFile();
            dataFileName = accessDataFile.GetFileName();
        }

        public void WriteData(string dataString)
        {
            File.WriteAllText(dataFileName, dataString, Encoding.UTF8);
        }
    }
}
