using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000D6 RID: 214
	public class MonitorEventResponse : LdapExtendedResponse
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001671C File Offset: 0x0001491C
		public EdirEventSpecifier[] SpecifierList
		{
			get
			{
				return this.specifier_list;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016724 File Offset: 0x00014924
		public MonitorEventResponse(RfcLdapMessage message)
			: base(message)
		{
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, null);
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)new LBERDecoder().decode(value);
			int num = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
			this.specifier_list = new EdirEventSpecifier[num];
			for (int i = 0; i < num; i++)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Set.get_Renamed(i);
				int num2 = ((Asn1Integer)asn1Sequence2.get_Renamed(0)).intValue();
				int num3 = ((Asn1Enumerated)asn1Sequence2.get_Renamed(1)).intValue();
				this.specifier_list[i] = new EdirEventSpecifier((EdirEventType)num2, (EdirEventResultType)num3);
			}
		}

		// Token: 0x0400046B RID: 1131
		protected EdirEventSpecifier[] specifier_list;
	}
}
