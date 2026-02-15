using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x0200000E RID: 14
	internal struct ZipLocalFileHeader
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002E6C File Offset: 0x0000106C
		public static bool TrySkipBlock(BinaryReader reader)
		{
			if (reader.ReadUInt32() != 67324752U)
			{
				return false;
			}
			if (reader.BaseStream.Length < reader.BaseStream.Position + 22L)
			{
				return false;
			}
			reader.BaseStream.Seek(22L, SeekOrigin.Current);
			ushort filenameLength = reader.ReadUInt16();
			ushort extraFieldLength = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + (long)((ulong)filenameLength) + (long)((ulong)extraFieldLength))
			{
				return false;
			}
			reader.BaseStream.Seek((long)(filenameLength + extraFieldLength), SeekOrigin.Current);
			return true;
		}

		// Token: 0x04000031 RID: 49
		public const uint DataDescriptorSignature = 134695760U;

		// Token: 0x04000032 RID: 50
		public const uint SignatureConstant = 67324752U;

		// Token: 0x04000033 RID: 51
		public const int OffsetToCrcFromHeaderStart = 14;

		// Token: 0x04000034 RID: 52
		public const int OffsetToBitFlagFromHeaderStart = 6;

		// Token: 0x04000035 RID: 53
		public const int SizeOfLocalHeader = 30;
	}
}
