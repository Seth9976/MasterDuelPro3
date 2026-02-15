using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002B RID: 43
	public class LdapDITStructureRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000827C File Offset: 0x0000647C
		public virtual int RuleID
		{
			get
			{
				return this.ruleID;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008284 File Offset: 0x00006484
		public virtual string NameForm
		{
			get
			{
				return this.nameForm;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000828C File Offset: 0x0000648C
		public virtual string[] Superiors
		{
			get
			{
				return this.superiorIDs;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008294 File Offset: 0x00006494
		public LdapDITStructureRuleSchema(string[] names, int ruleID, string description, bool obsolete, string nameForm, string[] superiorIDs)
			: base(LdapSchema.schemaTypeNames[5])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.ruleID = ruleID;
			this.description = description;
			this.obsolete = obsolete;
			this.nameForm = nameForm;
			this.superiorIDs = superiorIDs;
			base.Value = this.formatString();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000831C File Offset: 0x0000651C
		public LdapDITStructureRuleSchema(string raw)
			: base(LdapSchema.schemaTypeNames[5])
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
					this.ruleID = int.Parse(schemaParser.ID);
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Superiors != null)
				{
					this.superiorIDs = new string[schemaParser.Superiors.Length];
					schemaParser.Superiors.CopyTo(this.superiorIDs, 0);
				}
				if (schemaParser.NameForm != null)
				{
					this.nameForm = schemaParser.NameForm;
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

		// Token: 0x060001D4 RID: 468 RVA: 0x0000845C File Offset: 0x0000665C
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text = this.RuleID.ToString();
			stringBuilder.Append(text);
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
			if ((text = this.NameForm) != null)
			{
				stringBuilder.Append(" FORM ");
				stringBuilder.Append("'" + text + "'");
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
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(array[j]);
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
						for (int k = 0; k < qualifier.Length; k++)
						{
							if (k > 0)
							{
								stringBuilder.Append(" ");
							}
							stringBuilder.Append("'" + qualifier[k] + "'");
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

		// Token: 0x040000BC RID: 188
		private int ruleID;

		// Token: 0x040000BD RID: 189
		private string nameForm = "";

		// Token: 0x040000BE RID: 190
		private string[] superiorIDs = new string[] { "" };
	}
}
