using System;

namespace System.IO.Compression
{
	// Token: 0x02000025 RID: 37
	internal struct Zip64EndOfCentralDirectoryRecord
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00007484 File Offset: 0x00005684
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

		// Token: 0x0600011F RID: 287 RVA: 0x00007514 File Offset: 0x00005714
		public static void WriteBlock(Stream stream, long numberOfEntries, long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(101075792U);
			binaryWriter.Write(44UL);
			binaryWriter.Write(45);
			binaryWriter.Write(45);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(numberOfEntries);
			binaryWriter.Write(sizeOfCentralDirectory);
			binaryWriter.Write(startOfCentralDirectory);
		}

		// Token: 0x040000E8 RID: 232
		public ulong SizeOfThisRecord;

		// Token: 0x040000E9 RID: 233
		public ushort VersionMadeBy;

		// Token: 0x040000EA RID: 234
		public ushort VersionNeededToExtract;

		// Token: 0x040000EB RID: 235
		public uint NumberOfThisDisk;

		// Token: 0x040000EC RID: 236
		public uint NumberOfDiskWithStartOfCD;

		// Token: 0x040000ED RID: 237
		public ulong NumberOfEntriesOnThisDisk;

		// Token: 0x040000EE RID: 238
		public ulong NumberOfEntriesTotal;

		// Token: 0x040000EF RID: 239
		public ulong SizeOfCentralDirectory;

		// Token: 0x040000F0 RID: 240
		public ulong OffsetOfCentralDirectory;
	}
}
