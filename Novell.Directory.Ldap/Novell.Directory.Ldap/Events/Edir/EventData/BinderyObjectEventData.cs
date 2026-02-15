using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000D8 RID: 216
	public class BinderyObjectEventData : BaseEdirEventData
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001683E File Offset: 0x00014A3E
		public string EntryDN
		{
			get
			{
				return this.strEntryDN;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00016846 File Offset: 0x00014A46
		public int ValueType
		{
			get
			{
				return this.nType;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001684E File Offset: 0x00014A4E
		public int EmuObjFlags
		{
			get
			{
				return this.nEmuObjFlags;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00016856 File Offset: 0x00014A56
		public int Security
		{
			get
			{
				return this.nSecurity;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001685E File Offset: 0x00014A5E
		public string Name
		{
			get
			{
				return this.strName;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00016868 File Offset: 0x00014A68
		public BinderyObjectEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.strEntryDN = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			this.nType = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nEmuObjFlags = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.nSecurity = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strName = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016934 File Offset: 0x00014B34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BinderyObjectEvent");
			stringBuilder.AppendFormat("(EntryDn={0})", this.strEntryDN);
			stringBuilder.AppendFormat("(Type={0})", this.nType);
			stringBuilder.AppendFormat("(EnumOldFlags={0})", this.nEmuObjFlags);
			stringBuilder.AppendFormat("(Secuirty={0})", this.nSecurity);
			stringBuilder.AppendFormat("(Name={0})", this.strName);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400046F RID: 1135
		protected string strEntryDN;

		// Token: 0x04000470 RID: 1136
		protected int nType;

		// Token: 0x04000471 RID: 1137
		protected int nEmuObjFlags;

		// Token: 0x04000472 RID: 1138
		protected int nSecurity;

		// Token: 0x04000473 RID: 1139
		protected string strName;
	}
}
