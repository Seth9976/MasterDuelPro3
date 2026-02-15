using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000E1 RID: 225
	public class ReferralAddress
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x000179B0 File Offset: 0x00015BB0
		public int AddressType
		{
			get
			{
				return this.address_type;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x000179B8 File Offset: 0x00015BB8
		public string Address
		{
			get
			{
				return this.strAddress;
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000179C0 File Offset: 0x00015BC0
		public ReferralAddress(Asn1Sequence dseObject)
		{
			this.address_type = ((Asn1Integer)dseObject.get_Renamed(0)).intValue();
			this.strAddress = ((Asn1OctetString)dseObject.get_Renamed(1)).stringValue();
		}

		// Token: 0x0400049C RID: 1180
		protected int address_type;

		// Token: 0x0400049D RID: 1181
		protected string strAddress;
	}
}
