using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000FD RID: 253
	public class LBERDecoder : Asn1Decoder, ISerializable
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00019430 File Offset: 0x00017630
		public LBERDecoder()
		{
			this.InitBlock();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00002A00 File Offset: 0x00000C00
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001943E File Offset: 0x0001763E
		private void InitBlock()
		{
			this.asn1ID = new Asn1Identifier();
			this.asn1Len = new Asn1Length();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019458 File Offset: 0x00017658
		[CLSCompliant(false)]
		public virtual Asn1Object decode(sbyte[] value_Renamed)
		{
			Asn1Object asn1Object = null;
			MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(value_Renamed));
			try
			{
				asn1Object = this.decode(memoryStream);
			}
			catch (IOException)
			{
			}
			return asn1Object;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019494 File Offset: 0x00017694
		public virtual Asn1Object decode(Stream in_Renamed)
		{
			int[] array = new int[1];
			return this.decode(in_Renamed, array);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000194B0 File Offset: 0x000176B0
		public virtual Asn1Object decode(Stream in_Renamed, int[] len)
		{
			this.asn1ID.reset(in_Renamed);
			this.asn1Len.reset(in_Renamed);
			int length = this.asn1Len.Length;
			len[0] = this.asn1ID.EncodedLength + this.asn1Len.EncodedLength + length;
			if (this.asn1ID.Universal)
			{
				int tag = this.asn1ID.Tag;
				if (tag <= 10)
				{
					switch (tag)
					{
					case 1:
						return new Asn1Boolean(this, in_Renamed, length);
					case 2:
						return new Asn1Integer(this, in_Renamed, length);
					case 3:
						break;
					case 4:
						return new Asn1OctetString(this, in_Renamed, length);
					case 5:
						return new Asn1Null();
					default:
						if (tag == 10)
						{
							return new Asn1Enumerated(this, in_Renamed, length);
						}
						break;
					}
				}
				else
				{
					if (tag == 16)
					{
						return new Asn1Sequence(this, in_Renamed, length);
					}
					if (tag == 17)
					{
						return new Asn1Set(this, in_Renamed, length);
					}
				}
				throw new EndOfStreamException("Unknown tag");
			}
			return new Asn1Tagged(this, in_Renamed, length, (Asn1Identifier)this.asn1ID.Clone());
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000195AC File Offset: 0x000177AC
		public object decodeBoolean(Stream in_Renamed, int len)
		{
			sbyte[] array = new sbyte[len];
			if (SupportClass.ReadInput(in_Renamed, ref array, 0, array.Length) != len)
			{
				throw new EndOfStreamException("LBER: BOOLEAN: decode error: EOF");
			}
			return array[0] != 0;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000195E8 File Offset: 0x000177E8
		public object decodeNumeric(Stream in_Renamed, int len)
		{
			long num = 0L;
			int num2 = in_Renamed.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("LBER: NUMERIC: decode error: EOF");
			}
			if ((num2 & 128) != 0)
			{
				num = -1L;
			}
			num = (num << 8) | (long)num2;
			for (int i = 1; i < len; i++)
			{
				num2 = in_Renamed.ReadByte();
				if (num2 < 0)
				{
					throw new EndOfStreamException("LBER: NUMERIC: decode error: EOF");
				}
				num = (num << 8) | (long)num2;
			}
			return num;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00019650 File Offset: 0x00017850
		public object decodeOctetString(Stream in_Renamed, int len)
		{
			sbyte[] array = new sbyte[len];
			int num;
			for (int i = 0; i < len; i += num)
			{
				num = SupportClass.ReadInput(in_Renamed, ref array, i, len - i);
			}
			return array;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00019680 File Offset: 0x00017880
		public object decodeCharacterString(Stream in_Renamed, int len)
		{
			sbyte[] array = new sbyte[len];
			for (int i = 0; i < len; i++)
			{
				int num = in_Renamed.ReadByte();
				if (num == -1)
				{
					throw new EndOfStreamException("LBER: CHARACTER STRING: decode error: EOF");
				}
				array[i] = (sbyte)num;
			}
			return new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(array)));
		}

		// Token: 0x040004FD RID: 1277
		private Asn1Identifier asn1ID;

		// Token: 0x040004FE RID: 1278
		private Asn1Length asn1Len;
	}
}
