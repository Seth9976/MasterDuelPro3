using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000044 RID: 68
	public abstract class LdapSchemaElement : LdapAttribute
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000B3F0 File Offset: 0x000095F0
		private void InitBlock()
		{
			this.hashQualifier = new Hashtable();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B400 File Offset: 0x00009600
		public virtual string[] Names
		{
			get
			{
				if (this.names == null)
				{
					return null;
				}
				string[] array = new string[this.names.Length];
				this.names.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B433 File Offset: 0x00009633
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B43B File Offset: 0x0000963B
		public virtual string ID
		{
			get
			{
				return this.oid;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B443 File Offset: 0x00009643
		public virtual IEnumerator QualifierNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.hashQualifier.Keys).GetEnumerator());
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B45F File Offset: 0x0000965F
		public virtual bool Obsolete
		{
			get
			{
				return this.obsolete;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B468 File Offset: 0x00009668
		protected internal LdapSchemaElement(string attrName)
			: base(attrName)
		{
			this.InitBlock();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public virtual string[] getQualifier(string name)
		{
			AttributeQualifier attributeQualifier = (AttributeQualifier)this.hashQualifier[name];
			if (attributeQualifier != null)
			{
				return attributeQualifier.Values;
			}
			return null;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B4EA File Offset: 0x000096EA
		public override string ToString()
		{
			return this.formatString();
		}

		// Token: 0x0600029D RID: 669
		protected internal abstract string formatString();

		// Token: 0x0600029E RID: 670 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public virtual void setQualifier(string name, string[] values)
		{
			AttributeQualifier attributeQualifier = new AttributeQualifier(name, values);
			SupportClass.PutElement(this.hashQualifier, name, attributeQualifier);
			base.Value = this.formatString();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B523 File Offset: 0x00009723
		public override void addValue(string value_Renamed)
		{
			throw new NotSupportedException("addValue is not supported by LdapSchemaElement");
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B523 File Offset: 0x00009723
		public virtual void addValue(byte[] value_Renamed)
		{
			throw new NotSupportedException("addValue is not supported by LdapSchemaElement");
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B52F File Offset: 0x0000972F
		public override void removeValue(string value_Renamed)
		{
			throw new NotSupportedException("removeValue is not supported by LdapSchemaElement");
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B52F File Offset: 0x0000972F
		public virtual void removeValue(byte[] value_Renamed)
		{
			throw new NotSupportedException("removeValue is not supported by LdapSchemaElement");
		}

		// Token: 0x04000162 RID: 354
		[CLSCompliant(false)]
		protected internal string[] names = new string[] { "" };

		// Token: 0x04000163 RID: 355
		protected internal string oid = "";

		// Token: 0x04000164 RID: 356
		[CLSCompliant(false)]
		protected internal string description = "";

		// Token: 0x04000165 RID: 357
		[CLSCompliant(false)]
		protected internal bool obsolete;

		// Token: 0x04000166 RID: 358
		protected internal string[] qualifier = new string[] { "" };

		// Token: 0x04000167 RID: 359
		protected internal Hashtable hashQualifier;
	}
}
