using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000073 RID: 115
	public class RfcBindResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public virtual Asn1OctetString ServerSaslCreds
		{
			get
			{
				if (base.size() == 5)
				{
					return (Asn1OctetString)((Asn1Tagged)base.get_Renamed(4)).taggedValue();
				}
				if (base.size() == 4)
				{
					Asn1Object asn1Object = base.get_Renamed(3);
					if (asn1Object is Asn1Tagged)
					{
						return (Asn1OctetString)((Asn1Tagged)asn1Object).taggedValue();
					}
				}
				return null;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00011928 File Offset: 0x0000FB28
		[CLSCompliant(false)]
		public RfcBindResponse(Asn1Decoder dec, Stream in_Renamed, int len)
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

		// Token: 0x060003E1 RID: 993 RVA: 0x00011990 File Offset: 0x0000FB90
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001199E File Offset: 0x0000FB9E
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000119B6 File Offset: 0x0000FBB6
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000119D0 File Offset: 0x0000FBD0
		public RfcReferral getReferral()
		{
			if (base.size() > 3)
			{
				Asn1Object asn1Object = base.get_Renamed(3);
				if (asn1Object is RfcReferral)
				{
					return (RfcReferral)asn1Object;
				}
			}
			return null;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000119FE File Offset: 0x0000FBFE
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 1);
		}
	}
}
