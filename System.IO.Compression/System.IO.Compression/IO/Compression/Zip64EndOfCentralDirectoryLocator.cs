using System;

namespace System.IO.Compression
{
	// Token: 0x02000024 RID: 36
	internal struct Zip64EndOfCentralDirectoryLocator
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0000741F File Offset: 0x0000561F
		public static bool TryReadBlock(BinaryReader reader, out Zip64EndOfCentralDirectoryLocator zip64EOCDLocator)
		{
			zip64EOCDLocator = default(Zip64EndOfCentralDirectoryLocator);
			if (reader.ReadUInt32() != 117853008U)
			{
				return false;
			}
			zip64EOCDLocator.NumberOfDiskWithZip64EOCD = reader.ReadUInt32();
			zip64EOCDLocator.OffsetOfZip64EOCD = reader.ReadUInt64();
			zip64EOCDLocator.TotalNumberOfDisks = reader.ReadUInt32();
			return true;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000745C File Offset: 0x0000565C
		public static void WriteBlock(Stream stream, long zip64EOCDRecordStart)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(117853008U);
			binaryWriter.Write(0U);
			binaryWriter.Write(zip64EOCDRecordStart);
			binaryWriter.Write(1U);
		}

		// Token: 0x040000E5 RID: 229
		public uint NumberOfDiskWithZip64EOCD;

		// Token: 0x040000E6 RID: 230
		public ulong OffsetOfZip64EOCD;

		// Token: 0x040000E7 RID: 231
		public uint TotalNumberOfDisks;
	}
}
