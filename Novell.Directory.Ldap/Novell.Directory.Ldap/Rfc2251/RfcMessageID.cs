using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000088 RID: 136
	internal class RfcMessageID : Asn1Integer
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00013CAC File Offset: 0x00011EAC
		private static int MessageID
		{
			get
			{
				object obj = RfcMessageID.lock_Renamed;
				int num;
				lock (obj)
				{
					num = ((RfcMessageID.messageID < int.MaxValue) ? (++RfcMessageID.messageID) : (RfcMessageID.messageID = 1));
				}
				return num;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013D0C File Offset: 0x00011F0C
		protected internal RfcMessageID()
			: base(RfcMessageID.MessageID)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013D19 File Offset: 0x00011F19
		protected internal RfcMessageID(int i)
			: base(i)
		{
		}

		// Token: 0x04000284 RID: 644
		private static int messageID;

		// Token: 0x04000285 RID: 645
		private static object lock_Renamed = new object();
	}
}
