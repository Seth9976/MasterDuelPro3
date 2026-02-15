using System;
using System.Net;

namespace Mono.Net.Dns
{
	// Token: 0x0200008E RID: 142
	internal abstract class DnsResourceRecordIPAddress : DnsResourceRecord
	{
		// Token: 0x0600022D RID: 557 RVA: 0x000089FC File Offset: 0x00006BFC
		internal DnsResourceRecordIPAddress(DnsResourceRecord rr, int address_size)
		{
			base.CopyFrom(rr);
			ArraySegment<byte> data = rr.Data;
			byte[] array = new byte[address_size];
			Buffer.BlockCopy(data.Array, data.Offset, array, 0, address_size);
			this.address = new IPAddress(array);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008A46 File Offset: 0x00006C46
		public override string ToString()
		{
			string text = base.ToString();
			string text2 = " Address: ";
			IPAddress ipaddress = this.address;
			return text + text2 + ((ipaddress != null) ? ipaddress.ToString() : null);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00008A6A File Offset: 0x00006C6A
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x040001FC RID: 508
		private IPAddress address;
	}
}
