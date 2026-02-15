using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000029 RID: 41
	public class LdapControl : ICloneable
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public virtual string ID
		{
			get
			{
				return new StringBuilder(this.control.ControlType.stringValue()).ToString();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public virtual bool Critical
		{
			get
			{
				return this.control.Criticality.booleanValue();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007B02 File Offset: 0x00005D02
		internal static RespControlVector RegisteredControls
		{
			get
			{
				return LdapControl.registeredControls;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007B09 File Offset: 0x00005D09
		internal virtual RfcControl Asn1Object
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007B14 File Offset: 0x00005D14
		[CLSCompliant(false)]
		public LdapControl(string oid, bool critical, sbyte[] values)
		{
			if (oid == null)
			{
				throw new ArgumentException("An OID must be specified");
			}
			if (values == null)
			{
				this.control = new RfcControl(new RfcLdapOID(oid), new Asn1Boolean(critical));
				return;
			}
			this.control = new RfcControl(new RfcLdapOID(oid), new Asn1Boolean(critical), new Asn1OctetString(values));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007B6D File Offset: 0x00005D6D
		protected internal LdapControl(RfcControl control)
		{
			this.control = control;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007B7C File Offset: 0x00005D7C
		public object Clone()
		{
			LdapControl ldapControl;
			try
			{
				ldapControl = (LdapControl)base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			sbyte[] value = this.getValue();
			if (value != null)
			{
				sbyte[] array = new sbyte[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					array[i] = value[i];
				}
				ldapControl.control = new RfcControl(new RfcLdapOID(this.ID), new Asn1Boolean(this.Critical), new Asn1OctetString(array));
			}
			return ldapControl;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007C04 File Offset: 0x00005E04
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			sbyte[] array = null;
			Asn1OctetString controlValue = this.control.ControlValue;
			if (controlValue != null)
			{
				array = controlValue.byteValue();
			}
			return array;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007C2A File Offset: 0x00005E2A
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] controlValue)
		{
			this.control.ControlValue = new Asn1OctetString(controlValue);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007C3D File Offset: 0x00005E3D
		public static void register(string oid, Type controlClass)
		{
			LdapControl.registeredControls.registerResponseControl(oid, controlClass);
		}

		// Token: 0x040000B6 RID: 182
		private static RespControlVector registeredControls = new RespControlVector(5, 5);

		// Token: 0x040000B7 RID: 183
		private RfcControl control;
	}
}
