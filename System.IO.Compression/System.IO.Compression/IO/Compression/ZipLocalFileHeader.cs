using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000026 RID: 38
	internal readonly struct ZipLocalFileHeader
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00007574 File Offset: 0x00005774
		public static List<ZipGenericExtraField> GetExtraFields(BinaryReader reader)
		{
			reader.BaseStream.Seek(26L, SeekOrigin.Current);
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			reader.BaseStream.Seek((long)((ulong)num), SeekOrigin.Current);
			List<ZipGenericExtraField> list;
			using (Stream stream = new SubReadStream(reader.BaseStream, reader.BaseStream.Position, (long)((ulong)num2)))
			{
				list = ZipGenericExtraField.ParseExtraField(stream);
			}
			Zip64ExtraField.RemoveZip64Blocks(list);
			return list;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000075F4 File Offset: 0x000057F4
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
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			if (reader.BaseStream.Length < reader.BaseStream.Position + (long)((ulong)num) + (long)((ulong)num2))
			{
				return false;
			}
			reader.BaseStream.Seek((long)(num + num2), SeekOrigin.Current);
			return true;
		}
	}
}
