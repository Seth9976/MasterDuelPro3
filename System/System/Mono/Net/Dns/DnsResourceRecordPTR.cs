using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008F RID: 143
	internal class DnsResourceRecordPTR : DnsResourceRecord
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00008A74 File Offset: 0x00006C74
		internal DnsResourceRecordPTR(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.dname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public string DName
		{
			get
			{
				return this.dname;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public override string ToString()
		{
			return base.ToString() + " DNAME: " + this.dname.ToString();
		}

		// Token: 0x040001FD RID: 509
		private string dname;
	}
}
