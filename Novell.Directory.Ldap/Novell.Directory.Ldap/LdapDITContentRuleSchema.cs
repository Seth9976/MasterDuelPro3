using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002A RID: 42
	public class LdapDITContentRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007C59 File Offset: 0x00005E59
		public virtual string[] AuxiliaryClasses
		{
			get
			{
				return this.auxiliary;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007C61 File Offset: 0x00005E61
		public virtual string[] RequiredAttributes
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007C69 File Offset: 0x00005E69
		public virtual string[] OptionalAttributes
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007C71 File Offset: 0x00005E71
		public virtual string[] PrecludedAttributes
		{
			get
			{
				return this.precluded;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007C7C File Offset: 0x00005E7C
		public LdapDITContentRuleSchema(string[] names, string oid, string description, bool obsolete, string[] auxiliary, string[] required, string[] optional, string[] precluded)
			: base(LdapSchema.schemaTypeNames[4])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.auxiliary = auxiliary;
			this.required = required;
			this.optional = optional;
			this.precluded = precluded;
			base.Value = this.formatString();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007D44 File Offset: 0x00005F44
		public LdapDITContentRuleSchema(string raw)
			: base(LdapSchema.schemaTypeNames[4])
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
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Auxiliary != null)
				{
					this.auxiliary = new string[schemaParser.Auxiliary.Length];
					schemaParser.Auxiliary.CopyTo(this.auxiliary, 0);
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
				if (schemaParser.Precluded != null)
				{
					this.precluded = new string[schemaParser.Precluded.Length];
					schemaParser.Precluded.CopyTo(this.precluded, 0);
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

		// Token: 0x060001CE RID: 462 RVA: 0x00007F30 File Offset: 0x00006130
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
			if ((array = this.AuxiliaryClasses) != null)
			{
				stringBuilder.Append(" AUX ");
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
			if ((array = this.RequiredAttributes) != null)
			{
				stringBuilder.Append(" MUST ");
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
			if ((array = this.OptionalAttributes) != null)
			{
				stringBuilder.Append(" MAY ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (l > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[l]);
				}
				if (array.Length > 1)
				{
					stringBuilder.Append(" )");
				}
			}
			if ((array = this.PrecludedAttributes) != null)
			{
				stringBuilder.Append(" NOT ");
				if (array.Length > 1)
				{
					stringBuilder.Append("( ");
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (m > 0)
					{
						stringBuilder.Append(" $ ");
					}
					stringBuilder.Append(array[m]);
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
						for (int n = 0; n < qualifier.Length; n++)
						{
							if (n > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[n] + "'");
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

		// Token: 0x040000B8 RID: 184
		private string[] auxiliary = new string[] { "" };

		// Token: 0x040000B9 RID: 185
		private string[] required = new string[] { "" };

		// Token: 0x040000BA RID: 186
		private string[] optional = new string[] { "" };

		// Token: 0x040000BB RID: 187
		private string[] precluded = new string[] { "" };
	}
}
