using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000E8 RID: 232
	public class LdapSortResponse : LdapControl
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00018356 File Offset: 0x00016556
		public virtual string FailedAttribute
		{
			get
			{
				return this.failedAttribute;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001835E File Offset: 0x0001655E
		public virtual int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018368 File Offset: 0x00016568
		[CLSCompliant(false)]
		public LdapSortResponse(string oid, bool critical, sbyte[] values)
			: base(oid, critical, values)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object = lberdecoder.decode(values);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object2 = ((Asn1Sequence)asn1Object).get_Renamed(0);
			if (asn1Object2 != null && asn1Object2 is Asn1Enumerated)
			{
				this.resultCode = ((Asn1Enumerated)asn1Object2).intValue();
			}
			if (((Asn1Sequence)asn1Object).size() > 1)
			{
				Asn1Object asn1Object3 = ((Asn1Sequence)asn1Object).get_Renamed(1);
				if (asn1Object3 != null && asn1Object3 is Asn1OctetString)
				{
					this.failedAttribute = ((Asn1OctetString)asn1Object3).stringValue();
				}
			}
		}

		// Token: 0x040004C7 RID: 1223
		private string failedAttribute;

		// Token: 0x040004C8 RID: 1224
		private int resultCode;
	}
}
