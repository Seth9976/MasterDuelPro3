using System;

namespace System.IO.Compression
{
	// Token: 0x02000028 RID: 40
	internal struct ZipEndOfCentralDirectoryBlock
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00007904 File Offset: 0x00005B04
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory, byte[] archiveComment)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			ushort num = ((numberOfEntries > 65535L) ? ushort.MaxValue : ((ushort)numberOfEntries));
			uint num2 = ((startOfCentralDirectory > (long)((ulong)(-1))) ? uint.MaxValue : ((uint)startOfCentralDirectory));
			uint num3 = ((sizeOfCentralDirectory > (long)((ulong)(-1))) ? uint.MaxValue : ((uint)sizeOfCentralDirectory));
			binaryWriter.Write(101010256U);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(num);
			binaryWriter.Write(num);
			binaryWriter.Write(num3);
			binaryWriter.Write(num2);
			binaryWriter.Write((archiveComment != null) ? ((ushort)archiveComment.Length) : 0);
			if (archiveComment != null)
			{
				binaryWriter.Write(archiveComment);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007994 File Offset: 0x00005B94
		public static bool TryReadBlock(BinaryReader reader, out ZipEndOfCentralDirectoryBlock eocdBlock)
		{
			eocdBlock = default(ZipEndOfCentralDirectoryBlock);
			if (reader.ReadUInt32() != 101010256U)
			{
				return false;
			}
			eocdBlock.Signature = 101010256U;
			eocdBlock.NumberOfThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk = reader.ReadUInt16();
			eocdBlock.NumberOfEntriesInTheCentralDirectory = reader.ReadUInt16();
			eocdBlock.SizeOfCentralDirectory = reader.ReadUInt32();
			eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber = reader.ReadUInt32();
			ushort num = reader.ReadUInt16();
			eocdBlock.ArchiveComment = reader.ReadBytes((int)num);
			return true;
		}

		// Token: 0x04000104 RID: 260
		public uint Signature;

		// Token: 0x04000105 RID: 261
		public ushort NumberOfThisDisk;

		// Token: 0x04000106 RID: 262
		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		// Token: 0x04000107 RID: 263
		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		// Token: 0x04000108 RID: 264
		public ushort NumberOfEntriesInTheCentralDirectory;

		// Token: 0x04000109 RID: 265
		public uint SizeOfCentralDirectory;

		// Token: 0x0400010A RID: 266
		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		// Token: 0x0400010B RID: 267
		public byte[] ArchiveComment;
	}
}
