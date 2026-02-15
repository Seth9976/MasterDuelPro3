using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007B RID: 123
	public class RfcExtendedResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00011CC5 File Offset: 0x0000FEC5
		public virtual RfcLdapOID ResponseName
		{
			get
			{
				if (this.responseNameIndex == 0)
				{
					return null;
				}
				return (RfcLdapOID)base.get_Renamed(this.responseNameIndex);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00011CE2 File Offset: 0x0000FEE2
		[CLSCompliant(false)]
		public virtual Asn1OctetString Response
		{
			get
			{
				if (this.responseIndex == 0)
				{
					return null;
				}
				return (Asn1OctetString)base.get_Renamed(this.responseIndex);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00011D00 File Offset: 0x0000FF00
		[CLSCompliant(false)]
		public RfcExtendedResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				for (int i = 3; i < base.size(); i++)
				{
					Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
					int tag = asn1Tagged.getIdentifier().Tag;
					if (tag != 3)
					{
						if (tag != 10)
						{
							if (tag == 11)
							{
								base.set_Renamed(i, asn1Tagged.taggedValue());
								this.responseIndex = i;
							}
						}
						else
						{
							base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
							this.responseNameIndex = i;
						}
					}
					else
					{
						sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
						MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
						base.set_Renamed(i, new RfcReferral(dec, memoryStream, array.Length));
						this.referralIndex = i;
					}
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00011990 File Offset: 0x0000FB90
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001199E File Offset: 0x0000FB9E
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000119B6 File Offset: 0x0000FBB6
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		public RfcReferral getReferral()
		{
			if (this.referralIndex == 0)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(this.referralIndex);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00011DF3 File Offset: 0x0000FFF3
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 24);
		}

		// Token: 0x04000259 RID: 601
		public const int RESPONSE_NAME = 10;

		// Token: 0x0400025A RID: 602
		public const int RESPONSE = 11;

		// Token: 0x0400025B RID: 603
		private int referralIndex;

		// Token: 0x0400025C RID: 604
		private int responseNameIndex;

		// Token: 0x0400025D RID: 605
		private int responseIndex;
	}
}
