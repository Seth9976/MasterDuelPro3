using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003E RID: 62
	public class LdapObjectClassSchema : LdapSchemaElement
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A1E9 File Offset: 0x000083E9
		public virtual string[] Superiors
		{
			get
			{
				return this.superiors;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A1F1 File Offset: 0x000083F1
		public virtual string[] RequiredAttributes
		{
			get
			{
				return this.required;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A1F9 File Offset: 0x000083F9
		public virtual string[] OptionalAttributes
		{
			get
			{
				return this.optional;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A201 File Offset: 0x00008401
		public virtual int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A20C File Offset: 0x0000840C
		public LdapObjectClassSchema(string[] names, string oid, string[] superiors, string description, string[] required, string[] optional, int type, bool obsolete)
			: base(LdapSchema.schemaTypeNames[1])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.type = type;
			this.obsolete = obsolete;
			if (superiors != null)
			{
				this.superiors = new string[superiors.Length];
				superiors.CopyTo(this.superiors, 0);
			}
			if (required != null)
			{
				this.required = new string[required.Length];
				required.CopyTo(this.required, 0);
			}
			if (optional != null)
			{
				this.optional = new string[optional.Length];
				optional.CopyTo(this.optional, 0);
			}
			base.Value = this.formatString();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public LdapObjectClassSchema(string raw)
			: base(LdapSchema.schemaTypeNames[1])
		{
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
				this.obsolete = schemaParser.Obsolete;
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
				if (schemaParser.Superiors != null)
				{
					this.superiors = new string[schemaParser.Superiors.Length];
					schemaParser.Superiors.CopyTo(this.superiors, 0);
				}
				this.type = schemaParser.Type;
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

		// Token: 0x06000254 RID: 596 RVA: 0x0000A44C File Offset: 0x0000864C
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
			if ((array = this.Superiors) != null)
			{
				stringBuilder.Append(" SUP ");
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
			if (this.Type != -1)
			{
				if (this.Type == 0)
				{
					stringBuilder.Append(" ABSTRACT");
				}
				else if (this.Type == 2)
				{
					stringBuilder.Append(" AUXILIARY");
				}
				else if (this.Type == 1)
				{
					stringBuilder.Append(" STRUCTURAL");
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
						for (int m = 0; m < qualifier.Length; m++)
						{
							if (m > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[m] + "'");
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

		// Token: 0x0400014C RID: 332
		internal string[] superiors;

		// Token: 0x0400014D RID: 333
		internal string[] required;

		// Token: 0x0400014E RID: 334
		internal string[] optional;

		// Token: 0x0400014F RID: 335
		internal int type = -1;

		// Token: 0x04000150 RID: 336
		public const int ABSTRACT = 0;

		// Token: 0x04000151 RID: 337
		public const int STRUCTURAL = 1;

		// Token: 0x04000152 RID: 338
		public const int AUXILIARY = 2;
	}
}
