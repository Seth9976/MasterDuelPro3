using System;
using System.Collections.Generic;

namespace AssetsTools.NET
{
	// Token: 0x0200003A RID: 58
	public class AssetFileInfo
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000EA4F File Offset: 0x0000CC4F
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000EA57 File Offset: 0x0000CC57
		public long PathId { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000EA60 File Offset: 0x0000CC60
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000EA68 File Offset: 0x0000CC68
		public long ByteStart { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000EA71 File Offset: 0x0000CC71
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000EA79 File Offset: 0x0000CC79
		public uint ByteSize { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000EA82 File Offset: 0x0000CC82
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		public int TypeIdOrIndex { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000EA93 File Offset: 0x0000CC93
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000EA9B File Offset: 0x0000CC9B
		public ushort ClassId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		public ushort ScriptTypeIndex { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000EAB5 File Offset: 0x0000CCB5
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000EABD File Offset: 0x0000CCBD
		public byte Stripped { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000EAC6 File Offset: 0x0000CCC6
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000EACE File Offset: 0x0000CCCE
		public int TypeId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000EAD7 File Offset: 0x0000CCD7
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000EADF File Offset: 0x0000CCDF
		public long AbsoluteByteStart { get; set; }

		// Token: 0x06000174 RID: 372 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public static int GetSize(uint version)
		{
			int num = 0;
			num += 4;
			bool flag = version >= 14U;
			if (flag)
			{
				num += 4;
			}
			num += 12;
			bool flag2 = version >= 22U;
			if (flag2)
			{
				num += 4;
			}
			bool flag3 = version <= 15U;
			if (flag3)
			{
				num += 2;
			}
			bool flag4 = version <= 16U;
			if (flag4)
			{
				num += 2;
			}
			bool flag5 = 15U <= version && version <= 16U;
			if (flag5)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000EB64 File Offset: 0x0000CD64
		public void Read(AssetsFileReader reader, uint version)
		{
			reader.Align();
			bool flag = version >= 14U;
			if (flag)
			{
				this.PathId = reader.ReadInt64();
			}
			else
			{
				this.PathId = (long)((ulong)reader.ReadUInt32());
			}
			bool flag2 = version >= 22U;
			if (flag2)
			{
				this.ByteStart = reader.ReadInt64();
			}
			else
			{
				this.ByteStart = (long)((ulong)reader.ReadUInt32());
			}
			this.ByteSize = reader.ReadUInt32();
			this.TypeIdOrIndex = reader.ReadInt32();
			bool flag3 = version <= 15U;
			if (flag3)
			{
				this.ClassId = reader.ReadUInt16();
			}
			bool flag4 = version <= 16U;
			if (flag4)
			{
				this.ScriptTypeIndex = reader.ReadUInt16();
			}
			bool flag5 = 15U <= version && version <= 16U;
			if (flag5)
			{
				this.Stripped = reader.ReadByte();
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000EC48 File Offset: 0x0000CE48
		public void Write(AssetsFileWriter writer, uint version)
		{
			writer.Align();
			bool flag = version >= 14U;
			if (flag)
			{
				writer.Write(this.PathId);
			}
			else
			{
				writer.Write((uint)this.PathId);
			}
			bool flag2 = version >= 22U;
			if (flag2)
			{
				writer.Write(this.ByteStart);
			}
			else
			{
				writer.Write((uint)this.ByteStart);
			}
			writer.Write(this.ByteSize);
			writer.Write(this.TypeIdOrIndex);
			bool flag3 = version <= 15U;
			if (flag3)
			{
				writer.Write(this.ClassId);
			}
			bool flag4 = version <= 16U;
			if (flag4)
			{
				writer.Write(this.ScriptTypeIndex);
			}
			bool flag5 = 15U <= version && version <= 16U;
			if (flag5)
			{
				writer.Write(this.Stripped);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		public int GetTypeId(AssetsFile assetsFile)
		{
			return this.GetTypeId(assetsFile.Metadata.TypeTreeTypes, assetsFile.Header.Version);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public int GetTypeId(AssetsFileMetadata metadata, uint version)
		{
			return this.GetTypeId(metadata.TypeTreeTypes, version);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public int GetTypeId(List<TypeTreeType> typeTreeTypes, uint version)
		{
			bool flag = version < 16U;
			int num;
			if (flag)
			{
				num = this.TypeIdOrIndex;
			}
			else
			{
				bool flag2 = this.TypeIdOrIndex >= typeTreeTypes.Count;
				if (flag2)
				{
					throw new IndexOutOfRangeException("TypeIndex is larger than type tree count!");
				}
				num = typeTreeTypes[this.TypeIdOrIndex].TypeId;
			}
			return num;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		public long GetAbsoluteByteStart(AssetsFile assetsFile)
		{
			return assetsFile.Header.DataOffset + this.ByteStart;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		public long GetAbsoluteByteStart(AssetsFileHeader header)
		{
			return header.DataOffset + this.ByteStart;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000EE18 File Offset: 0x0000D018
		public long GetAbsoluteByteStart(long dataOffset)
		{
			return dataOffset + this.ByteStart;
		}
	}
}
