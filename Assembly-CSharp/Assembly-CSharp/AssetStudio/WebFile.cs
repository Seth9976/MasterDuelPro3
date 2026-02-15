using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AssetStudio
{
	// Token: 0x02000182 RID: 386
	public class WebFile
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public WebFile(EndianBinaryReader reader)
		{
			reader.Endian = EndianType.LittleEndian;
			reader.ReadStringToNull(32767);
			int headLength = reader.ReadInt32();
			List<WebFile.WebData> dataList = new List<WebFile.WebData>();
			while (reader.BaseStream.Position < (long)headLength)
			{
				WebFile.WebData data = new WebFile.WebData();
				data.dataOffset = reader.ReadInt32();
				data.dataLength = reader.ReadInt32();
				int pathLength = reader.ReadInt32();
				data.path = Encoding.UTF8.GetString(reader.ReadBytes(pathLength));
				dataList.Add(data);
			}
			this.fileList = new StreamFile[dataList.Count];
			for (int i = 0; i < dataList.Count; i++)
			{
				WebFile.WebData data2 = dataList[i];
				StreamFile file = new StreamFile();
				file.path = data2.path;
				file.fileName = Path.GetFileName(data2.path);
				reader.BaseStream.Position = (long)data2.dataOffset;
				file.stream = new MemoryStream(reader.ReadBytes(data2.dataLength));
				this.fileList[i] = file;
			}
		}

		// Token: 0x04000A21 RID: 2593
		public StreamFile[] fileList;

		// Token: 0x02000183 RID: 387
		private class WebData
		{
			// Token: 0x04000A22 RID: 2594
			public int dataOffset;

			// Token: 0x04000A23 RID: 2595
			public int dataLength;

			// Token: 0x04000A24 RID: 2596
			public string path;
		}
	}
}
