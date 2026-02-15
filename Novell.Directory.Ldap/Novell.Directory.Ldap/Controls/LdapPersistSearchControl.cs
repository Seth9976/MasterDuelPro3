using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000E5 RID: 229
	public class LdapPersistSearchControl : LdapControl
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00017F5D File Offset: 0x0001615D
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00017F65 File Offset: 0x00016165
		public virtual int ChangeTypes
		{
			get
			{
				return this.m_changeTypes;
			}
			set
			{
				this.m_changeTypes = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.CHANGETYPES_INDEX, new Asn1Integer(this.m_changeTypes));
				this.setValue();
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00017F8F File Offset: 0x0001618F
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00017F97 File Offset: 0x00016197
		public virtual bool ReturnControls
		{
			get
			{
				return this.m_returnControls;
			}
			set
			{
				this.m_returnControls = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.RETURNCONTROLS_INDEX, new Asn1Boolean(this.m_returnControls));
				this.setValue();
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00017FC1 File Offset: 0x000161C1
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00017FC9 File Offset: 0x000161C9
		public virtual bool ChangesOnly
		{
			get
			{
				return this.m_changesOnly;
			}
			set
			{
				this.m_changesOnly = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.CHANGESONLY_INDEX, new Asn1Boolean(this.m_changesOnly));
				this.setValue();
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00017FF3 File Offset: 0x000161F3
		public LdapPersistSearchControl()
			: this(LdapPersistSearchControl.ANY, true, true, true)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018004 File Offset: 0x00016204
		public LdapPersistSearchControl(int changeTypes, bool changesOnly, bool returnControls, bool isCritical)
			: base(LdapPersistSearchControl.requestOID, isCritical, null)
		{
			this.m_changeTypes = changeTypes;
			this.m_changesOnly = changesOnly;
			this.m_returnControls = returnControls;
			this.m_sequence = new Asn1Sequence(LdapPersistSearchControl.SEQUENCE_SIZE);
			this.m_sequence.add(new Asn1Integer(this.m_changeTypes));
			this.m_sequence.add(new Asn1Boolean(this.m_changesOnly));
			this.m_sequence.add(new Asn1Boolean(this.m_returnControls));
			this.setValue();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001808C File Offset: 0x0001628C
		public override string ToString()
		{
			sbyte[] encoding = this.m_sequence.getEncoding(LdapPersistSearchControl.s_encoder);
			StringBuilder stringBuilder = new StringBuilder(encoding.Length);
			for (int i = 0; i < encoding.Length; i++)
			{
				stringBuilder.Append(encoding[i].ToString());
				if (i < encoding.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000180EE File Offset: 0x000162EE
		private void setValue()
		{
			base.setValue(this.m_sequence.getEncoding(LdapPersistSearchControl.s_encoder));
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018108 File Offset: 0x00016308
		static LdapPersistSearchControl()
		{
			try
			{
				LdapControl.register(LdapPersistSearchControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapEntryChangeControl"));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x040004B0 RID: 1200
		private static int SEQUENCE_SIZE = 3;

		// Token: 0x040004B1 RID: 1201
		private static int CHANGETYPES_INDEX = 0;

		// Token: 0x040004B2 RID: 1202
		private static int CHANGESONLY_INDEX = 1;

		// Token: 0x040004B3 RID: 1203
		private static int RETURNCONTROLS_INDEX = 2;

		// Token: 0x040004B4 RID: 1204
		private static LBEREncoder s_encoder = new LBEREncoder();

		// Token: 0x040004B5 RID: 1205
		private int m_changeTypes;

		// Token: 0x040004B6 RID: 1206
		private bool m_changesOnly;

		// Token: 0x040004B7 RID: 1207
		private bool m_returnControls;

		// Token: 0x040004B8 RID: 1208
		private Asn1Sequence m_sequence;

		// Token: 0x040004B9 RID: 1209
		private static string requestOID = "2.16.840.1.113730.3.4.3";

		// Token: 0x040004BA RID: 1210
		private static string responseOID = "2.16.840.1.113730.3.4.7";

		// Token: 0x040004BB RID: 1211
		public const int ADD = 1;

		// Token: 0x040004BC RID: 1212
		public const int DELETE = 2;

		// Token: 0x040004BD RID: 1213
		public const int MODIFY = 4;

		// Token: 0x040004BE RID: 1214
		public const int MODDN = 8;

		// Token: 0x040004BF RID: 1215
		public static readonly int ANY = 15;
	}
}
