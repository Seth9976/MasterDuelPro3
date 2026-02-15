using System;
using System.Collections.Generic;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x0200000F RID: 15
	internal struct ZipCentralDirectoryFileHeader
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002EF8 File Offset: 0x000010F8
		public static bool TryReadBlock(BinaryReader reader, out ZipCentralDirectoryFileHeader header)
		{
			header = default(ZipCentralDirectoryFileHeader);
			if (reader.ReadUInt32() != 33639248U)
			{
				return false;
			}
			header.VersionMadeBySpecification = reader.ReadByte();
			header.VersionMadeByCompatibility = reader.ReadByte();
			header.VersionNeededToExtract = reader.ReadUInt16();
			header.GeneralPurposeBitFlag = reader.ReadUInt16();
			header.CompressionMethod = reader.ReadUInt16();
			header.LastModified = reader.ReadUInt32();
			header.Crc32 = reader.ReadUInt32();
			uint compressedSizeSmall = reader.ReadUInt32();
			uint uncompressedSizeSmall = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort diskNumberStartSmall = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint relativeOffsetOfLocalHeaderSmall = reader.ReadUInt32();
			header.Filename = reader.ReadBytes((int)header.FilenameLength);
			bool uncompressedSizeInZip64 = uncompressedSizeSmall == uint.MaxValue;
			bool compressedSizeInZip64 = compressedSizeSmall == uint.MaxValue;
			bool relativeOffsetInZip64 = relativeOffsetOfLocalHeaderSmall == uint.MaxValue;
			bool diskNumberStartInZip64 = diskNumberStartSmall == ushort.MaxValue;
			long endExtraFields = reader.BaseStream.Position + (long)((ulong)header.ExtraFieldLength);
			Zip64ExtraField zip64;
			using (Stream str = new SubReadOnlyStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)header.ExtraFieldLength), true))
			{
				header.ExtraFields = null;
				zip64 = Zip64ExtraField.GetJustZip64Block(str, uncompressedSizeInZip64, compressedSizeInZip64, relativeOffsetInZip64, diskNumberStartInZip64);
			}
			reader.BaseStream.AdvanceToPosition(endExtraFields);
			reader.BaseStream.Position += (long)((ulong)header.FileCommentLength);
			header.FileComment = null;
			header.UncompressedSize = (long)((zip64.UncompressedSize == null) ? ((ulong)uncompressedSizeSmall) : ((ulong)zip64.UncompressedSize.Value));
			header.CompressedSize = (long)((zip64.CompressedSize == null) ? ((ulong)compressedSizeSmall) : ((ulong)zip64.CompressedSize.Value));
			header.RelativeOffsetOfLocalHeader = (long)((zip64.LocalHeaderOffset == null) ? ((ulong)relativeOffsetOfLocalHeaderSmall) : ((ulong)zip64.LocalHeaderOffset.Value));
			header.DiskNumberStart = ((zip64.StartDiskNumber == null) ? ((int)diskNumberStartSmall) : zip64.StartDiskNumber.Value);
			return true;
		}

		// Token: 0x04000036 RID: 54
		public const uint SignatureConstant = 33639248U;

		// Token: 0x04000037 RID: 55
		public byte VersionMadeByCompatibility;

		// Token: 0x04000038 RID: 56
		public byte VersionMadeBySpecification;

		// Token: 0x04000039 RID: 57
		public ushort VersionNeededToExtract;

		// Token: 0x0400003A RID: 58
		public ushort GeneralPurposeBitFlag;

		// Token: 0x0400003B RID: 59
		public ushort CompressionMethod;

		// Token: 0x0400003C RID: 60
		public uint LastModified;

		// Token: 0x0400003D RID: 61
		public uint Crc32;

		// Token: 0x0400003E RID: 62
		public long CompressedSize;

		// Token: 0x0400003F RID: 63
		public long UncompressedSize;

		// Token: 0x04000040 RID: 64
		public ushort FilenameLength;

		// Token: 0x04000041 RID: 65
		public ushort ExtraFieldLength;

		// Token: 0x04000042 RID: 66
		public ushort FileCommentLength;

		// Token: 0x04000043 RID: 67
		public int DiskNumberStart;

		// Token: 0x04000044 RID: 68
		public ushort InternalFileAttributes;

		// Token: 0x04000045 RID: 69
		public uint ExternalFileAttributes;

		// Token: 0x04000046 RID: 70
		public long RelativeOffsetOfLocalHeader;

		// Token: 0x04000047 RID: 71
		public byte[] Filename;

		// Token: 0x04000048 RID: 72
		public byte[] FileComment;

		// Token: 0x04000049 RID: 73
		public List<ZipGenericExtraField> ExtraFields;
	}
}
