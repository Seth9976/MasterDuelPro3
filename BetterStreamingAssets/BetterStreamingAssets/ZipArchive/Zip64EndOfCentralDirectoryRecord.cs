using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x0200000D RID: 13
	internal struct Zip64EndOfCentralDirectoryRecord
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002DDC File Offset: 0x00000FDC
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryRecord zip64EOCDRecord)
		{
			zip64EOCDRecord = default(Zip64EndOfCentralDirectoryRecord);
			if (reader.ReadUInt32() != 101075792U)
			{
				return false;
			}
			zip64EOCDRecord.SizeOfThisRecord = reader.ReadUInt64();
			zip64EOCDRecord.VersionMadeBy = reader.ReadUInt16();
			zip64EOCDRecord.VersionNeededToExtract = reader.ReadUInt16();
			zip64EOCDRecord.NumberOfThisDisk = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfDiskWithStartOfCD = reader.ReadUInt32();
			zip64EOCDRecord.NumberOfEntriesOnThisDisk = reader.ReadUInt64();
			zip64EOCDRecord.NumberOfEntriesTotal = reader.ReadUInt64();
			zip64EOCDRecord.SizeOfCentralDirectory = reader.ReadUInt64();
			zip64EOCDRecord.OffsetOfCentralDirectory = reader.ReadUInt64();
			return true;
		}

		// Token: 0x04000026 RID: 38
		private const uint SignatureConstant = 101075792U;

		// Token: 0x04000027 RID: 39
		private const ulong NormalSize = 44UL;

		// Token: 0x04000028 RID: 40
		public ulong SizeOfThisRecord;

		// Token: 0x04000029 RID: 41
		public ushort VersionMadeBy;

		// Token: 0x0400002A RID: 42
		public ushort VersionNeededToExtract;

		// Token: 0x0400002B RID: 43
		public uint NumberOfThisDisk;

		// Token: 0x0400002C RID: 44
		public uint NumberOfDiskWithStartOfCD;

		// Token: 0x0400002D RID: 45
		public ulong NumberOfEntriesOnThisDisk;

		// Token: 0x0400002E RID: 46
		public ulong NumberOfEntriesTotal;

		// Token: 0x0400002F RID: 47
		public ulong SizeOfCentralDirectory;

		// Token: 0x04000030 RID: 48
		public ulong OffsetOfCentralDirectory;
	}
}
