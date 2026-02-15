using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001F RID: 31
	public class LdapAttributeSchema : LdapSchemaElement
	{
		// Token: 0x06000111 RID: 273 RVA: 0x000055D2 File Offset: 0x000037D2
		private void InitBlock()
		{
			this.usage = 0;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000055DB File Offset: 0x000037DB
		public virtual string SyntaxString
		{
			get
			{
				return this.syntaxString;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000055E3 File Offset: 0x000037E3
		public virtual string Superior
		{
			get
			{
				return this.superior;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000055EB File Offset: 0x000037EB
		public virtual bool SingleValued
		{
			get
			{
				return this.single;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000055F3 File Offset: 0x000037F3
		public virtual string EqualityMatchingRule
		{
			get
			{
				return this.equality;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000055FB File Offset: 0x000037FB
		public virtual string OrderingMatchingRule
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005603 File Offset: 0x00003803
		public virtual string SubstringMatchingRule
		{
			get
			{
				return this.substring;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000560B File Offset: 0x0000380B
		public virtual bool Collective
		{
			get
			{
				return this.collective;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005613 File Offset: 0x00003813
		public virtual bool UserModifiable
		{
			get
			{
				return this.userMod;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000561B File Offset: 0x0000381B
		public virtual int Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005624 File Offset: 0x00003824
		public LdapAttributeSchema(string[] names, string oid, string description, string syntaxString, bool single, string superior, bool obsolete, string equality, string ordering, string substring, bool collective, bool isUserModifiable, int usage)
			: base(LdapSchema.schemaTypeNames[0])
		{
			this.InitBlock();
			this.names = names;
			this.oid = oid;
			this.description = description;
			this.obsolete = obsolete;
			this.syntaxString = syntaxString;
			this.single = single;
			this.equality = equality;
			this.ordering = ordering;
			this.substring = substring;
			this.collective = collective;
			this.userMod = isUserModifiable;
			this.usage = usage;
			this.superior = superior;
			base.Value = this.formatString();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000056BC File Offset: 0x000038BC
		public LdapAttributeSchema(string raw)
			: base(LdapSchema.schemaTypeNames[0])
		{
			this.InitBlock();
			try
			{
				SchemaParser schemaParser = new SchemaParser(raw);
				if (schemaParser.Names != null)
				{
					this.names = schemaParser.Names;
				}
				if (schemaParser.ID != null)
				{
					this.oid = schemaParser.ID;
				}
				if (schemaParser.Description != null)
				{
					this.description = schemaParser.Description;
				}
				if (schemaParser.Syntax != null)
				{
					this.syntaxString = schemaParser.Syntax;
				}
				if (schemaParser.Superior != null)
				{
					this.superior = schemaParser.Superior;
				}
				this.single = schemaParser.Single;
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
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000057C0 File Offset: 0x000039C0
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
			if ((text = this.Superior) != null)
			{
				stringBuilder.Append(" SUP ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.EqualityMatchingRule) != null)
			{
				stringBuilder.Append(" EQUALITY ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.OrderingMatchingRule) != null)
			{
				stringBuilder.Append(" ORDERING ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.SubstringMatchingRule) != null)
			{
				stringBuilder.Append(" SUBSTR ");
				stringBuilder.Append("'" + text + "'");
			}
			if ((text = this.SyntaxString) != null)
			{
				stringBuilder.Append(" SYNTAX ");
				stringBuilder.Append(text);
			}
			if (this.SingleValued)
			{
				stringBuilder.Append(" SINGLE-VALUE");
			}
			if (this.Collective)
			{
				stringBuilder.Append(" COLLECTIVE");
			}
			if (!this.UserModifiable)
			{
				stringBuilder.Append(" NO-USER-MODIFICATION");
			}
			int num;
			if ((num = this.Usage) != 0)
			{
				switch (num)
				{
				case 1:
					stringBuilder.Append(" USAGE directoryOperation");
					break;
				case 2:
					stringBuilder.Append(" USAGE distributedOperation");
					break;
				case 3:
					stringBuilder.Append(" USAGE dSAOperation");
					break;
				}
			}
			IEnumerator qualifierNames = this.QualifierNames;
			while (qualifierNames.MoveNext())
			{
				object obj = qualifierNames.Current;
				text = (string)obj;
				if (text != null)
				{
					stringBuilder.Append(" " + text);
					array = this.getQualifier(text);
					if (array != null)
					{
						if (array.Length > 1)
						{
							stringBuilder.Append("(");
						}
						for (int j = 0; j < array.Length; j++)
						{
							stringBuilder.Append(" '" + array[j] + "'");
						}
						if (array.Length > 1)
						{
							stringBuilder.Append(" )");
						}
					}
				}
			}
			stringBuilder.Append(" )");
			return stringBuilder.ToString();
		}

		// Token: 0x04000085 RID: 133
		private string syntaxString;

		// Token: 0x04000086 RID: 134
		private bool single;

		// Token: 0x04000087 RID: 135
		private string superior;

		// Token: 0x04000088 RID: 136
		private string equality;

		// Token: 0x04000089 RID: 137
		private string ordering;

		// Token: 0x0400008A RID: 138
		private string substring;

		// Token: 0x0400008B RID: 139
		private bool collective;

		// Token: 0x0400008C RID: 140
		private bool userMod = true;

		// Token: 0x0400008D RID: 141
		private int usage;

		// Token: 0x0400008E RID: 142
		public const int USER_APPLICATIONS = 0;

		// Token: 0x0400008F RID: 143
		public const int DIRECTORY_OPERATION = 1;

		// Token: 0x04000090 RID: 144
		public const int DISTRIBUTED_OPERATION = 2;

		// Token: 0x04000091 RID: 145
		public const int DSA_OPERATION = 3;
	}
}
