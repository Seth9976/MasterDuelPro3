using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000177 RID: 375
	public class ResourceReader
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00018573 File Offset: 0x00016773
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001857C File Offset: 0x0001677C
		public ResourceReader(string path, SerializedFile assetsFile, long offset, long size)
		{
			this.needSearch = true;
			this.path = path;
			this.assetsFile = assetsFile;
			this.offset = offset;
			this.size = size;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000185A8 File Offset: 0x000167A8
		public ResourceReader(BinaryReader reader, long offset, long size)
		{
			this.reader = reader;
			this.offset = offset;
			this.size = size;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000185C8 File Offset: 0x000167C8
		private BinaryReader GetReader()
		{
			if (!this.needSearch)
			{
				return this.reader;
			}
			string resourceFileName = Path.GetFileName(this.path);
			if (this.assetsFile.assetsManager.resourceFileReaders.TryGetValue(resourceFileName, out this.reader))
			{
				this.needSearch = false;
				return this.reader;
			}
			string assetsFileDirectory = Path.GetDirectoryName(this.assetsFile.fullName);
			string resourceFilePath = Path.Combine(assetsFileDirectory, resourceFileName);
			if (!File.Exists(resourceFilePath))
			{
				string[] findFiles = Directory.GetFiles(assetsFileDirectory, resourceFileName, SearchOption.AllDirectories);
				if (findFiles.Length != 0)
				{
					resourceFilePath = findFiles[0];
				}
			}
			if (File.Exists(resourceFilePath))
			{
				this.needSearch = false;
				this.reader = new BinaryReader(File.OpenRead(resourceFilePath));
				this.assetsFile.assetsManager.resourceFileReaders.Add(resourceFileName, this.reader);
				return this.reader;
			}
			throw new FileNotFoundException("Can't find the resource file " + resourceFileName);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000186A4 File Offset: 0x000168A4
		public byte[] GetData()
		{
			BinaryReader binaryReader = this.GetReader();
			binaryReader.BaseStream.Position = this.offset;
			return binaryReader.ReadBytes((int)this.size);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000186C9 File Offset: 0x000168C9
		public void GetData(byte[] buff)
		{
			BinaryReader binaryReader = this.GetReader();
			binaryReader.BaseStream.Position = this.offset;
			binaryReader.Read(buff, 0, (int)this.size);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000186F4 File Offset: 0x000168F4
		public void WriteData(string path)
		{
			BinaryReader binaryReader = this.GetReader();
			binaryReader.BaseStream.Position = this.offset;
			using (FileStream writer = File.OpenWrite(path))
			{
				binaryReader.BaseStream.CopyTo(writer, this.size);
			}
		}

		// Token: 0x040009CE RID: 2510
		private bool needSearch;

		// Token: 0x040009CF RID: 2511
		private string path;

		// Token: 0x040009D0 RID: 2512
		private SerializedFile assetsFile;

		// Token: 0x040009D1 RID: 2513
		private long offset;

		// Token: 0x040009D2 RID: 2514
		private long size;

		// Token: 0x040009D3 RID: 2515
		private BinaryReader reader;
	}
}
