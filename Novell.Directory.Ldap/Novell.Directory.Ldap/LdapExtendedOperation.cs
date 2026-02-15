using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000031 RID: 49
	public class LdapExtendedOperation : ICloneable
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00008DB5 File Offset: 0x00006FB5
		[CLSCompliant(false)]
		public LdapExtendedOperation(string oid, sbyte[] vals)
		{
			this.oid = oid;
			this.vals = vals;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008DCC File Offset: 0x00006FCC
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				Array.Copy(this.vals, 0, ((LdapExtendedOperation)obj).vals, 0, this.vals.Length);
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008E24 File Offset: 0x00007024
		public virtual string getID()
		{
			return this.oid;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008E2C File Offset: 0x0000702C
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			return this.vals;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008E34 File Offset: 0x00007034
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] newVals)
		{
			this.vals = newVals;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008E3D File Offset: 0x0000703D
		protected internal virtual void setID(string newoid)
		{
			this.oid = newoid;
		}

		// Token: 0x04000120 RID: 288
		private string oid;

		// Token: 0x04000121 RID: 289
		private sbyte[] vals;
	}
}
