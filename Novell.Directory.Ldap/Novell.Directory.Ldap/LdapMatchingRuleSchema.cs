using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000036 RID: 54
	public class LdapMatchingRuleSchema : LdapSchemaElement
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009030 File Offset: 0x00007230
		public virtual string[] Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009038 File Offset: 0x00007238
		public virtual string SyntaxString
		{
			get
			{
				return this.syntaxString;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009040 File Offset: 0x00007240
		public LdapMatchingRuleSchema(string[] names, string oid, string description, string[] attributes, bool obsolete, string syntaxString)
			: base(LdapSchema.schemaTypeNames[6])
		{
			this.names = new string[names.Length];
			names.CopyTo(this.names, 0);
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.attributes = new string[attributes.Length];
			attributes.CopyTo(this.attributes, 0);
			this.syntaxString = syntaxString;
			base.Value = this.formatString();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000090BC File Offset: 0x000072BC
		public LdapMatchingRuleSchema(string rawMatchingRule, string rawMatchingRuleUse)
			: base(LdapSchema.schemaTypeNames[6])
		{
			try
			{
				SchemaParser schemaParser = new SchemaParser(rawMatchingRule);
				this.names = new string[schemaParser.Names.Length];
				schemaParser.Names.CopyTo(this.names, 0);
				this.oid = schemaParser.ID;
				this.description = schemaParser.Description;
				this.obsolete = schemaParser.Obsolete;
				this.syntaxString = schemaParser.Syntax;
				if (rawMatchingRuleUse != null)
				{
					SchemaParser schemaParser2 = new SchemaParser(rawMatchingRuleUse);
					this.attributes = schemaParser2.Applies;
				}
				base.Value = this.formatString();
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000916C File Offset: 0x0000736C
		protected internal override string formatString()
		{
			StringBuilder stringBuilder = new StringBuilder("( ");
			string text;
			if ((text = this.ID) != null)
			{
				stringBuilder.Append(text);
			}
			string[] names = this.Names;
			if (names != null)
			{
				stringBuilder.Append(" NAME ");
				if (names.Length == 1)
				{
					stringBuilder.Append("'" + names[0] + "'");
				}
				else
				{
					stringBuilder.Append("( ");
					for (int i = 0; i < names.Length; i++)
					{
						stringBuilder.Append(" '" + names[i] + "'");
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
			if ((text = this.SyntaxString) != null)
			{
				stringBuilder.Append(" SYNTAX ");
				stringBuilder.Append(text);
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x04000124 RID: 292
		private string syntaxString;

		// Token: 0x04000125 RID: 293
		private string[] attributes;
	}
}
