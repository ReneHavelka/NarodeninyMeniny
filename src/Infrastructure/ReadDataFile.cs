using ApplicationL.Common.Interfaces;
using System.Text;

namespace Infrastructure
{
	public class ReadDataFile : IReadDataFile
	{
		private string dataFileName;

		public ReadDataFile()
		{
			var accessDataFile = new AccessDataFile();
			dataFileName = accessDataFile.GetFileName();
		}

		public string ReadData()
		{
			string fileData = String.Empty;
			fileData = File.ReadAllText(dataFileName, Encoding.UTF8);

			return fileData;
		}
	}
}
