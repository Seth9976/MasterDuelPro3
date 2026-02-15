using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004B RID: 75
	public class LdapSyntaxSchema : LdapSchemaElement
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000BF19 File Offset: 0x0000A119
		public LdapSyntaxSchema(string oid, string description)
			: base(LdapSchema.schemaTypeNames[2])
		{
			this.oid = oid;
			this.description = description;
			base.Value = this.formatString();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000BF44 File Offset: 0x0000A144
		public LdapSyntaxSchema(string raw)
			: base(LdapSchema.schemaTypeNames[2])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.ID != null)
				{
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				IEnumerator qualifiers = schemaParser.Qualifiers;
				while (qualifiers.MoveNext())
				{
					object obj = qualifiers.Current;
					AttributeQualifier attributeQualifier = (AttributeQualifier)obj;
					this.setQualifier(attributeQualifier.Name, attributeQualifier.Values);
				}
				base.Value = this.formatString();
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			if ((text = this.Description) != null)
			{
				stringBuilder.Append(" DESC ");
				stringBuilder.Append("'" + text + "'");
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
					if ((qualifier = this.getQualifier(text2)) != null && qualifier.Length > 1)
					{
						stringBuilder.Append("( ");
						for (int i = 0; i < qualifier.Length; i++)
						{
							if (i > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[i] + "'");
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
	}
}
