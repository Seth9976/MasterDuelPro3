using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000DF RID: 223
	public class ModuleStateEventData : BaseEdirEventData
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001775B File Offset: 0x0001595B
		public string ConnectionDN
		{
			get
			{
				return this.strConnectionDN;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00017763 File Offset: 0x00015963
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001776B File Offset: 0x0001596B
		public string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00017773 File Offset: 0x00015973
		public string Description
		{
			get
			{
				return this.strDescription;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001777B File Offset: 0x0001597B
		public string Source
		{
			get
			{
				return this.strSource;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00017784 File Offset: 0x00015984
		public ModuleStateEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strConnectionDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strName = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strDescription = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.strSource = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00017850 File Offset: 0x00015A50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ModuleStateEvent");
			stringBuilder.AppendFormat("(connectionDN={0})", this.strConnectionDN);
			stringBuilder.AppendFormat("(flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(Name={0})", this.strName);
			stringBuilder.AppendFormat("(Description={0})", this.strDescription);
			stringBuilder.AppendFormat("(Source={0})", this.strSource);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000495 RID: 1173
		protected string strConnectionDN;

		// Token: 0x04000496 RID: 1174
		protected int nFlags;

		// Token: 0x04000497 RID: 1175
		protected string strName;

		// Token: 0x04000498 RID: 1176
		protected string strDescription;

		// Token: 0x04000499 RID: 1177
		protected string strSource;
	}
}
