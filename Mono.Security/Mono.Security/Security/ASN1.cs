using System;
using System.Collections;
using System.Text;

namespace Mono.Security
{
	// Token: 0x0200000B RID: 11
	public class ASN1
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public ASN1(byte tag)
			: this(tag, null)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BB3 File Offset: 0x00000DB3
		public ASN1(byte tag, byte[] data)
		{
			this.m_nTag = tag;
			this.m_aValue = data;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BCC File Offset: 0x00000DCC
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C73 File Offset: 0x00000E73
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C8A File Offset: 0x00000E8A
		public byte Tag
		{
			get
			{
				return this.m_nTag;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C92 File Offset: 0x00000E92
		public int Length
		{
			get
			{
				if (this.m_aValue != null)
				{
					return this.m_aValue.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002CA6 File Offset: 0x00000EA6
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002CC7 File Offset: 0x00000EC7
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
			set
			{
				if (value != null)
				{
					this.m_aValue = (byte[])value.Clone();
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002CE0 File Offset: 0x00000EE0
		private bool CompareArray(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D12 File Offset: 0x00000F12
		public bool CompareValue(byte[] value)
		{
			return this.CompareArray(this.m_aValue, value);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D21 File Offset: 0x00000F21
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

		// Token: 0x06000036 RID: 54 RVA: 0x00002D48 File Offset: 0x00000F48
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

		// Token: 0x06000037 RID: 55 RVA: 0x00002F64 File Offset: 0x00001164
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

		// Token: 0x06000038 RID: 56 RVA: 0x00002FB8 File Offset: 0x000011B8
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

		// Token: 0x17000007 RID: 7
		public ASN1 this[int index]
		{
			get
			{
				ASN1 asn;
				try
				{
					if (this.elist == null || index >= this.elist.Count)
					{
						asn = null;
					}
					else
					{
						asn = (ASN1)this.elist[index];
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					asn = null;
				}
				return asn;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003090 File Offset: 0x00001290
		public ASN1 Element(int index, byte anTag)
		{
			ASN1 asn;
			try
			{
				if (this.elist == null || index >= this.elist.Count)
				{
					asn = null;
				}
				else
				{
					ASN1 asn2 = (ASN1)this.elist[index];
					if (asn2.Tag == anTag)
					{
						asn = asn2;
					}
					else
					{
						asn = null;
					}
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				asn = null;
			}
			return asn;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000030F0 File Offset: 0x000012F0
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

		// Token: 0x04000012 RID: 18
		private byte m_nTag;

		// Token: 0x04000013 RID: 19
		private byte[] m_aValue;

		// Token: 0x04000014 RID: 20
		private ArrayList elist;
	}
}
