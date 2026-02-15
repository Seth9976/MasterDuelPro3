using System;
using System.IO;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000071 RID: 113
	public class AssetsFileWriter : BinaryWriter
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00017051 File Offset: 0x00015251
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00017059 File Offset: 0x00015259
		public bool BigEndian { get; set; } = false;

		// Token: 0x06000415 RID: 1045 RVA: 0x00017062 File Offset: 0x00015262
		public AssetsFileWriter(string filePath)
			: base(File.Open(filePath, FileMode.Create, FileAccess.Write))
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001707B File Offset: 0x0001527B
		public AssetsFileWriter(FileStream fileStream)
			: base(fileStream)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001707B File Offset: 0x0001527B
		public AssetsFileWriter(MemoryStream memoryStream)
			: base(memoryStream)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001707B File Offset: 0x0001527B
		public AssetsFileWriter(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00017090 File Offset: 0x00015290
		public override void Write(short val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write((short)this.ReverseShort((ushort)val));
			}
			else
			{
				base.Write(val);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000170C4 File Offset: 0x000152C4
		public override void Write(ushort val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write(this.ReverseShort(val));
			}
			else
			{
				base.Write(val);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000170F8 File Offset: 0x000152F8
		public override void Write(int val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write((int)this.ReverseInt((uint)val));
			}
			else
			{
				base.Write(val);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001712C File Offset: 0x0001532C
		public override void Write(uint val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write(this.ReverseInt(val));
			}
			else
			{
				base.Write(val);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00017160 File Offset: 0x00015360
		public override void Write(long val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write((long)this.ReverseLong((ulong)val));
			}
			else
			{
				base.Write(val);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00017194 File Offset: 0x00015394
		public override void Write(ulong val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write(this.ReverseLong(val));
			}
			else
			{
				base.Write(val);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000171C6 File Offset: 0x000153C6
		public void WriteRawString(string val)
		{
			base.Write(Encoding.UTF8.GetBytes(val));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000171DC File Offset: 0x000153DC
		public void WriteUInt24(uint val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write(BitConverter.GetBytes(this.ReverseInt(val)), 1, 3);
			}
			else
			{
				base.Write(BitConverter.GetBytes(val), 0, 3);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001721C File Offset: 0x0001541C
		public void WriteInt24(int val)
		{
			bool bigEndian = this.BigEndian;
			if (bigEndian)
			{
				base.Write(BitConverter.GetBytes((int)this.ReverseInt((uint)val)), 1, 3);
			}
			else
			{
				base.Write(BitConverter.GetBytes(val), 0, 3);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001725C File Offset: 0x0001545C
		public ushort ReverseShort(ushort value)
		{
			return (ushort)(((value & 65280) >> 8) | ((int)(value & 255) << 8));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00017284 File Offset: 0x00015484
		public uint ReverseInt(uint value)
		{
			value = (value >> 16) | (value << 16);
			return ((value & 4278255360U) >> 8) | ((value & 16711935U) << 8);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000172B4 File Offset: 0x000154B4
		public ulong ReverseLong(ulong value)
		{
			value = (value >> 32) | (value << 32);
			value = ((value & 18446462603027742720UL) >> 16) | ((value & 281470681808895UL) << 16);
			return ((value & 18374966859414961920UL) >> 8) | ((value & 71777214294589695UL) << 8);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001730C File Offset: 0x0001550C
		public void Align()
		{
			while (this.BaseStream.Position % 4L != 0L)
			{
				this.Write(0);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001733C File Offset: 0x0001553C
		public void Align8()
		{
			while (this.BaseStream.Position % 8L != 0L)
			{
				this.Write(0);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001736C File Offset: 0x0001556C
		public void Align16()
		{
			while (this.BaseStream.Position % 16L != 0L)
			{
				this.Write(0);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001739B File Offset: 0x0001559B
		public void WriteNullTerminated(string text)
		{
			this.WriteRawString(text);
			this.Write(0);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000173B0 File Offset: 0x000155B0
		public void WriteCountString(string text)
		{
			bool flag = Encoding.UTF8.GetByteCount(text) > 255;
			if (flag)
			{
				new Exception("String is longer than 255! Use the Int32 variant instead!");
			}
			this.Write((byte)Encoding.UTF8.GetByteCount(text));
			this.WriteRawString(text);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000173FC File Offset: 0x000155FC
		public void WriteCountStringInt16(string text)
		{
			bool flag = Encoding.UTF8.GetByteCount(text) > 65535;
			if (flag)
			{
				new Exception("String is longer than 65535! Use the Int32 variant instead!");
			}
			this.Write((ushort)Encoding.UTF8.GetByteCount(text));
			this.WriteRawString(text);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00017446 File Offset: 0x00015646
		public void WriteCountStringInt32(string text)
		{
			this.Write(Encoding.UTF8.GetByteCount(text));
			this.WriteRawString(text);
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00017464 File Offset: 0x00015664
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00017481 File Offset: 0x00015681
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
