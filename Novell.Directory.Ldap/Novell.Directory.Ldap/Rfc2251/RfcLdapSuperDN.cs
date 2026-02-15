using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000085 RID: 133
	public class RfcLdapSuperDN : Asn1Tagged
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00013B14 File Offset: 0x00011D14
		public RfcLdapSuperDN(string s)
			: base(RfcLdapSuperDN.ID, new Asn1OctetString(s), false)
		{
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(s));
				this.content = array;
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00013B70 File Offset: 0x00011D70
		[CLSCompliant(false)]
		public RfcLdapSuperDN(sbyte[] ba)
			: base(RfcLdapSuperDN.ID, new Asn1OctetString(ba), false)
		{
			this.content = ba;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013B8B File Offset: 0x00011D8B
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00013B95 File Offset: 0x00011D95
		[CLSCompliant(false)]
		public sbyte[] byteValue()
		{
			return this.content;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00013BA0 File Offset: 0x00011DA0
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

		// Token: 0x06000464 RID: 1124 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public override string ToString()
		{
			return base.ToString() + " " + this.stringValue();
		}

		// Token: 0x04000281 RID: 641
		private sbyte[] content;

		// Token: 0x04000282 RID: 642
		public static readonly int TAG = 0;

		// Token: 0x04000283 RID: 643
		protected static readonly Asn1Identifier ID = new Asn1Identifier(2, false, RfcLdapSuperDN.TAG);
	}
}
