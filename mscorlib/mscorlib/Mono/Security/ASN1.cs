using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Mono.Security
{
	// Token: 0x02000063 RID: 99
	[DefaultMember("Item")]
	internal class ASN1
	{
		// Token: 0x06000138 RID: 312 RVA: 0x0000785C File Offset: 0x00005A5C
		public ASN1(byte tag)
			: this(tag, null)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007866 File Offset: 0x00005A66
		public ASN1(byte tag, byte[] data)
		{
			this.m_nTag = tag;
			this.m_aValue = data;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000787C File Offset: 0x00005A7C
		public ASN1(byte[] data)
		{
			this.m_nTag = data[0];
			int num = 0;
			int num2 = (int)data[1];
			if (num2 > 128)
			{
				num = num2 - 128;
				num2 = 0;
				for (int i = 0; i < num; i++)
				{
					num2 *= 256;
					num2 += (int)data[i + 2];
				}
			}
			else if (num2 == 128)
			{
				throw new NotSupportedException("Undefined length encoding.");
			}
			this.m_aValue = new byte[num2];
			Buffer.BlockCopy(data, 2 + num, this.m_aValue, 0, num2);
			if ((this.m_nTag & 32) == 32)
			{
				int num3 = 0;
				this.Decode(this.m_aValue, ref num3, this.m_aValue.Length);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007923 File Offset: 0x00005B23
		public int Count
		{
			get
			{
				if (this.elist == null)
				{
					return 0;
				}
				return this.elist.Count;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000793A File Offset: 0x00005B3A
		public byte[] Value
		{
			get
			{
				if (this.m_aValue == null)
				{
					this.GetBytes();
				}
				return (byte[])this.m_aValue.Clone();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000795B File Offset: 0x00005B5B
		public ASN1 Add(ASN1 asn1)
		{
			if (asn1 != null)
			{
				if (this.elist == null)
				{
					this.elist = new ArrayList();
				}
				this.elist.Add(asn1);
			}
			return asn1;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007984 File Offset: 0x00005B84
		public virtual byte[] GetBytes()
		{
			byte[] array = null;
			if (this.Count > 0)
			{
				int num = 0;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.elist)
				{
					byte[] bytes = ((ASN1)obj).GetBytes();
					arrayList.Add(bytes);
					num += bytes.Length;
				}
				array = new byte[num];
				int num2 = 0;
				for (int i = 0; i < this.elist.Count; i++)
				{
					byte[] array2 = (byte[])arrayList[i];
					Buffer.BlockCopy(array2, 0, array, num2, array2.Length);
					num2 += array2.Length;
				}
			}
			else if (this.m_aValue != null)
			{
				array = this.m_aValue;
			}
			int num3 = 0;
			byte[] array3;
			if (array != null)
			{
				int num4 = array.Length;
				if (num4 > 127)
				{
					if (num4 <= 255)
					{
						array3 = new byte[3 + num4];
						Buffer.BlockCopy(array, 0, array3, 3, num4);
						num3 = 129;
						array3[2] = (byte)num4;
					}
					else if (num4 <= 65535)
					{
						array3 = new byte[4 + num4];
						Buffer.BlockCopy(array, 0, array3, 4, num4);
						num3 = 130;
						array3[2] = (byte)(num4 >> 8);
						array3[3] = (byte)num4;
					}
					else if (num4 <= 16777215)
					{
						array3 = new byte[5 + num4];
						Buffer.BlockCopy(array, 0, array3, 5, num4);
						num3 = 131;
						array3[2] = (byte)(num4 >> 16);
						array3[3] = (byte)(num4 >> 8);
						array3[4] = (byte)num4;
					}
					else
					{
						array3 = new byte[6 + num4];
						Buffer.BlockCopy(array, 0, array3, 6, num4);
						num3 = 132;
						array3[2] = (byte)(num4 >> 24);
						array3[3] = (byte)(num4 >> 16);
						array3[4] = (byte)(num4 >> 8);
						array3[5] = (byte)num4;
					}
				}
				else
				{
					array3 = new byte[2 + num4];
					Buffer.BlockCopy(array, 0, array3, 2, num4);
					num3 = num4;
				}
				if (this.m_aValue == null)
				{
					this.m_aValue = array;
				}
			}
			else
			{
				array3 = new byte[2];
			}
			array3[0] = this.m_nTag;
			array3[1] = (byte)num3;
			return array3;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007BA0 File Offset: 0x00005DA0
		protected void Decode(byte[] asn1, ref int anPos, int anLength)
		{
			while (anPos < anLength - 1)
			{
				byte b;
				int num;
				byte[] array;
				this.DecodeTLV(asn1, ref anPos, out b, out num, out array);
				if (b != 0)
				{
					ASN1 asn2 = this.Add(new ASN1(b, array));
					if ((b & 32) == 32)
					{
						int num2 = anPos;
						asn2.Decode(asn1, ref num2, num2 + num);
					}
					anPos += num;
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007BF4 File Offset: 0x00005DF4
		protected void DecodeTLV(byte[] asn1, ref int pos, out byte tag, out int length, out byte[] content)
		{
			int num = pos;
			pos = num + 1;
			tag = asn1[num];
			num = pos;
			pos = num + 1;
			length = (int)asn1[num];
			if ((length & 128) == 128)
			{
				int num2 = length & 127;
				length = 0;
				for (int i = 0; i < num2; i++)
				{
					int num3 = length * 256;
					num = pos;
					pos = num + 1;
					length = num3 + (int)asn1[num];
				}
			}
			content = new byte[length];
			Buffer.BlockCopy(asn1, pos, content, 0, length);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007C78 File Offset: 0x00005E78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Tag: {0} {1}", this.m_nTag.ToString("X2"), Environment.NewLine);
			stringBuilder.AppendFormat("Length: {0} {1}", this.Value.Length, Environment.NewLine);
			stringBuilder.Append("Value: ");
			stringBuilder.Append(Environment.NewLine);
			for (int i = 0; i < this.Value.Length; i++)
			{
				stringBuilder.AppendFormat("{0} ", this.Value[i].ToString("X2"));
				if ((i + 1) % 16 == 0)
				{
					stringBuilder.AppendFormat(Environment.NewLine, Array.Empty<object>());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001BF RID: 447
		private byte m_nTag;

		// Token: 0x040001C0 RID: 448
		private byte[] m_aValue;

		// Token: 0x040001C1 RID: 449
		private ArrayList elist;
	}
}
