using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000027 RID: 39
	internal struct ZipCentralDirectoryFileHeader
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00007680 File Offset: 0x00005880
		public static bool TryReadBlock(BinaryReader reader, bool saveExtraFieldsAndComments, out ZipCentralDirectoryFileHeader header)
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
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			header.FilenameLength = reader.ReadUInt16();
			header.ExtraFieldLength = reader.ReadUInt16();
			header.FileCommentLength = reader.ReadUInt16();
			ushort num3 = reader.ReadUInt16();
			header.InternalFileAttributes = reader.ReadUInt16();
			header.ExternalFileAttributes = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			header.Filename = reader.ReadBytes((int)header.FilenameLength);
			bool flag = num2 == uint.MaxValue;
			bool flag2 = num == uint.MaxValue;
			bool flag3 = num4 == uint.MaxValue;
			bool flag4 = num3 == ushort.MaxValue;
			long num5 = reader.BaseStream.Position + (long)((ulong)header.ExtraFieldLength);
			Zip64ExtraField zip64ExtraField;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)header.ExtraFieldLength)))
			{
				if (saveExtraFieldsAndComments)
				{
					header.ExtraFields = ZipGenericExtraField.ParseExtraField(stream);
					zip64ExtraField = Zip64ExtraField.GetAndRemoveZip64Block(header.ExtraFields, flag, flag2, flag3, flag4);
				}
				else
				{
					header.ExtraFields = null;
					zip64ExtraField = Zip64ExtraField.GetJustZip64Block(stream, flag, flag2, flag3, flag4);
				}
			}
			reader.BaseStream.AdvanceToPosition(num5);
			if (saveExtraFieldsAndComments)
			{
				header.FileComment = reader.ReadBytes((int)header.FileCommentLength);
			}
			else
			{
				reader.BaseStream.Position += (long)((ulong)header.FileCommentLength);
				header.FileComment = null;
			}
			header.UncompressedSize = (long)((zip64ExtraField.UncompressedSize == null) ? ((ulong)num2) : ((ulong)zip64ExtraField.UncompressedSize.Value));
			header.CompressedSize = (long)((zip64ExtraField.CompressedSize == null) ? ((ulong)num) : ((ulong)zip64ExtraField.CompressedSize.Value));
			header.RelativeOffsetOfLocalHeader = (long)((zip64ExtraField.LocalHeaderOffset == null) ? ((ulong)num4) : ((ulong)zip64ExtraField.LocalHeaderOffset.Value));
			header.DiskNumberStart = ((zip64ExtraField.StartDiskNumber == null) ? ((int)num3) : zip64ExtraField.StartDiskNumber.Value);
			return true;
		}

		// Token: 0x040000F1 RID: 241
		public byte VersionMadeByCompatibility;

		// Token: 0x040000F2 RID: 242
		public byte VersionMadeBySpecification;

		// Token: 0x040000F3 RID: 243
		public ushort VersionNeededToExtract;

		// Token: 0x040000F4 RID: 244
		public ushort GeneralPurposeBitFlag;

		// Token: 0x040000F5 RID: 245
		public ushort CompressionMethod;

		// Token: 0x040000F6 RID: 246
		public uint LastModified;

		// Token: 0x040000F7 RID: 247
		public uint Crc32;

		// Token: 0x040000F8 RID: 248
		public long CompressedSize;

		// Token: 0x040000F9 RID: 249
		public long UncompressedSize;

		// Token: 0x040000FA RID: 250
		public ushort FilenameLength;

		// Token: 0x040000FB RID: 251
		public ushort ExtraFieldLength;

		// Token: 0x040000FC RID: 252
		public ushort FileCommentLength;

		// Token: 0x040000FD RID: 253
		public int DiskNumberStart;

		// Token: 0x040000FE RID: 254
		public ushort InternalFileAttributes;

		// Token: 0x040000FF RID: 255
		public uint ExternalFileAttributes;

		// Token: 0x04000100 RID: 256
		public long RelativeOffsetOfLocalHeader;

		// Token: 0x04000101 RID: 257
		public byte[] Filename;

		// Token: 0x04000102 RID: 258
		public byte[] FileComment;

		// Token: 0x04000103 RID: 259
		public List<ZipGenericExtraField> ExtraFields;
	}
}
