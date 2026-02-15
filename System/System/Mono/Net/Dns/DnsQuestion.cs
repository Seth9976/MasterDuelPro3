using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000088 RID: 136
	internal class DnsQuestion
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000026E5 File Offset: 0x000008E5
		internal DnsQuestion()
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008765 File Offset: 0x00006965
		internal int Init(DnsPacket packet, int offset)
		{
			this.name = packet.ReadName(ref offset);
			this.type = (DnsQType)packet.ReadUInt16(ref offset);
			this._class = (DnsQClass)packet.ReadUInt16(ref offset);
			return offset;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008792 File Offset: 0x00006992
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000879A File Offset: 0x0000699A
		public DnsQType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000087A2 File Offset: 0x000069A2
		public DnsQClass Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000087AA File Offset: 0x000069AA
		public override string ToString()
		{
			return string.Format("Name: {0} Type: {1} Class: {2}", this.Name, this.Type, this.Class);
		}

		// Token: 0x040001DE RID: 478
		private string name;

		// Token: 0x040001DF RID: 479
		private DnsQType type;

		// Token: 0x040001E0 RID: 480
		private DnsQClass _class;
	}
}
