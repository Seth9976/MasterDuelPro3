using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000E4 RID: 228
	public class LdapEntryChangeControl : LdapControl
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00017E22 File Offset: 0x00016022
		public virtual bool HasChangeNumber
		{
			get
			{
				return this.m_hasChangeNumber;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00017E2A File Offset: 0x0001602A
		public virtual int ChangeNumber
		{
			get
			{
				return this.m_changeNumber;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00017E32 File Offset: 0x00016032
		public virtual int ChangeType
		{
			get
			{
				return this.m_changeType;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00017E3A File Offset: 0x0001603A
		public virtual string PreviousDN
		{
			get
			{
				return this.m_previousDN;
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00017E44 File Offset: 0x00016044
		[CLSCompliant(false)]
		public LdapEntryChangeControl(string oid, bool critical, sbyte[] value_Renamed)
			: base(oid, critical, value_Renamed)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error.");
			}
			Asn1Object asn1Object = lberdecoder.decode(value_Renamed);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error.");
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)asn1Object;
			Asn1Object asn1Object2 = asn1Sequence.get_Renamed(0);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Enumerated))
			{
				throw new IOException("Decoding error.");
			}
			this.m_changeType = ((Asn1Enumerated)asn1Object2).intValue();
			if (asn1Sequence.size() > 1 && this.m_changeType == 8)
			{
				asn1Object2 = asn1Sequence.get_Renamed(1);
				if (asn1Object2 == null || !(asn1Object2 is Asn1OctetString))
				{
					throw new IOException("Decoding error get previous DN");
				}
				this.m_previousDN = ((Asn1OctetString)asn1Object2).stringValue();
			}
			else
			{
				this.m_previousDN = "";
			}
			if (asn1Sequence.size() != 3)
			{
				this.m_hasChangeNumber = false;
				return;
			}
			asn1Object2 = asn1Sequence.get_Renamed(2);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Integer))
			{
				throw new IOException("Decoding error getting change number");
			}
			this.m_changeNumber = ((Asn1Integer)asn1Object2).intValue();
			this.m_hasChangeNumber = true;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00017F55 File Offset: 0x00016155
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040004AC RID: 1196
		private int m_changeType;

		// Token: 0x040004AD RID: 1197
		private string m_previousDN;

		// Token: 0x040004AE RID: 1198
		private bool m_hasChangeNumber;

		// Token: 0x040004AF RID: 1199
		private int m_changeNumber;
	}
}
