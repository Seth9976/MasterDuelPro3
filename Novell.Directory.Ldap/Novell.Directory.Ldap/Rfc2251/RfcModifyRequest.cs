using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200008B RID: 139
	public class RfcModifyRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00013D97 File Offset: 0x00011F97
		public virtual Asn1SequenceOf Modifications
		{
			get
			{
				return (Asn1SequenceOf)base.get_Renamed(1);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000116AF File Offset: 0x0000F8AF
		public RfcModifyRequest(RfcLdapDN object_Renamed, Asn1SequenceOf modification)
			: base(2)
		{
			base.add(object_Renamed);
			base.add(modification);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000116C6 File Offset: 0x0000F8C6
		internal RfcModifyRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00013DA5 File Offset: 0x00011FA5
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 6);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00013DAF File Offset: 0x00011FAF
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000116FA File Offset: 0x0000F8FA
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
