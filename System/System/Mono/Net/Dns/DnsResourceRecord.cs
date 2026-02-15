using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008A RID: 138
	internal class DnsResourceRecord
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000026E5 File Offset: 0x000008E5
		internal DnsResourceRecord()
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000087D4 File Offset: 0x000069D4
		internal void CopyFrom(DnsResourceRecord rr)
		{
			this.name = rr.name;
			this.type = rr.type;
			this.klass = rr.klass;
			this.ttl = rr.ttl;
			this.rdlength = rr.rdlength;
			this.m_rdata = rr.m_rdata;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000882C File Offset: 0x00006A2C
		internal static DnsResourceRecord CreateFromBuffer(DnsPacket packet, int size, ref int offset)
		{
			string text = packet.ReadName(ref offset);
			DnsType dnsType = (DnsType)packet.ReadUInt16(ref offset);
			DnsClass dnsClass = (DnsClass)packet.ReadUInt16(ref offset);
			int num = packet.ReadInt32(ref offset);
			ushort num2 = packet.ReadUInt16(ref offset);
			DnsResourceRecord dnsResourceRecord = new DnsResourceRecord();
			dnsResourceRecord.name = text;
			dnsResourceRecord.type = dnsType;
			dnsResourceRecord.klass = dnsClass;
			dnsResourceRecord.ttl = num;
			dnsResourceRecord.rdlength = num2;
			dnsResourceRecord.m_rdata = new ArraySegment<byte>(packet.Packet, offset, (int)num2);
			offset += (int)num2;
			if (dnsClass == DnsClass.Internet)
			{
				if (dnsType <= DnsType.CNAME)
				{
					if (dnsType != DnsType.A)
					{
						if (dnsType == DnsType.CNAME)
						{
							dnsResourceRecord = new DnsResourceRecordCName(dnsResourceRecord);
						}
					}
					else
					{
						dnsResourceRecord = new DnsResourceRecordA(dnsResourceRecord);
					}
				}
				else if (dnsType != DnsType.PTR)
				{
					if (dnsType == DnsType.AAAA)
					{
						dnsResourceRecord = new DnsResourceRecordAAAA(dnsResourceRecord);
					}
				}
				else
				{
					dnsResourceRecord = new DnsResourceRecordPTR(dnsResourceRecord);
				}
			}
			return dnsResourceRecord;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000088F9 File Offset: 0x00006AF9
		public DnsType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008901 File Offset: 0x00006B01
		public DnsClass Class
		{
			get
			{
				return this.klass;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008909 File Offset: 0x00006B09
		public ArraySegment<byte> Data
		{
			get
			{
				return this.m_rdata;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008914 File Offset: 0x00006B14
		public override string ToString()
		{
			return string.Format("Name: {0}, Type: {1}, Class: {2}, Ttl: {3}, Data length: {4}", new object[]
			{
				this.name,
				this.type,
				this.klass,
				this.ttl,
				this.Data.Count
			});
		}

		// Token: 0x040001F5 RID: 501
		private string name;

		// Token: 0x040001F6 RID: 502
		private DnsType type;

		// Token: 0x040001F7 RID: 503
		private DnsClass klass;

		// Token: 0x040001F8 RID: 504
		private int ttl;

		// Token: 0x040001F9 RID: 505
		private ushort rdlength;

		// Token: 0x040001FA RID: 506
		private ArraySegment<byte> m_rdata;
	}
}
