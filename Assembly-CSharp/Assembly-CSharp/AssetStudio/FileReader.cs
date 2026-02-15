using System;
using System.IO;
using System.Linq;

namespace AssetStudio
{
	// Token: 0x0200014E RID: 334
	public class FileReader : EndianBinaryReader
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x00014F59 File Offset: 0x00013159
		public FileReader(string path)
			: this(path, File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00014F6B File Offset: 0x0001316B
		public FileReader(string path, Stream stream)
			: base(stream, EndianType.BigEndian)
		{
			this.FullPath = Path.GetFullPath(path);
			this.FileName = Path.GetFileName(path);
			this.FileType = this.CheckFileType();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00014F9C File Offset: 0x0001319C
		private FileType CheckFileType()
		{
			string signature = this.ReadStringToNull(20);
			base.Position = 0L;
			if (signature == "UnityWeb" || signature == "UnityRaw" || signature == "UnityArchive" || signature == "UnityFS")
			{
				return FileType.BundleFile;
			}
			if (signature == "UnityWebData1.0")
			{
				return FileType.WebFile;
			}
			byte[] magic = this.ReadBytes(2);
			base.Position = 0L;
			if (FileReader.gzipMagic.SequenceEqual(magic))
			{
				return FileType.GZipFile;
			}
			base.Position = 32L;
			magic = this.ReadBytes(6);
			base.Position = 0L;
			if (FileReader.brotliMagic.SequenceEqual(magic))
			{
				return FileType.BrotliFile;
			}
			if (this.IsSerializedFile())
			{
				return FileType.AssetsFile;
			}
			magic = this.ReadBytes(4);
			base.Position = 0L;
			if (FileReader.zipMagic.SequenceEqual(magic) || FileReader.zipSpannedMagic.SequenceEqual(magic))
			{
				return FileType.ZipFile;
			}
			return FileType.ResourceFile;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015080 File Offset: 0x00013280
		private bool IsSerializedFile()
		{
			long fileSize = this.BaseStream.Length;
			if (fileSize < 20L)
			{
				return false;
			}
			this.ReadUInt32();
			long m_FileSize = (long)((ulong)this.ReadUInt32());
			int num = (int)this.ReadUInt32();
			long m_DataOffset = (long)((ulong)this.ReadUInt32());
			this.ReadByte();
			this.ReadBytes(3);
			if (num >= 22)
			{
				if (fileSize < 48L)
				{
					base.Position = 0L;
					return false;
				}
				this.ReadUInt32();
				m_FileSize = this.ReadInt64();
				m_DataOffset = this.ReadInt64();
			}
			base.Position = 0L;
			return m_FileSize == fileSize && m_DataOffset <= fileSize;
		}

		// Token: 0x04000926 RID: 2342
		public string FullPath;

		// Token: 0x04000927 RID: 2343
		public string FileName;

		// Token: 0x04000928 RID: 2344
		public FileType FileType;

		// Token: 0x04000929 RID: 2345
		private static readonly byte[] gzipMagic = new byte[] { 31, 139 };

		// Token: 0x0400092A RID: 2346
		private static readonly byte[] brotliMagic = new byte[] { 98, 114, 111, 116, 108, 105 };

		// Token: 0x0400092B RID: 2347
		private static readonly byte[] zipMagic = new byte[] { 80, 75, 3, 4 };

		// Token: 0x0400092C RID: 2348
		private static readonly byte[] zipSpannedMagic = new byte[] { 80, 75, 7, 8 };
	}
}
