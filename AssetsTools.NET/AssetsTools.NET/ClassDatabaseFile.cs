using System;
using System.Collections.Generic;
using System.IO;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Extra.Decompressors.LZ4;
using LZ4ps;
using SevenZip.Compression.LZMA;

namespace AssetsTools.NET
{
	// Token: 0x02000062 RID: 98
	public class ClassDatabaseFile
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00014D74 File Offset: 0x00012F74
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00014D7C File Offset: 0x00012F7C
		public ClassDatabaseFileHeader Header { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00014D85 File Offset: 0x00012F85
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00014D8D File Offset: 0x00012F8D
		public List<ClassDatabaseType> Classes { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00014D96 File Offset: 0x00012F96
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00014D9E File Offset: 0x00012F9E
		public ClassDatabaseStringTable StringTable { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00014DA7 File Offset: 0x00012FA7
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00014DAF File Offset: 0x00012FAF
		public List<ushort> CommonStringBufferIndices { get; set; }

		// Token: 0x06000360 RID: 864 RVA: 0x00014DB8 File Offset: 0x00012FB8
		public void Read(AssetsFileReader reader)
		{
			if (this.Header == null)
			{
				this.Header = new ClassDatabaseFileHeader();
			}
			this.Header.Read(reader);
			AssetsFileReader decompressedReader = this.GetDecompressedReader(reader);
			int num = decompressedReader.ReadInt32();
			this.Classes = new List<ClassDatabaseType>(num);
			for (int i = 0; i < num; i++)
			{
				ClassDatabaseType classDatabaseType = new ClassDatabaseType();
				classDatabaseType.Read(decompressedReader);
				this.Classes.Add(classDatabaseType);
			}
			if (this.StringTable == null)
			{
				this.StringTable = new ClassDatabaseStringTable();
			}
			this.StringTable.Read(decompressedReader);
			if (this.CommonStringBufferIndices == null)
			{
				this.CommonStringBufferIndices = new List<ushort>();
			}
			int num2 = decompressedReader.ReadInt32();
			for (int j = 0; j < num2; j++)
			{
				this.CommonStringBufferIndices.Add(decompressedReader.ReadUInt16());
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00014EA8 File Offset: 0x000130A8
		public void Write(AssetsFileWriter writer, ClassFileCompressionType compressionType)
		{
			this.Header.CompressionType = compressionType;
			MemoryStream memoryStream = new MemoryStream();
			AssetsFileWriter assetsFileWriter = new AssetsFileWriter(memoryStream);
			assetsFileWriter.Write(this.Classes.Count);
			for (int i = 0; i < this.Classes.Count; i++)
			{
				this.Classes[i].Write(assetsFileWriter);
			}
			this.StringTable.Write(assetsFileWriter);
			assetsFileWriter.Write(this.CommonStringBufferIndices.Count);
			for (int j = 0; j < this.CommonStringBufferIndices.Count; j++)
			{
				assetsFileWriter.Write(this.CommonStringBufferIndices[j]);
			}
			using (MemoryStream compressedStream = this.GetCompressedStream(memoryStream))
			{
				this.Header.CompressedSize = (int)compressedStream.Length;
				this.Header.DecompressedSize = (int)memoryStream.Length;
				this.Header.Write(writer);
				compressedStream.CopyToCompat(writer.BaseStream, -1L, 81920);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00014FD4 File Offset: 0x000131D4
		private AssetsFileReader GetDecompressedReader(AssetsFileReader reader)
		{
			AssetsFileReader assetsFileReader = reader;
			bool flag = this.Header.CompressionType > ClassFileCompressionType.Uncompressed;
			if (flag)
			{
				bool flag2 = this.Header.CompressionType == ClassFileCompressionType.Lz4;
				MemoryStream memoryStream2;
				if (flag2)
				{
					byte[] array = new byte[this.Header.DecompressedSize];
					using (MemoryStream memoryStream = new MemoryStream(reader.ReadBytes(this.Header.CompressedSize)))
					{
						Lz4DecoderStream lz4DecoderStream = new Lz4DecoderStream(memoryStream, long.MaxValue);
						lz4DecoderStream.Read(array, 0, this.Header.DecompressedSize);
						lz4DecoderStream.Dispose();
					}
					memoryStream2 = new MemoryStream(array);
				}
				else
				{
					bool flag3 = this.Header.CompressionType == ClassFileCompressionType.Lzma;
					if (!flag3)
					{
						throw new Exception(string.Format("Class database is using invalid compression type {0}!", this.Header.CompressionType));
					}
					using (MemoryStream memoryStream3 = new MemoryStream(reader.ReadBytes(this.Header.CompressedSize)))
					{
						memoryStream2 = SevenZipHelper.StreamDecompress(memoryStream3);
					}
				}
				assetsFileReader = new AssetsFileReader(memoryStream2);
			}
			return assetsFileReader;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00015118 File Offset: 0x00013318
		private MemoryStream GetCompressedStream(MemoryStream inStream)
		{
			bool flag = this.Header.CompressionType > ClassFileCompressionType.Uncompressed;
			MemoryStream memoryStream;
			if (flag)
			{
				bool flag2 = this.Header.CompressionType == ClassFileCompressionType.Lz4;
				if (flag2)
				{
					byte[] array = LZ4Codec.Encode32HC(inStream.ToArray(), 0, (int)inStream.Length);
					memoryStream = new MemoryStream(array);
				}
				else
				{
					bool flag3 = this.Header.CompressionType == ClassFileCompressionType.Lzma;
					if (!flag3)
					{
						throw new Exception(string.Format("Class database is using invalid compression type {0}!", this.Header.CompressionType));
					}
					MemoryStream memoryStream2 = new MemoryStream();
					SevenZipHelper.Compress(inStream, memoryStream2, null);
					memoryStream2.Position = 0L;
					memoryStream = memoryStream2;
				}
			}
			else
			{
				inStream.Position = 0L;
				memoryStream = inStream;
			}
			return memoryStream;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000151D0 File Offset: 0x000133D0
		public ClassDatabaseType FindAssetClassByID(int id)
		{
			bool flag = id < 0;
			if (flag)
			{
				id = 114;
			}
			foreach (ClassDatabaseType classDatabaseType in this.Classes)
			{
				bool flag2 = classDatabaseType.ClassId == id;
				if (flag2)
				{
					return classDatabaseType;
				}
			}
			return null;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00015248 File Offset: 0x00013448
		public ClassDatabaseType FindAssetClassByName(string name)
		{
			foreach (ClassDatabaseType classDatabaseType in this.Classes)
			{
				bool flag = this.GetString(classDatabaseType.Name) == name;
				if (flag)
				{
					return classDatabaseType;
				}
			}
			return null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000152B8 File Offset: 0x000134B8
		public string GetString(ushort index)
		{
			return this.StringTable.GetString(index);
		}
	}
}
