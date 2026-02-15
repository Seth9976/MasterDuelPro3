using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000072 RID: 114
	public class RfcBindRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000117F8 File Offset: 0x0000F9F8
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011806 File Offset: 0x0000FA06
		public virtual Asn1Integer Version
		{
			get
			{
				return (Asn1Integer)base.get_Renamed(0);
			}
			set
			{
				base.set_Renamed(0, value);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011810 File Offset: 0x0000FA10
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0001181E File Offset: 0x0000FA1E
		public virtual RfcLdapDN Name
		{
			get
			{
				return (RfcLdapDN)base.get_Renamed(1);
			}
			set
			{
				base.set_Renamed(1, value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011828 File Offset: 0x0000FA28
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00011836 File Offset: 0x0000FA36
		public virtual RfcAuthenticationChoice AuthenticationChoice
		{
			get
			{
				return (RfcAuthenticationChoice)base.get_Renamed(2);
			}
			set
			{
				base.set_Renamed(2, value);
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00011840 File Offset: 0x0000FA40
		public RfcBindRequest(Asn1Integer version, RfcLdapDN name, RfcAuthenticationChoice auth)
			: base(3)
		{
			base.add(version);
			base.add(name);
			base.add(auth);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001185E File Offset: 0x0000FA5E
		[CLSCompliant(false)]
		public RfcBindRequest(int version, string dn, string mechanism, sbyte[] credentials)
			: this(new Asn1Integer(version), new RfcLdapDN(dn), new RfcAuthenticationChoice(mechanism, credentials))
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001187A File Offset: 0x0000FA7A
		internal RfcBindRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(1, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011896 File Offset: 0x0000FA96
		public override Asn1Identifier getIdentifier()
		{
			return RfcBindRequest.ID;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001189D File Offset: 0x0000FA9D
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcBindRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000118AB File Offset: 0x0000FAAB
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(1)).stringValue();
		}

		// Token: 0x04000255 RID: 597
		private static readonly Asn1Identifier ID = new Asn1Identifier(1, true, 0);
	}
}
