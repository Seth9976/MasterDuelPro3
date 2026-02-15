using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000DA RID: 218
	public class ConnectionStateEventData : BaseEdirEventData
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00016B96 File Offset: 0x00014D96
		public string ConnectionDN
		{
			get
			{
				return this.strConnectionDN;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00016B9E File Offset: 0x00014D9E
		public int OldFlags
		{
			get
			{
				return this.old_flags;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00016BA6 File Offset: 0x00014DA6
		public int NewFlags
		{
			get
			{
				return this.new_flags;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00016BAE File Offset: 0x00014DAE
		public string SourceModule
		{
			get
			{
				return this.source_module;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public ConnectionStateEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strConnectionDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.old_flags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.new_flags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.source_module = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016C64 File Offset: 0x00014E64
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ConnectionStateEvent");
			stringBuilder.AppendFormat("(ConnectionDN={0})", this.strConnectionDN);
			stringBuilder.AppendFormat("(oldFlags={0})", this.old_flags);
			stringBuilder.AppendFormat("(newFlags={0})", this.new_flags);
			stringBuilder.AppendFormat("(SourceModule={0})", this.source_module);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400047A RID: 1146
		protected string strConnectionDN;

		// Token: 0x0400047B RID: 1147
		protected int old_flags;

		// Token: 0x0400047C RID: 1148
		protected int new_flags;

		// Token: 0x0400047D RID: 1149
		protected string source_module;
	}
}
