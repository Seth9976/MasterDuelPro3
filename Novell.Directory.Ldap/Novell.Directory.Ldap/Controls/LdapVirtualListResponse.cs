using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000EA RID: 234
	public class LdapVirtualListResponse : LdapControl
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001879C File Offset: 0x0001699C
		public virtual int ContentCount
		{
			get
			{
				return this.m_ContentCount;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000187A4 File Offset: 0x000169A4
		public virtual int FirstPosition
		{
			get
			{
				return this.m_firstPosition;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000187AC File Offset: 0x000169AC
		public virtual int ResultCode
		{
			get
			{
				return this.m_resultCode;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000187B4 File Offset: 0x000169B4
		public virtual string Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000187BC File Offset: 0x000169BC
		[CLSCompliant(false)]
		public LdapVirtualListResponse(string oid, bool critical, sbyte[] values)
			: base(oid, critical, values)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object = lberdecoder.decode(values);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object2 = ((Asn1Sequence)asn1Object).get_Renamed(0);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Integer))
			{
				throw new IOException("Decoding error");
			}
			this.m_firstPosition = ((Asn1Integer)asn1Object2).intValue();
			Asn1Object asn1Object3 = ((Asn1Sequence)asn1Object).get_Renamed(1);
			if (asn1Object3 == null || !(asn1Object3 is Asn1Integer))
			{
				throw new IOException("Decoding error");
			}
			this.m_ContentCount = ((Asn1Integer)asn1Object3).intValue();
			Asn1Object asn1Object4 = ((Asn1Sequence)asn1Object).get_Renamed(2);
			if (asn1Object4 != null && asn1Object4 is Asn1Enumerated)
			{
				this.m_resultCode = ((Asn1Enumerated)asn1Object4).intValue();
				if (((Asn1Sequence)asn1Object).size() > 3)
				{
					Asn1Object asn1Object5 = ((Asn1Sequence)asn1Object).get_Renamed(3);
					if (asn1Object5 != null && asn1Object5 is Asn1OctetString)
					{
						this.m_context = ((Asn1OctetString)asn1Object5).stringValue();
					}
				}
				return;
			}
			throw new IOException("Decoding error");
		}

		// Token: 0x040004D4 RID: 1236
		private int m_firstPosition;

		// Token: 0x040004D5 RID: 1237
		private int m_ContentCount;

		// Token: 0x040004D6 RID: 1238
		private int m_resultCode;

		// Token: 0x040004D7 RID: 1239
		private string m_context;
	}
}
