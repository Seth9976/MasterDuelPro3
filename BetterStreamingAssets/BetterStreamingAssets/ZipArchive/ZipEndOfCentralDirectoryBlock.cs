using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x02000010 RID: 16
	internal struct ZipEndOfCentralDirectoryBlock
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003140 File Offset: 0x00001340
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
			ushort commentLength = reader.ReadUInt16();
			eocdBlock.ArchiveComment = reader.ReadBytes((int)commentLength);
			return true;
		}

		// Token: 0x0400004A RID: 74
		public const uint SignatureConstant = 101010256U;

		// Token: 0x0400004B RID: 75
		public const int SizeOfBlockWithoutSignature = 18;

		// Token: 0x0400004C RID: 76
		public uint Signature;

		// Token: 0x0400004D RID: 77
		public ushort NumberOfThisDisk;

		// Token: 0x0400004E RID: 78
		public ushort NumberOfTheDiskWithTheStartOfTheCentralDirectory;

		// Token: 0x0400004F RID: 79
		public ushort NumberOfEntriesInTheCentralDirectoryOnThisDisk;

		// Token: 0x04000050 RID: 80
		public ushort NumberOfEntriesInTheCentralDirectory;

		// Token: 0x04000051 RID: 81
		public uint SizeOfCentralDirectory;

		// Token: 0x04000052 RID: 82
		public uint OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;

		// Token: 0x04000053 RID: 83
		public byte[] ArchiveComment;
	}
}
