using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000087 RID: 135
	internal class DnsQuery : DnsPacket
	{
		// Token: 0x0600021A RID: 538 RVA: 0x000086C0 File Offset: 0x000068C0
		public DnsQuery(string name, DnsQType qtype, DnsQClass qclass)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int num = DnsUtil.GetEncodedLength(name);
			if (num == -1)
			{
				throw new ArgumentException("Invalid DNS name", "name");
			}
			num += 16;
			this.packet = new byte[num];
			this.header = new DnsHeader(this.packet, 0);
			this.position = 12;
			base.WriteDnsName(name);
			base.WriteUInt16((ushort)qtype);
			base.WriteUInt16((ushort)qclass);
			base.Header.QuestionCount = 1;
			base.Header.IsQuery = true;
			base.Header.RecursionDesired = true;
		}
	}
}
