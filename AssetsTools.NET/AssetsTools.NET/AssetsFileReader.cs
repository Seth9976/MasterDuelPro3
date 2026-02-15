using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000070 RID: 112
	public class AssetsFileReader : BinaryReader
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00016B96 File Offset: 0x00014D96
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00016B9E File Offset: 0x00014D9E
		public bool BigEndian { get; set; } = false;

		// Token: 0x060003FB RID: 1019 RVA: 0x00016BA7 File Offset: 0x00014DA7
		public AssetsFileReader(string filePath)
			: base(File.OpenRead(filePath))
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00016BBE File Offset: 0x00014DBE
		public AssetsFileReader(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00016BD0 File Offset: 0x00014DD0
		public override short ReadInt16()
		{
			return this.BigEndian ? ((short)this.ReverseShort((ushort)base.ReadInt16())) : base.ReadInt16();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00016C04 File Offset: 0x00014E04
		public override ushort ReadUInt16()
		{
			return this.BigEndian ? this.ReverseShort(base.ReadUInt16()) : base.ReadUInt16();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016C34 File Offset: 0x00014E34
		public int ReadInt24()
		{
			return (int)(this.BigEndian ? this.ReverseInt((uint)BitConverter.ToInt32(this.ReadBytes(3).Concat(new byte[1]).ToArray<byte>(), 0)) : ((uint)BitConverter.ToInt32(this.ReadBytes(3).Concat(new byte[1]).ToArray<byte>(), 0)));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00016C94 File Offset: 0x00014E94
		public uint ReadUInt24()
		{
			return this.BigEndian ? this.ReverseInt(BitConverter.ToUInt32(this.ReadBytes(3).Concat(new byte[1]).ToArray<byte>(), 0)) : BitConverter.ToUInt32(this.ReadBytes(3).Concat(new byte[1]).ToArray<byte>(), 0);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00016CF4 File Offset: 0x00014EF4
		public override int ReadInt32()
		{
			return (int)(this.BigEndian ? this.ReverseInt((uint)base.ReadInt32()) : ((uint)base.ReadInt32()));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00016D24 File Offset: 0x00014F24
		public override uint ReadUInt32()
		{
			return this.BigEndian ? this.ReverseInt(base.ReadUInt32()) : base.ReadUInt32();
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00016D54 File Offset: 0x00014F54
		public override long ReadInt64()
		{
			return (long)(this.BigEndian ? this.ReverseLong((ulong)base.ReadInt64()) : ((ulong)base.ReadInt64()));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00016D84 File Offset: 0x00014F84
		public override ulong ReadUInt64()
		{
			return this.BigEndian ? this.ReverseLong(base.ReadUInt64()) : base.ReadUInt64();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00016DB4 File Offset: 0x00014FB4
		public ushort ReverseShort(ushort value)
		{
			return (ushort)(((value & 65280) >> 8) | ((int)(value & 255) << 8));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00016DDC File Offset: 0x00014FDC
		public uint ReverseInt(uint value)
		{
			value = (value >> 16) | (value << 16);
			return ((value & 4278255360U) >> 8) | ((value & 16711935U) << 8);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00016E0C File Offset: 0x0001500C
		public ulong ReverseLong(ulong value)
		{
			value = (value >> 32) | (value << 32);
			value = ((value & 18446462603027742720UL) >> 16) | ((value & 281470681808895UL) << 16);
			return ((value & 18374966859414961920UL) >> 8) | ((value & 71777214294589695UL) << 8);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00016E64 File Offset: 0x00015064
		public void Align()
		{
			long num = 4L - this.BaseStream.Position % 4L;
			bool flag = num != 4L;
			if (flag)
			{
				this.BaseStream.Position += num;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00016EA4 File Offset: 0x000150A4
		public void Align8()
		{
			long num = 8L - this.BaseStream.Position % 8L;
			bool flag = num != 8L;
			if (flag)
			{
				this.BaseStream.Position += num;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00016EE4 File Offset: 0x000150E4
		public void Align16()
		{
			long num = 16L - this.BaseStream.Position % 16L;
			bool flag = num != 16L;
			if (flag)
			{
				this.BaseStream.Position += num;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00016F28 File Offset: 0x00015128
		public string ReadStringLength(int len)
		{
			return Encoding.UTF8.GetString(this.ReadBytes(len));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00016F4C File Offset: 0x0001514C
		public string ReadNullTerminated()
		{
			string text = "";
			char c;
			while ((c = this.ReadChar()) > '\0')
			{
				text += c.ToString();
			}
			return text;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00016F88 File Offset: 0x00015188
		public static string ReadNullTerminatedArray(byte[] bytes, uint pos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char c;
			while ((c = (char)bytes[(int)pos]) > '\0')
			{
				stringBuilder.Append(c);
				pos += 1U;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00016FC4 File Offset: 0x000151C4
		public string ReadCountString()
		{
			byte b = this.ReadByte();
			return this.ReadStringLength((int)b);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00016FE4 File Offset: 0x000151E4
		public string ReadCountStringInt16()
		{
			ushort num = this.ReadUInt16();
			return this.ReadStringLength((int)num);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00017004 File Offset: 0x00015204
		public string ReadCountStringInt32()
		{
			int num = this.ReadInt32();
			return this.ReadStringLength(num);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00017024 File Offset: 0x00015224
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00017041 File Offset: 0x00015241
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
	}
}
