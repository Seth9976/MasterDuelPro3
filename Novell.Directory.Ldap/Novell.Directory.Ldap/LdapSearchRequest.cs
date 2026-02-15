using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000047 RID: 71
	public class LdapSearchRequest : LdapMessage
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00005D4D File Offset: 0x00003F4D
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B6F9 File Offset: 0x000098F9
		public virtual int Scope
		{
			get
			{
				return ((Asn1Enumerated)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(1)).intValue();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B71C File Offset: 0x0000991C
		public virtual int Dereference
		{
			get
			{
				return ((Asn1Enumerated)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(2)).intValue();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B73F File Offset: 0x0000993F
		public virtual int MaxResults
		{
			get
			{
				return ((Asn1Integer)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(3)).intValue();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B762 File Offset: 0x00009962
		public virtual int ServerTimeLimit
		{
			get
			{
				return ((Asn1Integer)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(4)).intValue();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B785 File Offset: 0x00009985
		public virtual bool TypesOnly
		{
			get
			{
				return ((Asn1Boolean)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(5)).booleanValue();
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public virtual string[] Attributes
		{
			get
			{
				RfcAttributeDescriptionList rfcAttributeDescriptionList = (RfcAttributeDescriptionList)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(7);
				string[] array = new string[rfcAttributeDescriptionList.size()];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ((RfcAttributeDescription)rfcAttributeDescriptionList.get_Renamed(i)).stringValue();
				}
				return array;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B801 File Offset: 0x00009A01
		public virtual string StringFilter
		{
			get
			{
				return this.RfcFilter.filterToString();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B80E File Offset: 0x00009A0E
		private RfcFilter RfcFilter
		{
			get
			{
				return (RfcFilter)((RfcSearchRequest)this.Asn1Object.get_Renamed(1)).get_Renamed(6);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B82C File Offset: 0x00009A2C
		public virtual IEnumerator SearchFilter
		{
			get
			{
				return this.RfcFilter.getFilterIterator();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B83C File Offset: 0x00009A3C
		public LdapSearchRequest(string base_Renamed, int scope, string filter, string[] attrs, int dereference, int maxResults, int serverTimeLimit, bool typesOnly, LdapControl[] cont)
			: base(3, new RfcSearchRequest(new RfcLdapDN(base_Renamed), new Asn1Enumerated(scope), new Asn1Enumerated(dereference), new Asn1Integer(maxResults), new Asn1Integer(serverTimeLimit), new Asn1Boolean(typesOnly), new RfcFilter(filter), new RfcAttributeDescriptionList(attrs)), cont)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B88C File Offset: 0x00009A8C
		public LdapSearchRequest(string base_Renamed, int scope, RfcFilter filter, string[] attrs, int dereference, int maxResults, int serverTimeLimit, bool typesOnly, LdapControl[] cont)
			: base(3, new RfcSearchRequest(new RfcLdapDN(base_Renamed), new Asn1Enumerated(scope), new Asn1Enumerated(dereference), new Asn1Integer(maxResults), new Asn1Integer(serverTimeLimit), new Asn1Boolean(typesOnly), filter, new RfcAttributeDescriptionList(attrs)), cont)
		{
		}

		// Token: 0x04000173 RID: 371
		public const int AND = 0;

		// Token: 0x04000174 RID: 372
		public const int OR = 1;

		// Token: 0x04000175 RID: 373
		public const int NOT = 2;

		// Token: 0x04000176 RID: 374
		public const int EQUALITY_MATCH = 3;

		// Token: 0x04000177 RID: 375
		public const int SUBSTRINGS = 4;

		// Token: 0x04000178 RID: 376
		public const int GREATER_OR_EQUAL = 5;

		// Token: 0x04000179 RID: 377
		public const int LESS_OR_EQUAL = 6;

		// Token: 0x0400017A RID: 378
		public const int PRESENT = 7;

		// Token: 0x0400017B RID: 379
		public const int APPROX_MATCH = 8;

		// Token: 0x0400017C RID: 380
		public const int EXTENSIBLE_MATCH = 9;

		// Token: 0x0400017D RID: 381
		public const int INITIAL = 0;

		// Token: 0x0400017E RID: 382
		public const int ANY = 1;

		// Token: 0x0400017F RID: 383
		public const int FINAL = 2;
	}
}
