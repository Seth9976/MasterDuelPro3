using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000E9 RID: 233
	public class LdapVirtualListControl : LdapControl
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001840E File Offset: 0x0001660E
		public virtual int AfterCount
		{
			get
			{
				return this.m_afterCount;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00018416 File Offset: 0x00016616
		public virtual int BeforeCount
		{
			get
			{
				return this.m_beforeCount;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001841E File Offset: 0x0001661E
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00018426 File Offset: 0x00016626
		public virtual int ListSize
		{
			get
			{
				return this.m_contentCount;
			}
			set
			{
				this.m_contentCount = value;
				this.BuildIndexedVLVRequest();
				this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001844B File Offset: 0x0001664B
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00018454 File Offset: 0x00016654
		public virtual string Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				int num = 3;
				this.m_context = value;
				if (this.m_vlvRequest.size() == 4)
				{
					this.m_vlvRequest.set_Renamed(num, new Asn1OctetString(this.m_context));
				}
				else if (this.m_vlvRequest.size() == 3)
				{
					this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
				}
				this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000184CB File Offset: 0x000166CB
		public LdapVirtualListControl(string jumpTo, int beforeCount, int afterCount)
			: this(jumpTo, beforeCount, afterCount, null)
		{
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000184D8 File Offset: 0x000166D8
		public LdapVirtualListControl(string jumpTo, int beforeCount, int afterCount, string context)
		{
			this.m_contentCount = -1;
			base..ctor(LdapVirtualListControl.requestOID, true, null);
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_jumpTo = jumpTo;
			this.m_context = context;
			this.BuildTypedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018534 File Offset: 0x00016734
		private void BuildTypedVLVRequest()
		{
			this.m_vlvRequest = new Asn1Sequence(4);
			this.m_vlvRequest.add(new Asn1Integer(this.m_beforeCount));
			this.m_vlvRequest.add(new Asn1Integer(this.m_afterCount));
			this.m_vlvRequest.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapVirtualListControl.GREATERTHANOREQUAL), new Asn1OctetString(this.m_jumpTo), false));
			if (this.m_context != null)
			{
				this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000185BF File Offset: 0x000167BF
		public LdapVirtualListControl(int startIndex, int beforeCount, int afterCount, int contentCount)
			: this(startIndex, beforeCount, afterCount, contentCount, null)
		{
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000185D0 File Offset: 0x000167D0
		public LdapVirtualListControl(int startIndex, int beforeCount, int afterCount, int contentCount, string context)
		{
			this.m_contentCount = -1;
			base..ctor(LdapVirtualListControl.requestOID, true, null);
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_startIndex = startIndex;
			this.m_contentCount = contentCount;
			this.m_context = context;
			this.BuildIndexedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018634 File Offset: 0x00016834
		private void BuildIndexedVLVRequest()
		{
			this.m_vlvRequest = new Asn1Sequence(4);
			this.m_vlvRequest.add(new Asn1Integer(this.m_beforeCount));
			this.m_vlvRequest.add(new Asn1Integer(this.m_afterCount));
			Asn1Sequence asn1Sequence = new Asn1Sequence(2);
			asn1Sequence.add(new Asn1Integer(this.m_startIndex));
			asn1Sequence.add(new Asn1Integer(this.m_contentCount));
			this.m_vlvRequest.add(new Asn1Tagged(new Asn1Identifier(2, true, LdapVirtualListControl.BYOFFSET), asn1Sequence, false));
			if (this.m_context != null)
			{
				this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000186DE File Offset: 0x000168DE
		public virtual void setRange(int listIndex, int beforeCount, int afterCount)
		{
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_startIndex = listIndex;
			this.BuildIndexedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00018711 File Offset: 0x00016911
		public virtual void setRange(string jumpTo, int beforeCount, int afterCount)
		{
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_jumpTo = jumpTo;
			this.BuildTypedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018744 File Offset: 0x00016944
		static LdapVirtualListControl()
		{
			try
			{
				LdapControl.register(LdapVirtualListControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapVirtualListResponse"));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x040004C9 RID: 1225
		private static int BYOFFSET = 0;

		// Token: 0x040004CA RID: 1226
		private static int GREATERTHANOREQUAL = 1;

		// Token: 0x040004CB RID: 1227
		private static string requestOID = "2.16.840.1.113730.3.4.9";

		// Token: 0x040004CC RID: 1228
		private static string responseOID = "2.16.840.1.113730.3.4.10";

		// Token: 0x040004CD RID: 1229
		private Asn1Sequence m_vlvRequest;

		// Token: 0x040004CE RID: 1230
		private int m_beforeCount;

		// Token: 0x040004CF RID: 1231
		private int m_afterCount;

		// Token: 0x040004D0 RID: 1232
		private string m_jumpTo;

		// Token: 0x040004D1 RID: 1233
		private string m_context;

		// Token: 0x040004D2 RID: 1234
		private int m_startIndex;

		// Token: 0x040004D3 RID: 1235
		private int m_contentCount;
	}
}
