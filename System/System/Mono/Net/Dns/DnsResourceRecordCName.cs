using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008D RID: 141
	internal class DnsResourceRecordCName : DnsResourceRecord
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00008990 File Offset: 0x00006B90
		internal DnsResourceRecordCName(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.cname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000089D4 File Offset: 0x00006BD4
		public string CName
		{
			get
			{
				return this.cname;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000089DC File Offset: 0x00006BDC
		public override string ToString()
		{
			return base.ToString() + " CNAME: " + this.cname.ToString();
		}

		// Token: 0x040001FB RID: 507
		private string cname;
	}
}
