using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000D9 RID: 217
	public class ChangeAddressEventData : BaseEdirEventData
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000169CC File Offset: 0x00014BCC
		public int Flags
		{
			get
			{
				return this.nFlags;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x000169D4 File Offset: 0x00014BD4
		public int Proto
		{
			get
			{
				return this.nProto;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000169DC File Offset: 0x00014BDC
		public int AddressFamily
		{
			get
			{
				return this.address_family;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x000169E4 File Offset: 0x00014BE4
		public string Address
		{
			get
			{
				return this.strAddress;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000169EC File Offset: 0x00014BEC
		public string PstkName
		{
			get
			{
				return this.pstk_name;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000169F4 File Offset: 0x00014BF4
		public string SourceModule
		{
			get
			{
				return this.source_module;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000169FC File Offset: 0x00014BFC
		public ChangeAddressEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.nFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nProto = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.address_family = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strAddress = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.pstk_name = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.source_module = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016AEC File Offset: 0x00014CEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ChangeAddresssEvent");
			stringBuilder.AppendFormat("(flags={0})", this.nFlags);
			stringBuilder.AppendFormat("(proto={0})", this.nProto);
			stringBuilder.AppendFormat("(addrFamily={0})", this.address_family);
			stringBuilder.AppendFormat("(address={0})", this.strAddress);
			stringBuilder.AppendFormat("(pstkName={0})", this.pstk_name);
			stringBuilder.AppendFormat("(source={0})", this.source_module);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000474 RID: 1140
		protected int nFlags;

		// Token: 0x04000475 RID: 1141
		protected int nProto;

		// Token: 0x04000476 RID: 1142
		protected int address_family;

		// Token: 0x04000477 RID: 1143
		protected string strAddress;

		// Token: 0x04000478 RID: 1144
		protected string pstk_name;

		// Token: 0x04000479 RID: 1145
		protected string source_module;
	}
}
