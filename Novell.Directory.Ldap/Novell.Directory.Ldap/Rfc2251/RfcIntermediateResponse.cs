using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007F RID: 127
	public class RfcIntermediateResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0001357C File Offset: 0x0001177C
		[CLSCompliant(false)]
		public RfcIntermediateResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			this.m_responseNameIndex = (this.m_responseValueIndex = 0);
			int i;
			if (base.size() >= 3)
			{
				i = 3;
			}
			else
			{
				i = 0;
			}
			while (i < base.size())
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
				int tag = asn1Tagged.getIdentifier().Tag;
				if (tag != 0)
				{
					if (tag == 1)
					{
						base.set_Renamed(i, asn1Tagged.taggedValue());
						this.m_responseValueIndex = i;
					}
				}
				else
				{
					base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
					this.m_responseNameIndex = i;
				}
				i++;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001361D File Offset: 0x0001181D
		public Asn1Enumerated getResultCode()
		{
			if (base.size() > 3)
			{
				return (Asn1Enumerated)base.get_Renamed(0);
			}
			return null;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013636 File Offset: 0x00011836
		public RfcLdapDN getMatchedDN()
		{
			if (base.size() > 3)
			{
				return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
			}
			return null;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013659 File Offset: 0x00011859
		public RfcLdapString getErrorMessage()
		{
			if (base.size() > 3)
			{
				return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
			}
			return null;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001367C File Offset: 0x0001187C
		public RfcReferral getReferral()
		{
			if (base.size() <= 3)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(3);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013695 File Offset: 0x00011895
		public RfcLdapOID getResponseName()
		{
			if (this.m_responseNameIndex < 0)
			{
				return null;
			}
			return (RfcLdapOID)base.get_Renamed(this.m_responseNameIndex);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000136B3 File Offset: 0x000118B3
		public Asn1OctetString getResponse()
		{
			if (this.m_responseValueIndex == 0)
			{
				return null;
			}
			return (Asn1OctetString)base.get_Renamed(this.m_responseValueIndex);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000136D0 File Offset: 0x000118D0
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 25);
		}

		// Token: 0x04000278 RID: 632
		public const int TAG_RESPONSE_NAME = 0;

		// Token: 0x04000279 RID: 633
		public const int TAG_RESPONSE = 1;

		// Token: 0x0400027A RID: 634
		private int m_referralIndex;

		// Token: 0x0400027B RID: 635
		private int m_responseNameIndex;

		// Token: 0x0400027C RID: 636
		private int m_responseValueIndex;
	}
}
