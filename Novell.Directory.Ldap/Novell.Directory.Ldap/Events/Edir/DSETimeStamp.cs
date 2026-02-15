using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000C9 RID: 201
	public class DSETimeStamp
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000160E5 File Offset: 0x000142E5
		public int Seconds
		{
			get
			{
				return this.nSeconds;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000160ED File Offset: 0x000142ED
		public int ReplicaNumber
		{
			get
			{
				return this.replica_number;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000160F5 File Offset: 0x000142F5
		public int Event
		{
			get
			{
				return this.nEvent;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00016100 File Offset: 0x00014300
		public DSETimeStamp(Asn1Sequence dseObject)
		{
			this.nSeconds = ((Asn1Integer)dseObject.get_Renamed(0)).intValue();
			this.replica_number = ((Asn1Integer)dseObject.get_Renamed(1)).intValue();
			this.nEvent = ((Asn1Integer)dseObject.get_Renamed(2)).intValue();
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00016158 File Offset: 0x00014358
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[TimeStamp (seconds={0})", this.nSeconds);
			stringBuilder.AppendFormat("(replicaNumber={0})", this.replica_number);
			stringBuilder.AppendFormat("(event={0})", this.nEvent);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000361 RID: 865
		protected int nSeconds;

		// Token: 0x04000362 RID: 866
		protected int replica_number;

		// Token: 0x04000363 RID: 867
		protected int nEvent;
	}
}
