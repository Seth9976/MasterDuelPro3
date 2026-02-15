using System;

namespace System.Xml.Schema
{
	// Token: 0x02000211 RID: 529
	internal class CompiledIdentityConstraint
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x000999B5 File Offset: 0x00097BB5
		public CompiledIdentityConstraint.ConstraintRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x000999BD File Offset: 0x00097BBD
		public Asttree Selector
		{
			get
			{
				return this.selector;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x000999C5 File Offset: 0x00097BC5
		public Asttree[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000999CD File Offset: 0x00097BCD
		private CompiledIdentityConstraint()
		{
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000999EC File Offset: 0x00097BEC
		public CompiledIdentityConstraint(XmlSchemaIdentityConstraint constraint, XmlNamespaceManager nsmgr)
		{
			this.name = constraint.QualifiedName;
			try
			{
				this.selector = new Asttree(constraint.Selector.XPath, false, nsmgr);
			}
			catch (XmlSchemaException ex)
			{
				ex.SetSource(constraint.Selector);
				throw ex;
			}
			XmlSchemaObjectCollection xmlSchemaObjectCollection = constraint.Fields;
			this.fields = new Asttree[xmlSchemaObjectCollection.Count];
			for (int i = 0; i < xmlSchemaObjectCollection.Count; i++)
			{
				try
				{
					this.fields[i] = new Asttree(((XmlSchemaXPath)xmlSchemaObjectCollection[i]).XPath, true, nsmgr);
				}
				catch (XmlSchemaException ex2)
				{
					ex2.SetSource(constraint.Fields[i]);
					throw ex2;
				}
			}
			if (constraint is XmlSchemaUnique)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Unique;
				return;
			}
			if (constraint is XmlSchemaKey)
			{
				this.role = CompiledIdentityConstraint.ConstraintRole.Key;
				return;
			}
			this.role = CompiledIdentityConstraint.ConstraintRole.Keyref;
			this.refer = ((XmlSchemaKeyref)constraint).Refer;
		}

		// Token: 0x04000B36 RID: 2870
		internal XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000B37 RID: 2871
		private CompiledIdentityConstraint.ConstraintRole role;

		// Token: 0x04000B38 RID: 2872
		private Asttree selector;

		// Token: 0x04000B39 RID: 2873
		private Asttree[] fields;

		// Token: 0x04000B3A RID: 2874
		internal XmlQualifiedName refer = XmlQualifiedName.Empty;

		// Token: 0x04000B3B RID: 2875
		public static readonly CompiledIdentityConstraint Empty = new CompiledIdentityConstraint();

		// Token: 0x02000212 RID: 530
		public enum ConstraintRole
		{
			// Token: 0x04000B3D RID: 2877
			Unique,
			// Token: 0x04000B3E RID: 2878
			Key,
			// Token: 0x04000B3F RID: 2879
			Keyref
		}
	}
}
