using System;
using System.IO;
using System.Linq;
using K4os.Compression.LZ4;

namespace AssetStudio
{
	// Token: 0x02000093 RID: 147
	public class BundleFile
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000BC54 File Offset: 0x00009E54
		public BundleFile(FileReader reader)
		{
			this.m_Header = new BundleFile.Header();
			this.m_Header.signature = reader.ReadStringToNull(32767);
			this.m_Header.version = reader.ReadUInt32();
			this.m_Header.unityVersion = reader.ReadStringToNull(32767);
			this.m_Header.unityRevision = reader.ReadStringToNull(32767);
			string signature = this.m_Header.signature;
			if (!(signature == "UnityArchive"))
			{
				if (!(signature == "UnityWeb") && !(signature == "UnityRaw"))
				{
					if (!(signature == "UnityFS"))
					{
						return;
					}
				}
				else if (this.m_Header.version != 6U)
				{
					this.ReadHeaderAndBlocksInfo(reader);
					using (Stream blocksStream = this.CreateBlocksStream(reader.FullPath))
					{
						this.ReadBlocksAndDirectory(reader, blocksStream);
						this.ReadFiles(blocksStream, reader.FullPath);
						return;
					}
				}
				this.ReadHeader(reader);
				this.ReadBlocksInfoAndDirectory(reader);
				using (Stream blocksStream2 = this.CreateBlocksStream(reader.FullPath))
				{
					this.ReadBlocks(reader, blocksStream2);
					this.ReadFiles(blocksStream2, reader.FullPath);
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		private void ReadHeaderAndBlocksInfo(EndianBinaryReader reader)
		{
			if (this.m_Header.version >= 4U)
			{
				reader.ReadBytes(16);
				reader.ReadUInt32();
			}
			reader.ReadUInt32();
			this.m_Header.size = (long)((ulong)reader.ReadUInt32());
			reader.ReadUInt32();
			int levelCount = reader.ReadInt32();
			this.m_BlocksInfo = new BundleFile.StorageBlock[1];
			for (int i = 0; i < levelCount; i++)
			{
				BundleFile.StorageBlock storageBlock = new BundleFile.StorageBlock
				{
					compressedSize = reader.ReadUInt32(),
					uncompressedSize = reader.ReadUInt32()
				};
				if (i == levelCount - 1)
				{
					this.m_BlocksInfo[0] = storageBlock;
				}
			}
			if (this.m_Header.version >= 2U)
			{
				reader.ReadUInt32();
			}
			if (this.m_Header.version >= 3U)
			{
				reader.ReadUInt32();
			}
			reader.Position = this.m_Header.size;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000BE78 File Offset: 0x0000A078
		private Stream CreateBlocksStream(string path)
		{
			long uncompressedSizeSum = this.m_BlocksInfo.Sum((BundleFile.StorageBlock x) => (long)((ulong)x.uncompressedSize));
			Stream blocksStream;
			if (uncompressedSizeSum >= 2147483647L)
			{
				blocksStream = new FileStream(path + ".temp", FileMode.Create, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
			}
			else
			{
				blocksStream = new MemoryStream((int)uncompressedSizeSum);
			}
			return blocksStream;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		private void ReadBlocksAndDirectory(EndianBinaryReader reader, Stream blocksStream)
		{
			bool isCompressed = this.m_Header.signature == "UnityWeb";
			foreach (BundleFile.StorageBlock blockInfo in this.m_BlocksInfo)
			{
				byte[] uncompressedBytes = reader.ReadBytes((int)blockInfo.compressedSize);
				if (isCompressed)
				{
					using (MemoryStream memoryStream = new MemoryStream(uncompressedBytes))
					{
						using (MemoryStream decompressStream = SevenZipHelper.StreamDecompress(memoryStream))
						{
							uncompressedBytes = decompressStream.ToArray();
						}
					}
				}
				blocksStream.Write(uncompressedBytes, 0, uncompressedBytes.Length);
			}
			blocksStream.Position = 0L;
			EndianBinaryReader blocksReader = new EndianBinaryReader(blocksStream, EndianType.BigEndian);
			int nodesCount = blocksReader.ReadInt32();
			this.m_DirectoryInfo = new BundleFile.Node[nodesCount];
			for (int i = 0; i < nodesCount; i++)
			{
				this.m_DirectoryInfo[i] = new BundleFile.Node
				{
					path = blocksReader.ReadStringToNull(32767),
					offset = (long)((ulong)blocksReader.ReadUInt32()),
					size = (long)((ulong)blocksReader.ReadUInt32())
				};
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C004 File Offset: 0x0000A204
		public void ReadFiles(Stream blocksStream, string path)
		{
			this.fileList = new StreamFile[this.m_DirectoryInfo.Length];
			for (int i = 0; i < this.m_DirectoryInfo.Length; i++)
			{
				BundleFile.Node node = this.m_DirectoryInfo[i];
				StreamFile file = new StreamFile();
				this.fileList[i] = file;
				file.path = node.path;
				file.fileName = Path.GetFileName(node.path);
				if (node.size >= 2147483647L)
				{
					string extractPath = path + "_unpacked" + Path.DirectorySeparatorChar.ToString();
					Directory.CreateDirectory(extractPath);
					file.stream = new FileStream(extractPath + file.fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
				}
				else
				{
					file.stream = new MemoryStream((int)node.size);
				}
				blocksStream.Position = node.offset;
				blocksStream.CopyTo(file.stream, node.size);
				file.stream.Position = 0L;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		private void ReadHeader(EndianBinaryReader reader)
		{
			this.m_Header.size = reader.ReadInt64();
			this.m_Header.compressedBlocksInfoSize = reader.ReadUInt32();
			this.m_Header.uncompressedBlocksInfoSize = reader.ReadUInt32();
			this.m_Header.flags = (ArchiveFlags)reader.ReadUInt32();
			if (this.m_Header.signature != "UnityFS")
			{
				reader.ReadByte();
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C16C File Offset: 0x0000A36C
		private void ReadBlocksInfoAndDirectory(EndianBinaryReader reader)
		{
			if (this.m_Header.version >= 7U)
			{
				reader.AlignStream(16);
			}
			byte[] blocksInfoBytes;
			if ((this.m_Header.flags & ArchiveFlags.BlocksInfoAtTheEnd) != (ArchiveFlags)0)
			{
				long position = reader.Position;
				reader.Position = reader.BaseStream.Length - (long)((ulong)this.m_Header.compressedBlocksInfoSize);
				blocksInfoBytes = reader.ReadBytes((int)this.m_Header.compressedBlocksInfoSize);
				reader.Position = position;
			}
			else
			{
				blocksInfoBytes = reader.ReadBytes((int)this.m_Header.compressedBlocksInfoSize);
			}
			uint uncompressedSize = this.m_Header.uncompressedBlocksInfoSize;
			CompressionType compressionType = (CompressionType)(this.m_Header.flags & ArchiveFlags.CompressionTypeMask);
			MemoryStream blocksInfoUncompresseddStream;
			switch (compressionType)
			{
			case CompressionType.None:
				blocksInfoUncompresseddStream = new MemoryStream(blocksInfoBytes);
				break;
			case CompressionType.Lzma:
			{
				blocksInfoUncompresseddStream = new MemoryStream((int)uncompressedSize);
				using (MemoryStream blocksInfoCompressedStream = new MemoryStream(blocksInfoBytes))
				{
					SevenZipHelper.StreamDecompress(blocksInfoCompressedStream, blocksInfoUncompresseddStream, (long)((ulong)this.m_Header.compressedBlocksInfoSize), (long)((ulong)this.m_Header.uncompressedBlocksInfoSize));
				}
				blocksInfoUncompresseddStream.Position = 0L;
				break;
			}
			case CompressionType.Lz4:
			case CompressionType.Lz4HC:
			{
				byte[] uncompressedBytes = new byte[uncompressedSize];
				int numWrite = LZ4Codec.Decode(blocksInfoBytes, uncompressedBytes);
				if ((long)numWrite != (long)((ulong)uncompressedSize))
				{
					throw new IOException(string.Format("Lz4 decompression error, write {0} bytes but expected {1} bytes", numWrite, uncompressedSize));
				}
				blocksInfoUncompresseddStream = new MemoryStream(uncompressedBytes);
				break;
			}
			default:
				throw new IOException(string.Format("Unsupported compression type {0}", compressionType));
			}
			using (EndianBinaryReader blocksInfoReader = new EndianBinaryReader(blocksInfoUncompresseddStream, EndianType.BigEndian))
			{
				blocksInfoReader.ReadBytes(16);
				int blocksInfoCount = blocksInfoReader.ReadInt32();
				this.m_BlocksInfo = new BundleFile.StorageBlock[blocksInfoCount];
				for (int i = 0; i < blocksInfoCount; i++)
				{
					this.m_BlocksInfo[i] = new BundleFile.StorageBlock
					{
						uncompressedSize = blocksInfoReader.ReadUInt32(),
						compressedSize = blocksInfoReader.ReadUInt32(),
						flags = (StorageBlockFlags)blocksInfoReader.ReadUInt16()
					};
				}
				int nodesCount = blocksInfoReader.ReadInt32();
				this.m_DirectoryInfo = new BundleFile.Node[nodesCount];
				for (int j = 0; j < nodesCount; j++)
				{
					this.m_DirectoryInfo[j] = new BundleFile.Node
					{
						offset = blocksInfoReader.ReadInt64(),
						size = blocksInfoReader.ReadInt64(),
						flags = blocksInfoReader.ReadUInt32(),
						path = blocksInfoReader.ReadStringToNull(32767)
					};
				}
			}
			if ((this.m_Header.flags & ArchiveFlags.BlockInfoNeedPaddingAtStart) != (ArchiveFlags)0)
			{
				reader.AlignStream(16);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C404 File Offset: 0x0000A604
		private void ReadBlocks(EndianBinaryReader reader, Stream blocksStream)
		{
			foreach (BundleFile.StorageBlock blockInfo in this.m_BlocksInfo)
			{
				CompressionType compressionType = (CompressionType)(blockInfo.flags & StorageBlockFlags.CompressionTypeMask);
				switch (compressionType)
				{
				case CompressionType.None:
					reader.BaseStream.CopyTo(blocksStream, (long)((ulong)blockInfo.compressedSize));
					break;
				case CompressionType.Lzma:
					SevenZipHelper.StreamDecompress(reader.BaseStream, blocksStream, (long)((ulong)blockInfo.compressedSize), (long)((ulong)blockInfo.uncompressedSize));
					break;
				case CompressionType.Lz4:
				case CompressionType.Lz4HC:
				{
					int compressedSize = (int)blockInfo.compressedSize;
					byte[] compressedBytes = BigArrayPool<byte>.Shared.Rent(compressedSize);
					reader.Read(compressedBytes, 0, compressedSize);
					int uncompressedSize = (int)blockInfo.uncompressedSize;
					byte[] uncompressedBytes = BigArrayPool<byte>.Shared.Rent(uncompressedSize);
					int numWrite = LZ4Codec.Decode(compressedBytes, 0, compressedSize, uncompressedBytes, 0, uncompressedSize);
					if (numWrite != uncompressedSize)
					{
						throw new IOException(string.Format("Lz4 decompression error, write {0} bytes but expected {1} bytes", numWrite, uncompressedSize));
					}
					blocksStream.Write(uncompressedBytes, 0, uncompressedSize);
					BigArrayPool<byte>.Shared.Return(compressedBytes, false);
					BigArrayPool<byte>.Shared.Return(uncompressedBytes, false);
					break;
				}
				default:
					throw new IOException(string.Format("Unsupported compression type {0}", compressionType));
				}
			}
			blocksStream.Position = 0L;
		}

		// Token: 0x040003B6 RID: 950
		public BundleFile.Header m_Header;

		// Token: 0x040003B7 RID: 951
		private BundleFile.StorageBlock[] m_BlocksInfo;

		// Token: 0x040003B8 RID: 952
		private BundleFile.Node[] m_DirectoryInfo;

		// Token: 0x040003B9 RID: 953
		public StreamFile[] fileList;

		// Token: 0x02000094 RID: 148
		public class Header
		{
			// Token: 0x040003BA RID: 954
			public string signature;

			// Token: 0x040003BB RID: 955
			public uint version;

			// Token: 0x040003BC RID: 956
			public string unityVersion;

			// Token: 0x040003BD RID: 957
			public string unityRevision;

			// Token: 0x040003BE RID: 958
			public long size;

			// Token: 0x040003BF RID: 959
			public uint compressedBlocksInfoSize;

			// Token: 0x040003C0 RID: 960
			public uint uncompressedBlocksInfoSize;

			// Token: 0x040003C1 RID: 961
			public ArchiveFlags flags;
		}

		// Token: 0x02000095 RID: 149
		public class StorageBlock
		{
			// Token: 0x040003C2 RID: 962
			public uint compressedSize;

			// Token: 0x040003C3 RID: 963
			public uint uncompressedSize;

			// Token: 0x040003C4 RID: 964
			public StorageBlockFlags flags;
		}

		// Token: 0x02000096 RID: 150
		public class Node
		{
			// Token: 0x040003C5 RID: 965
			public long offset;

			// Token: 0x040003C6 RID: 966
			public long size;

			// Token: 0x040003C7 RID: 967
			public uint flags;

			// Token: 0x040003C8 RID: 968
			public string path;
		}
	}
}
