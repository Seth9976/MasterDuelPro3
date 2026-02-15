using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000076 RID: 118
	public class RfcControl : Asn1Sequence
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00011A64 File Offset: 0x0000FC64
		public virtual Asn1OctetString ControlType
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00011A74 File Offset: 0x0000FC74
		public virtual Asn1Boolean Criticality
		{
			get
			{
				if (base.size() > 1)
				{
					Asn1Object asn1Object = base.get_Renamed(1);
					if (asn1Object is Asn1Boolean)
					{
						return (Asn1Boolean)asn1Object;
					}
				}
				return new Asn1Boolean(false);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00011AEC File Offset: 0x0000FCEC
		public virtual Asn1OctetString ControlValue
		{
			get
			{
				if (base.size() > 2)
				{
					return (Asn1OctetString)base.get_Renamed(2);
				}
				if (base.size() > 1)
				{
					Asn1Object asn1Object = base.get_Renamed(1);
					if (asn1Object is Asn1OctetString)
					{
						return (Asn1OctetString)asn1Object;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (base.size() == 3)
				{
					base.set_Renamed(2, value);
					return;
				}
				if (base.size() != 2)
				{
					return;
				}
				if (base.get_Renamed(1) is Asn1OctetString)
				{
					base.set_Renamed(1, value);
					return;
				}
				base.add(value);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011B2C File Offset: 0x0000FD2C
		public RfcControl(RfcLdapOID controlType)
			: this(controlType, new Asn1Boolean(false), null)
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011B3C File Offset: 0x0000FD3C
		public RfcControl(RfcLdapOID controlType, Asn1Boolean criticality)
			: this(controlType, criticality, null)
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011B47 File Offset: 0x0000FD47
		public RfcControl(RfcLdapOID controlType, Asn1Boolean criticality, Asn1OctetString controlValue)
			: base(3)
		{
			base.add(controlType);
			if (criticality.booleanValue())
			{
				base.add(criticality);
			}
			if (controlValue != null)
			{
				base.add(controlValue);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011B70 File Offset: 0x0000FD70
		[CLSCompliant(false)]
		public RfcControl(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public RfcControl(Asn1Sequence seqObj)
			: base(3)
		{
			int num = seqObj.size();
			for (int i = 0; i < num; i++)
			{
				base.add(seqObj.get_Renamed(i));
			}
		}
	}
}
