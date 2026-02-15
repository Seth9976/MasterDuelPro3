using System;
using System.Buffers.Binary;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000148 RID: 328
	public class EndianBinaryReader : BinaryReader
	{
		// Token: 0x0600039E RID: 926 RVA: 0x000149D1 File Offset: 0x00012BD1
		public EndianBinaryReader(Stream stream, EndianType endian = EndianType.BigEndian)
			: base(stream)
		{
			this.Endian = endian;
			this.buffer = new byte[8];
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000149ED File Offset: 0x00012BED
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x000149FA File Offset: 0x00012BFA
		public long Position
		{
			get
			{
				return this.BaseStream.Position;
			}
			set
			{
				this.BaseStream.Position = value;
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00014A08 File Offset: 0x00012C08
		public override short ReadInt16()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 2);
				return BinaryPrimitives.ReadInt16BigEndian(this.buffer);
			}
			return base.ReadInt16();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00014A39 File Offset: 0x00012C39
		public override int ReadInt32()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 4);
				return BinaryPrimitives.ReadInt32BigEndian(this.buffer);
			}
			return base.ReadInt32();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00014A6A File Offset: 0x00012C6A
		public override long ReadInt64()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 8);
				return BinaryPrimitives.ReadInt64BigEndian(this.buffer);
			}
			return base.ReadInt64();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00014A9B File Offset: 0x00012C9B
		public override ushort ReadUInt16()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 2);
				return BinaryPrimitives.ReadUInt16BigEndian(this.buffer);
			}
			return base.ReadUInt16();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014ACC File Offset: 0x00012CCC
		public override uint ReadUInt32()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 4);
				return BinaryPrimitives.ReadUInt32BigEndian(this.buffer);
			}
			return base.ReadUInt32();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00014AFD File Offset: 0x00012CFD
		public override ulong ReadUInt64()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 8);
				return BinaryPrimitives.ReadUInt64BigEndian(this.buffer);
			}
			return base.ReadUInt64();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00014B2E File Offset: 0x00012D2E
		public override float ReadSingle()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 4);
				Array.Reverse<byte>(this.buffer, 0, 4);
				return BitConverter.ToSingle(this.buffer, 0);
			}
			return base.ReadSingle();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00014B68 File Offset: 0x00012D68
		public override double ReadDouble()
		{
			if (this.Endian == EndianType.BigEndian)
			{
				this.Read(this.buffer, 0, 8);
				Array.Reverse<byte>(this.buffer);
				return BitConverter.ToDouble(this.buffer, 0);
			}
			return base.ReadDouble();
		}

		// Token: 0x0400091C RID: 2332
		private readonly byte[] buffer;

		// Token: 0x0400091D RID: 2333
		public EndianType Endian;
	}
}
