using System;
using System.Collections.Generic;

namespace System.IO.Compression
{
	// Token: 0x02000023 RID: 35
	internal struct Zip64ExtraField
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006E7C File Offset: 0x0000507C
		public ushort TotalSize
		{
			get
			{
				return this._size + 4;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006E87 File Offset: 0x00005087
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006E8F File Offset: 0x0000508F
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006E9E File Offset: 0x0000509E
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006EA6 File Offset: 0x000050A6
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006EB5 File Offset: 0x000050B5
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006EBD File Offset: 0x000050BD
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006ECC File Offset: 0x000050CC
		public int? StartDiskNumber
		{
			get
			{
				return this._startDiskNumber;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006ED4 File Offset: 0x000050D4
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

		// Token: 0x06000117 RID: 279 RVA: 0x00006F58 File Offset: 0x00005158
		public static Zip64ExtraField GetJustZip64Block(Stream extraFieldStream, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64ExtraField;
			using (BinaryReader binaryReader = new BinaryReader(extraFieldStream))
			{
				ZipGenericExtraField zipGenericExtraField;
				while (ZipGenericExtraField.TryReadBlock(binaryReader, extraFieldStream.Length, out zipGenericExtraField))
				{
					if (Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(zipGenericExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64ExtraField))
					{
						return zip64ExtraField;
					}
				}
			}
			zip64ExtraField = new Zip64ExtraField
			{
				_compressedSize = null,
				_uncompressedSize = null,
				_localHeaderOffset = null,
				_startDiskNumber = null
			};
			return zip64ExtraField;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006FEC File Offset: 0x000051EC
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
			MemoryStream memoryStream = null;
			bool flag;
			try
			{
				memoryStream = new MemoryStream(extraField.Data);
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					memoryStream = null;
					zip64Block._size = extraField.Size;
					ushort num = 0;
					if (readUncompressedSize)
					{
						num += 8;
					}
					if (readCompressedSize)
					{
						num += 8;
					}
					if (readLocalHeaderOffset)
					{
						num += 8;
					}
					if (readStartDiskNumber)
					{
						num += 4;
					}
					if (num != zip64Block._size)
					{
						flag = false;
					}
					else
					{
						if (readUncompressedSize)
						{
							zip64Block._uncompressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readCompressedSize)
						{
							zip64Block._compressedSize = new long?(binaryReader.ReadInt64());
						}
						if (readLocalHeaderOffset)
						{
							zip64Block._localHeaderOffset = new long?(binaryReader.ReadInt64());
						}
						if (readStartDiskNumber)
						{
							zip64Block._startDiskNumber = new int?(binaryReader.ReadInt32());
						}
						long? num2 = zip64Block._uncompressedSize;
						long num3 = 0L;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new InvalidDataException("Uncompressed Size cannot be held in an Int64.");
						}
						num2 = zip64Block._compressedSize;
						num3 = 0L;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new InvalidDataException("Compressed Size cannot be held in an Int64.");
						}
						num2 = zip64Block._localHeaderOffset;
						num3 = 0L;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new InvalidDataException("Local Header Offset cannot be held in an Int64.");
						}
						int? startDiskNumber = zip64Block._startDiskNumber;
						int num4 = 0;
						if ((startDiskNumber.GetValueOrDefault() < num4) & (startDiskNumber != null))
						{
							throw new InvalidDataException("Start Disk Number cannot be held in an Int64.");
						}
						flag = true;
					}
				}
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000071F0 File Offset: 0x000053F0
		public static Zip64ExtraField GetAndRemoveZip64Block(List<ZipGenericExtraField> extraFields, bool readUncompressedSize, bool readCompressedSize, bool readLocalHeaderOffset, bool readStartDiskNumber)
		{
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			zip64ExtraField._compressedSize = null;
			zip64ExtraField._uncompressedSize = null;
			zip64ExtraField._localHeaderOffset = null;
			zip64ExtraField._startDiskNumber = null;
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			bool flag = false;
			foreach (ZipGenericExtraField zipGenericExtraField in extraFields)
			{
				if (zipGenericExtraField.Tag == 1)
				{
					list.Add(zipGenericExtraField);
					if (!flag && Zip64ExtraField.TryGetZip64BlockFromGenericExtraField(zipGenericExtraField, readUncompressedSize, readCompressedSize, readLocalHeaderOffset, readStartDiskNumber, out zip64ExtraField))
					{
						flag = true;
					}
				}
			}
			foreach (ZipGenericExtraField zipGenericExtraField2 in list)
			{
				extraFields.Remove(zipGenericExtraField2);
			}
			return zip64ExtraField;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000072E4 File Offset: 0x000054E4
		public static void RemoveZip64Blocks(List<ZipGenericExtraField> extraFields)
		{
			List<ZipGenericExtraField> list = new List<ZipGenericExtraField>();
			foreach (ZipGenericExtraField zipGenericExtraField in extraFields)
			{
				if (zipGenericExtraField.Tag == 1)
				{
					list.Add(zipGenericExtraField);
				}
			}
			foreach (ZipGenericExtraField zipGenericExtraField2 in list)
			{
				extraFields.Remove(zipGenericExtraField2);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007380 File Offset: 0x00005580
		public void WriteBlock(Stream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(1);
			binaryWriter.Write(this._size);
			if (this._uncompressedSize != null)
			{
				binaryWriter.Write(this._uncompressedSize.Value);
			}
			if (this._compressedSize != null)
			{
				binaryWriter.Write(this._compressedSize.Value);
			}
			if (this._localHeaderOffset != null)
			{
				binaryWriter.Write(this._localHeaderOffset.Value);
			}
			if (this._startDiskNumber != null)
			{
				binaryWriter.Write(this._startDiskNumber.Value);
			}
		}

		// Token: 0x040000E0 RID: 224
		private ushort _size;

		// Token: 0x040000E1 RID: 225
		private long? _uncompressedSize;

		// Token: 0x040000E2 RID: 226
		private long? _compressedSize;

		// Token: 0x040000E3 RID: 227
		private long? _localHeaderOffset;

		// Token: 0x040000E4 RID: 228
		private int? _startDiskNumber;
	}
}
