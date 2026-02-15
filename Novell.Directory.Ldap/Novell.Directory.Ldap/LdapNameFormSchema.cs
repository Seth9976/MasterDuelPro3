using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003D RID: 61
	public class LdapNameFormSchema : LdapSchemaElement
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00009D23 File Offset: 0x00007F23
		public virtual string ObjectClass
		{
			get
			{
				return this.objectClass;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009D2B File Offset: 0x00007F2B
		public virtual string[] RequiredNamingAttributes
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00009D33 File Offset: 0x00007F33
		public virtual string[] OptionalNamingAttributes
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009D3C File Offset: 0x00007F3C
		public LdapNameFormSchema(string[] names, string oid, string description, bool obsolete, string objectClass, string[] required, string[] optional)
			: base(LdapSchema.schemaTypeNames[3])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.objectClass = objectClass;
			this.required = new string[required.Length];
			required.CopyTo(this.required, 0);
			this.optional = new string[optional.Length];
			optional.CopyTo(this.optional, 0);
			base.Value = this.formatString();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public LdapNameFormSchema(string raw)
			: base(LdapSchema.schemaTypeNames[3])
		{
			this.obsolete = false;
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.Names != null)
				{
					this.names = new string[schemaParser.Names.Length];
					schemaParser.Names.CopyTo(this.names, 0);
				}
				if (schemaParser.ID != null)
				{
					this.oid = new StringBuilder(schemaParser.ID).ToString();
				}
				if (schemaParser.Description != null)
				{
					this.description = new StringBuilder(schemaParser.Description).ToString();
				}
				if (schemaParser.Required != null)
				{
					this.required = new string[schemaParser.Required.Length];
					schemaParser.Required.CopyTo(this.required, 0);
				}
				if (schemaParser.Optional != null)
				{
					this.optional = new string[schemaParser.Optional.Length];
					schemaParser.Optional.CopyTo(this.optional, 0);
				}
				if (schemaParser.ObjectClass != null)
				{
					this.objectClass = schemaParser.ObjectClass;
				}
				this.obsolete = schemaParser.Obsolete;
				IEnumerator qualifiers = schemaParser.Qualifiers;
				while (qualifiers.MoveNext())
				{
					object obj = qualifiers.Current;
					AttributeQualifier attributeQualifier = (AttributeQualifier)obj;
					this.setQualifier(attributeQualifier.Name, attributeQualifier.Values);
				}
				base.Value = this.formatString();
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009F40 File Offset: 0x00008140
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			string[] array = this.Names;
			if (array != null)
			{
				stringBuilder.Append(" NAME ");
				if (array.Length == 1)
				{
					stringBuilder.Append("'" + array[0] + "'");
				}
				else
				{
					stringBuilder.Append("( ");
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(" '" + array[i] + "'");
					}
					stringBuilder.Append(" )");
				}
			}
			if ((text = this.Description) != null)
			{
				stringBuilder.Append(" DESC ");
				stringBuilder.Append("'" + text + "'");
			}
			if (this.Obsolete)
			{
				stringBuilder.Append(" OBSOLETE");
			}
			if ((text = this.ObjectClass) != null)
			{
				stringBuilder.Append(" OC ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((array = this.RequiredNamingAttributes) != null)
			{
				stringBuilder.Append(" MUST ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[j]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.OptionalNamingAttributes) != null)
			{
				stringBuilder.Append(" MAY ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int k = 0; k < array.Length; k++)
				{
					if (k > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[k]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			IEnumerator qualifierNames;
			if ((qualifierNames = this.QualifierNames) != null)
			{
				while (qualifierNames.MoveNext())
				{
					object obj = qualifierNames.Current;
					string text2 = (string)obj;
					stringBuilder.Append(" " + text2 + " ");
					string[] qualifier;
					if ((qualifier = this.getQualifier(text2)) != null)
					{
						if (qualifier.Length > 1)
						{
							stringBuilder.Append("( ");
						}
						for (int l = 0; l < qualifier.Length; l++)
						{
							if (l > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[l] + "'");
						}
						if (qualifier.Length > 1)
						{
							stringBuilder.Append(" )");
						}
					}
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x04000149 RID: 329
		private string objectClass;

		// Token: 0x0400014A RID: 330
		private string[] required;

		// Token: 0x0400014B RID: 331
		private string[] optional;
	}
}
