using System;
using System.IO;

namespace Better.StreamingAssets.ZipArchive
{
	// Token: 0x0200000B RID: 11
	internal struct Zip64ExtraField
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A32 File Offset: 0x00000C32
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002A3A File Offset: 0x00000C3A
		public long? UncompressedSize
		{
			get
			{
				return this._uncompressedSize;
			}
			set
			{
				this._uncompressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A49 File Offset: 0x00000C49
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002A51 File Offset: 0x00000C51
		public long? CompressedSize
		{
			get
			{
				return this._compressedSize;
			}
			set
			{
				this._compressedSize = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002A68 File Offset: 0x00000C68
		public long? LocalHeaderOffset
		{
			get
			{
				return this._localHeaderOffset;
			}
			set
			{
				this._localHeaderOffset = value;
				this.UpdateSize();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A77 File Offset: 0x00000C77
		public int? StartDiskNumber
		{
			get
			{
				return this._startDiskNumber;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A80 File Offset: 0x00000C80
		private void UpdateSize()
		{
			this._size = 0;
			if (this._uncompressedSize != null)
			{
				this._size += 8;
			}
			if (this._compressedSize != null)
			{
				this._size += 8;
			}
			if (this._localHeaderOffset != null)
			{
				this._size += 8;
			}
			if (this._startDiskNumber != null)
			{
				this._size += 4;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B04 File Offset: 0x00000D04
		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64Field;
			using (BinaryReader reader = new BinaryReader(extraFieldStream))
			{
				ZipGenericExtraField currentExtraField;
				while (ZipGenericExtraField.TryReadBlock(reader, extraFieldStream.Length, out currentExtraField))
				{
					if (Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(currentExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64Field))
					{
						return zip64Field;
					}
				}
			}
			zip64Field = new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			return zip64Field;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B98 File Offset: 0x00000D98
		private static bool TryGetZip64BlockFromGenericExtraField(ZipGenericExtraField extraField, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber, out Zip64ExtraField zip64Block)
		{
			zip64Block = default(Zip64ExtraField);
			zip64Block._compressedSize = null;
			zip64Block._uncompressedSize = null;
			zip64Block._localHeaderOffset = null;
			zip64Block._startDiskNumber = null;
			if (extraField.Tag != 1)
			{
				return false;
			}
			MemoryStream ms = null;
			bool flag;
			try
			{
				ms = new MemoryStream(extraField.Data);
				using (BinaryReader reader = new BinaryReader(ms))
				{
					ms = null;
					zip64Block._size = extraField.Size;
					ushort expectedSize = 0;
					if (readUncompressedSize)
					{
						expectedSize += 8;
					}
					if (readCompressedSize)
					{
						expectedSize += 8;
					}
					if (readLocalHeaderOffset)
					{
						expectedSize += 8;
					}
					if (readStartDiskNumber)
					{
						expectedSize += 4;
					}
					if (expectedSize != zip64Block._size)
					{
						flag = false;
					}
					else
					{
						if (readUncompressedSize)
						{
							zip64Block._uncompressedSize = new long?(reader.ReadInt64());
						}
						if (readCompressedSize)
						{
							zip64Block._compressedSize = new long?(reader.ReadInt64());
						}
						if (readLocalHeaderOffset)
						{
							zip64Block._localHeaderOffset = new long?(reader.ReadInt64());
						}
						if (readStartDiskNumber)
						{
							zip64Block._startDiskNumber = new int?(reader.ReadInt32());
						}
						long? num = zip64Block._uncompressedSize;
						long num2 = 0L;
						if ((num.GetValueOrDefault() < num2) & (num != null))
						{
							throw new ZipArchiveException("FieldTooBigUncompressedSize");
						}
						num = zip64Block._compressedSize;
						num2 = 0L;
						if ((num.GetValueOrDefault() < num2) & (num != null))
						{
							throw new ZipArchiveException("FieldTooBigCompressedSize");
						}
						num = zip64Block._localHeaderOffset;
						num2 = 0L;
						if ((num.GetValueOrDefault() < num2) & (num != null))
						{
							throw new ZipArchiveException("FieldTooBigLocalHeaderOffset");
						}
						int? startDiskNumber = zip64Block._startDiskNumber;
						int num3 = 0;
						if ((startDiskNumber.GetValueOrDefault() < num3) & (startDiskNumber != null))
						{
							throw new ZipArchiveException("FieldTooBigStartDiskNumber");
						}
						flag = true;
					}
				}
			}
			finally
			{
				if (ms != null)
				{
					ms.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x0400001A RID: 26
		public const int OffsetToFirstField = 4;

		// Token: 0x0400001B RID: 27
		private const ushort TagConstant = 1;

		// Token: 0x0400001C RID: 28
		private ushort _size;

		// Token: 0x0400001D RID: 29
		private long? _uncompressedSize;

		// Token: 0x0400001E RID: 30
		private long? _compressedSize;

		// Token: 0x0400001F RID: 31
		private long? _localHeaderOffset;

		// Token: 0x04000020 RID: 32
		private int? _startDiskNumber;
	}
}
