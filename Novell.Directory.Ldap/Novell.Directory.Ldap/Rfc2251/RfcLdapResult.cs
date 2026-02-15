using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000083 RID: 131
	public class RfcLdapResult : Asn1Sequence, RfcResponse
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00013A6A File Offset: 0x00011C6A
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage)
			: this(resultCode, matchedDN, errorMessage, null)
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00013A76 File Offset: 0x00011C76
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(4)
		{
			base.add(resultCode);
			base.add(matchedDN);
			base.add(errorMessage);
			if (referral != null)
			{
				base.add(referral);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00013AA0 File Offset: 0x00011CA0
		[CLSCompliant(false)]
		public RfcLdapResult(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(3);
				if (asn1Tagged.getIdentifier().Tag == 3)
				{
					sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
					MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
					base.set_Renamed(3, new RfcReferral(dec, memoryStream, array.Length));
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00011990 File Offset: 0x0000FB90
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001199E File Offset: 0x0000FB9E
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000119B6 File Offset: 0x0000FBB6
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001367C File Offset: 0x0001187C
		public RfcReferral getReferral()
		{
			if (base.size() <= 3)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(3);
		}

		// Token: 0x04000280 RID: 640
		public const int REFERRAL = 3;
	}
}
