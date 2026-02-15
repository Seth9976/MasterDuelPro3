using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x0200000A RID: 10
	internal struct ZipGenericExtraField
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029AE File Offset: 0x00000BAE
		public ushort Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000029B6 File Offset: 0x00000BB6
		public ushort Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000029BE File Offset: 0x00000BBE
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029C8 File Offset: 0x00000BC8
		public static bool TryReadBlock(BinaryReader reader, long endExtraField, out ZipGenericExtraField field)
		{
			field = default(ZipGenericExtraField);
			if (endExtraField - reader.BaseStream.Position < 4L)
			{
				return false;
			}
			field._tag = reader.ReadUInt16();
			field._size = reader.ReadUInt16();
			if (endExtraField - reader.BaseStream.Position < (long)((ulong)field._size))
			{
				return false;
			}
			field._data = reader.ReadBytes((int)field._size);
			return true;
		}

		// Token: 0x04000016 RID: 22
		private const int SizeOfHeader = 4;

		// Token: 0x04000017 RID: 23
		private ushort _tag;

		// Token: 0x04000018 RID: 24
		private ushort _size;

		// Token: 0x04000019 RID: 25
		private byte[] _data;
	}
}
