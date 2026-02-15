using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000022 RID: 34
	internal struct ZipGenericExtraField
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006CD4 File Offset: 0x00004ED4
		public ushort Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006CDC File Offset: 0x00004EDC
		public ushort Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public byte[] Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006CEC File Offset: 0x00004EEC
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(this.Tag);
			binaryWriter.Write(this.Size);
			binaryWriter.Write(this.Data);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006D18 File Offset: 0x00004F18
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

		// Token: 0x0600010B RID: 267 RVA: 0x00006D84 File Offset: 0x00004F84
		public static List<ZipGenericExtraField> ParseExtraField(Stream extraFieldData)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			using (BinaryReader binaryReader = new BinaryReader(extraFieldData))
			{
				ZipGenericExtraField zipGenericExtraField;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldData.Length, out zipGenericExtraField))
				{
					list.Add(zipGenericExtraField);
				}
			}
			return list;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public static int TotalSize(List<ZipGenericExtraField> fields)
		{
			int num = 0;
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				num += (int)(zipGenericExtraField.Size + 4);
			}
			return num;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006E2C File Offset: 0x0000502C
		public static void WriteAllBlocks(List<ZipGenericExtraField> fields, Stream stream)
		{
			foreach (ZipGenericExtraField zipGenericExtraField in fields)
			{
				zipGenericExtraField.WriteBlock(stream);
			}
		}

		// Token: 0x040000DD RID: 221
		private ushort _tag;

		// Token: 0x040000DE RID: 222
		private ushort _size;

		// Token: 0x040000DF RID: 223
		private byte[] _data;
	}
}
