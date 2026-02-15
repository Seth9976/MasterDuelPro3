using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000E0 RID: 224
	public class NetworkAddressEventData : BaseEdirEventData
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000178DE File Offset: 0x00015ADE
		public int ValueType
		{
			get
			{
				return this.nType;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000178E6 File Offset: 0x00015AE6
		public string Data
		{
			get
			{
				return this.strData;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000178F0 File Offset: 0x00015AF0
		public NetworkAddressEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.nType = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strData = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017958 File Offset: 0x00015B58
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NetworkAddress");
			stringBuilder.AppendFormat("(type={0})", this.nType);
			stringBuilder.AppendFormat("(Data={0})", this.strData);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400049A RID: 1178
		protected int nType;

		// Token: 0x0400049B RID: 1179
		protected string strData;
	}
}
