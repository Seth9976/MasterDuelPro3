using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000084 RID: 132
	internal abstract class DnsPacket
	{
		// Token: 0x0600020D RID: 525 RVA: 0x000026E5 File Offset: 0x000008E5
		protected DnsPacket()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000845C File Offset: 0x0000665C
		protected DnsPacket(byte[] buffer, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", "Must be greater than zero.");
			}
			this.packet = buffer;
			this.position = length;
			this.header = new DnsHeader(new ArraySegment<byte>(this.packet, 0, 12));
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000084B8 File Offset: 0x000066B8
		public byte[] Packet
		{
			get
			{
				return this.packet;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000084C0 File Offset: 0x000066C0
		public int Length
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000084C8 File Offset: 0x000066C8
		public DnsHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000084D0 File Offset: 0x000066D0
		protected void WriteUInt16(ushort v)
		{
			byte[] array = this.packet;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)((v & 65280) >> 8);
			byte[] array2 = this.packet;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(v & 255);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008520 File Offset: 0x00006720
		protected void WriteStringBytes(string str, int offset, int count)
		{
			int num = offset;
			int i = 0;
			while (i < count)
			{
				byte[] array = this.packet;
				int num2 = this.position;
				this.position = num2 + 1;
				array[num2] = (byte)str[num];
				i++;
				num++;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008560 File Offset: 0x00006760
		protected void WriteLabel(string str, int offset, int count)
		{
			byte[] array = this.packet;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)count;
			this.WriteStringBytes(str, offset, count);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008590 File Offset: 0x00006790
		protected void WriteDnsName(string name)
		{
			if (!DnsUtil.IsValidDnsName(name))
			{
				throw new ArgumentException("Invalid DNS name");
			}
			if (!string.IsNullOrEmpty(name))
			{
				int length = name.Length;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < length; i++)
				{
					if (name[i] != '.')
					{
						num2++;
					}
					else
					{
						if (i == 0)
						{
							break;
						}
						this.WriteLabel(name, num, num2);
						num += num2 + 1;
						num2 = 0;
					}
				}
				if (num2 > 0)
				{
					this.WriteLabel(name, num, num2);
				}
			}
			byte[] array = this.packet;
			int num3 = this.position;
			this.position = num3 + 1;
			array[num3] = 0;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000861D File Offset: 0x0000681D
		protected internal string ReadName(ref int offset)
		{
			return DnsUtil.ReadName(this.packet, ref offset);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000862B File Offset: 0x0000682B
		protected internal static string ReadName(byte[] buffer, ref int offset)
		{
			return DnsUtil.ReadName(buffer, ref offset);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008634 File Offset: 0x00006834
		protected internal ushort ReadUInt16(ref int offset)
		{
			byte[] array = this.packet;
			int num = offset;
			offset = num + 1;
			ushort num2 = array[num] << 8;
			byte[] array2 = this.packet;
			num = offset;
			offset = num + 1;
			return num2 + array2[num];
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008668 File Offset: 0x00006868
		protected internal int ReadInt32(ref int offset)
		{
			byte[] array = this.packet;
			int num = offset;
			offset = num + 1;
			int num2 = array[num] << 24;
			byte[] array2 = this.packet;
			num = offset;
			offset = num + 1;
			int num3 = num2 + (array2[num] << 16);
			byte[] array3 = this.packet;
			num = offset;
			offset = num + 1;
			int num4 = num3 + (array3[num] << 8);
			byte[] array4 = this.packet;
			num = offset;
			offset = num + 1;
			return num4 + array4[num];
		}

		// Token: 0x04000189 RID: 393
		protected byte[] packet;

		// Token: 0x0400018A RID: 394
		protected int position;

		// Token: 0x0400018B RID: 395
		protected DnsHeader header;
	}
}
