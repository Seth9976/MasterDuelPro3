using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000F6 RID: 246
	public class Asn1OctetString : Asn1Object
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00018EFD File Offset: 0x000170FD
		[CLSCompliant(false)]
		public Asn1OctetString(sbyte[] content)
			: base(Asn1OctetString.ID)
		{
			this.content = content;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00018F14 File Offset: 0x00017114
		public Asn1OctetString(string content)
			: base(Asn1OctetString.ID)
		{
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(content));
				this.content = array;
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00018F68 File Offset: 0x00017168
		[CLSCompliant(false)]
		public Asn1OctetString(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1OctetString.ID)
		{
			this.content = ((len > 0) ? ((sbyte[])dec.decodeOctetString(in_Renamed, len)) : new sbyte[0]);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00018F94 File Offset: 0x00017194
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00018F9E File Offset: 0x0001719E
		[CLSCompliant(false)]
		public sbyte[] byteValue()
		{
			return this.content;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00018FA8 File Offset: 0x000171A8
		public string stringValue()
		{
			string text = null;
			try
			{
				text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(this.content)));
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
			return text;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00018FF8 File Offset: 0x000171F8
		public override string ToString()
		{
			return base.ToString() + "OCTET STRING: " + this.stringValue();
		}

		// Token: 0x040004EE RID: 1262
		private sbyte[] content;

		// Token: 0x040004EF RID: 1263
		public const int TAG = 4;

		// Token: 0x040004F0 RID: 1264
		protected internal static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 4);
	}
}
