using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x0200000C RID: 12
	internal struct Zip64EndOfCentralDirectoryLocator
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002D9C File Offset: 0x00000F9C
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

		// Token: 0x04000021 RID: 33
		public const uint SignatureConstant = 117853008U;

		// Token: 0x04000022 RID: 34
		public const int SizeOfBlockWithoutSignature = 16;

		// Token: 0x04000023 RID: 35
		public uint NumberOfDiskWithZip64EOCD;

		// Token: 0x04000024 RID: 36
		public ulong OffsetOfZip64EOCD;

		// Token: 0x04000025 RID: 37
		public uint TotalNumberOfDisks;
	}
}
