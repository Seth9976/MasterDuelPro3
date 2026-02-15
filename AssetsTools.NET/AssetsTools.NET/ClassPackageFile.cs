using System;
using System.Collections.Generic;
using System.IO;
using AssetsTools.NET.Extra;
using AssetsTools.NET.Extra.Decompressors.LZ4;
using LZ4ps;
using SevenZip.Compression.LZMA;

namespace AssetsTools.NET
{
	// Token: 0x02000069 RID: 105
	public class ClassPackageFile
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00015969 File Offset: 0x00013B69
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00015971 File Offset: 0x00013B71
		public ClassPackageHeader Header { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001597A File Offset: 0x00013B7A
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00015982 File Offset: 0x00013B82
		public ClassPackageTypeTree TpkTypeTree { get; set; }

		// Token: 0x060003A3 RID: 931 RVA: 0x0001598C File Offset: 0x00013B8C
		public void Read(AssetsFileReader reader)
		{
			this.Header = new ClassPackageHeader();
			this.Header.Read(reader);
			bool flag = this.Header.CompressionType == ClassFileCompressionType.Lz4;
			AssetsFileReader assetsFileReader;
			if (flag)
			{
				byte[] array = new byte[this.Header.DecompressedSize];
				using (MemoryStream memoryStream = new MemoryStream(reader.ReadBytes((int)this.Header.CompressedSize)))
				{
					Lz4DecoderStream lz4DecoderStream = new Lz4DecoderStream(memoryStream, long.MaxValue);
					lz4DecoderStream.Read(array, 0, (int)this.Header.DecompressedSize);
					lz4DecoderStream.Dispose();
				}
				MemoryStream memoryStream2 = new MemoryStream(array);
				assetsFileReader = new AssetsFileReader(memoryStream2);
				assetsFileReader.Position = 0L;
			}
			else
			{
				bool flag2 = this.Header.CompressionType == ClassFileCompressionType.Lzma;
				if (flag2)
				{
					using (MemoryStream memoryStream3 = new MemoryStream(reader.ReadBytes((int)this.Header.CompressedSize)))
					{
						MemoryStream memoryStream4 = SevenZipHelper.StreamDecompress(memoryStream3, (long)this.Header.DecompressedSize);
						assetsFileReader = new AssetsFileReader(memoryStream4);
						assetsFileReader.Position = 0L;
					}
				}
				else
				{
					assetsFileReader = reader;
				}
			}
			this.TpkTypeTree = new ClassPackageTypeTree();
			this.TpkTypeTree.Read(assetsFileReader);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015AEC File Offset: 0x00013CEC
		public void Read(string path)
		{
			this.Read(new AssetsFileReader(File.OpenRead(path)));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00015B00 File Offset: 0x00013D00
		public void Write(AssetsFileWriter writer, ClassFileCompressionType compressionType)
		{
			this.Header.CompressionType = compressionType;
			MemoryStream memoryStream = new MemoryStream();
			AssetsFileWriter assetsFileWriter = new AssetsFileWriter(memoryStream);
			this.TpkTypeTree.Write(assetsFileWriter);
			bool flag = this.Header.CompressionType == ClassFileCompressionType.Lz4;
			if (flag)
			{
				byte[] array = LZ4Codec.Encode32HC(memoryStream.ToArray(), 0, (int)memoryStream.Length);
				this.Header.CompressedSize = (uint)array.Length;
				this.Header.DecompressedSize = (uint)memoryStream.Length;
				this.Header.Write(writer);
				writer.Write(array);
			}
			else
			{
				bool flag2 = this.Header.CompressionType == ClassFileCompressionType.Lzma;
				if (flag2)
				{
					MemoryStream memoryStream2 = new MemoryStream();
					SevenZipHelper.Compress(memoryStream, memoryStream2, null);
					this.Header.CompressedSize = (uint)memoryStream2.Length;
					this.Header.DecompressedSize = (uint)memoryStream.Length;
					this.Header.Write(writer);
					memoryStream2.CopyToCompat(writer.BaseStream, -1L, 81920);
				}
				else
				{
					this.Header.CompressedSize = (uint)memoryStream.Length;
					this.Header.DecompressedSize = (uint)memoryStream.Length;
					this.Header.Write(writer);
					memoryStream.CopyToCompat(writer.BaseStream, -1L, 81920);
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015C54 File Offset: 0x00013E54
		public void Write(string path, ClassFileCompressionType compressionType)
		{
			this.Write(new AssetsFileWriter(File.OpenWrite(path)), compressionType);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015C6C File Offset: 0x00013E6C
		public ClassDatabaseFile GetClassDatabase(string version)
		{
			return this.GetClassDatabase(new UnityVersion(version));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00015C8C File Offset: 0x00013E8C
		public ClassDatabaseFile GetClassDatabase(UnityVersion version)
		{
			bool flag = this.Header == null;
			if (flag)
			{
				throw new Exception("Header not loaded! (Did you forget to call package.Read?)");
			}
			ClassDatabaseFile classDatabaseFile = new ClassDatabaseFile();
			classDatabaseFile.Header = new ClassDatabaseFileHeader
			{
				Magic = "CLDB",
				FileVersion = 1,
				Version = version,
				CompressionType = ClassFileCompressionType.Uncompressed,
				CompressedSize = 0,
				DecompressedSize = 0
			};
			classDatabaseFile.Classes = new List<ClassDatabaseType>();
			foreach (ClassPackageClassInfo classPackageClassInfo in this.TpkTypeTree.ClassInformation)
			{
				ClassPackageType typeForVersion = classPackageClassInfo.GetTypeForVersion(version);
				bool flag2 = typeForVersion == null;
				if (!flag2)
				{
					ClassDatabaseType classDatabaseType = new ClassDatabaseType
					{
						ClassId = classPackageClassInfo.ClassId,
						Name = typeForVersion.Name,
						BaseName = typeForVersion.BaseName,
						Flags = typeForVersion.Flags
					};
					bool flag3 = typeForVersion.EditorRootNode != ushort.MaxValue;
					if (flag3)
					{
						classDatabaseType.EditorRootNode = this.ConvertNodes(typeForVersion.EditorRootNode);
					}
					bool flag4 = typeForVersion.ReleaseRootNode != ushort.MaxValue;
					if (flag4)
					{
						classDatabaseType.ReleaseRootNode = this.ConvertNodes(typeForVersion.ReleaseRootNode);
					}
					classDatabaseFile.Classes.Add(classDatabaseType);
				}
			}
			classDatabaseFile.StringTable = this.TpkTypeTree.StringTable;
			byte commonStringLengthForVersion = this.TpkTypeTree.CommonString.GetCommonStringLengthForVersion(version);
			classDatabaseFile.CommonStringBufferIndices = new List<ushort>((int)commonStringLengthForVersion);
			for (int i = 0; i < (int)commonStringLengthForVersion; i++)
			{
				classDatabaseFile.CommonStringBufferIndices.Add(this.TpkTypeTree.CommonString.StringBufferIndices[i]);
			}
			return classDatabaseFile;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00015E8C File Offset: 0x0001408C
		private ClassDatabaseTypeNode ConvertNodes(ushort tpkNodeIdx)
		{
			ClassPackageTypeNode classPackageTypeNode = this.TpkTypeTree.Nodes[(int)tpkNodeIdx];
			ClassDatabaseTypeNode classDatabaseTypeNode = new ClassDatabaseTypeNode
			{
				TypeName = classPackageTypeNode.TypeName,
				FieldName = classPackageTypeNode.FieldName,
				ByteSize = classPackageTypeNode.ByteSize,
				Version = classPackageTypeNode.Version,
				TypeFlags = classPackageTypeNode.TypeFlags,
				MetaFlag = classPackageTypeNode.MetaFlag
			};
			int num = classPackageTypeNode.SubNodes.Length;
			classDatabaseTypeNode.Children = new List<ClassDatabaseTypeNode>(num);
			for (int i = 0; i < num; i++)
			{
				classDatabaseTypeNode.Children.Add(this.ConvertNodes(classPackageTypeNode.SubNodes[i]));
			}
			return classDatabaseTypeNode;
		}
	}
}
