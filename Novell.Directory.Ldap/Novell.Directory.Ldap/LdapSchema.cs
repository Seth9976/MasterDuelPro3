using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000043 RID: 67
	public class LdapSchema : LdapEntry
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000AD41 File Offset: 0x00008F41
		private void InitBlock()
		{
			this.nameTable = new Hashtable[8];
			this.idTable = new Hashtable[8];
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000AD5B File Offset: 0x00008F5B
		public virtual IEnumerator AttributeSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[0].Values.GetEnumerator());
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000AD74 File Offset: 0x00008F74
		public virtual IEnumerator DITContentRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[4].Values.GetEnumerator());
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000AD8D File Offset: 0x00008F8D
		public virtual IEnumerator DITStructureRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[5].Values.GetEnumerator());
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000ADA6 File Offset: 0x00008FA6
		public virtual IEnumerator MatchingRuleSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[6].Values.GetEnumerator());
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000ADBF File Offset: 0x00008FBF
		public virtual IEnumerator MatchingRuleUseSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[7].Values.GetEnumerator());
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public virtual IEnumerator NameFormSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[3].Values.GetEnumerator());
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000ADF1 File Offset: 0x00008FF1
		public virtual IEnumerator ObjectClassSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[1].Values.GetEnumerator());
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000AE0A File Offset: 0x0000900A
		public virtual IEnumerator SyntaxSchemas
		{
			get
			{
				return new EnumeratedIterator(this.idTable[2].Values.GetEnumerator());
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000AE23 File Offset: 0x00009023
		public virtual IEnumerator AttributeNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[0].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000AE41 File Offset: 0x00009041
		public virtual IEnumerator DITContentRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[4].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000AE5F File Offset: 0x0000905F
		public virtual IEnumerator DITStructureRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[5].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000AE7D File Offset: 0x0000907D
		public virtual IEnumerator MatchingRuleNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[6].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000AE9B File Offset: 0x0000909B
		public virtual IEnumerator MatchingRuleUseNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[7].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000AEB9 File Offset: 0x000090B9
		public virtual IEnumerator NameFormNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[3].Keys).GetEnumerator());
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AED7 File Offset: 0x000090D7
		public virtual IEnumerator ObjectClassNames
		{
			get
			{
				return new EnumeratedIterator(new SupportClass.SetSupport(this.nameTable[1].Keys).GetEnumerator());
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public LdapSchema(LdapEntry ent)
			: base(ent.DN, ent.getAttributeSet())
		{
			this.InitBlock();
			for (int i = 0; i < LdapSchema.schemaTypeNames.Length; i++)
			{
				this.idTable[i] = new Hashtable();
				this.nameTable[i] = new Hashtable();
			}
			foreach (object obj in base.getAttributeSet())
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				string name = ldapAttribute.Name;
				IEnumerator stringValues = ldapAttribute.StringValues;
				if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[1].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj2 = stringValues.Current;
						string text = (string)obj2;
						LdapObjectClassSchema ldapObjectClassSchema;
						try
						{
							ldapObjectClassSchema = new LdapObjectClassSchema(text);
						}
						catch (Exception)
						{
							continue;
						}
						this.addElement(1, ldapObjectClassSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[0].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj3 = stringValues.Current;
						string text = (string)obj3;
						LdapAttributeSchema ldapAttributeSchema;
						try
						{
							ldapAttributeSchema = new LdapAttributeSchema(text);
						}
						catch (Exception)
						{
							continue;
						}
						this.addElement(0, ldapAttributeSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[2].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj4 = stringValues.Current;
						string text = (string)obj4;
						LdapSyntaxSchema ldapSyntaxSchema = new LdapSyntaxSchema(text);
						this.addElement(2, ldapSyntaxSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[6].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj5 = stringValues.Current;
						string text = (string)obj5;
						LdapMatchingRuleSchema ldapMatchingRuleSchema = new LdapMatchingRuleSchema(text, null);
						this.addElement(6, ldapMatchingRuleSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[7].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj6 = stringValues.Current;
						string text = (string)obj6;
						LdapMatchingRuleUseSchema ldapMatchingRuleUseSchema = new LdapMatchingRuleUseSchema(text);
						this.addElement(7, ldapMatchingRuleUseSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[4].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj7 = stringValues.Current;
						string text = (string)obj7;
						LdapDITContentRuleSchema ldapDITContentRuleSchema = new LdapDITContentRuleSchema(text);
						this.addElement(4, ldapDITContentRuleSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[5].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj8 = stringValues.Current;
						string text = (string)obj8;
						LdapDITStructureRuleSchema ldapDITStructureRuleSchema = new LdapDITStructureRuleSchema(text);
						this.addElement(5, ldapDITStructureRuleSchema);
					}
				}
				else if (name.ToUpper().Equals(LdapSchema.schemaTypeNames[3].ToUpper()))
				{
					while (stringValues.MoveNext())
					{
						object obj9 = stringValues.Current;
						string text = (string)obj9;
						LdapNameFormSchema ldapNameFormSchema = new LdapNameFormSchema(text);
						this.addElement(3, ldapNameFormSchema);
					}
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B1D8 File Offset: 0x000093D8
		private void addElement(int schemaType, LdapSchemaElement element)
		{
			SupportClass.PutElement(this.idTable[schemaType], element.ID, element);
			string[] names = element.Names;
			for (int i = 0; i < names.Length; i++)
			{
				SupportClass.PutElement(this.nameTable[schemaType], names[i].ToUpper(), element);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B228 File Offset: 0x00009428
		private LdapSchemaElement getSchemaElement(int schemaType, string key)
		{
			if (key == null || key.ToUpper().Equals("".ToUpper()))
			{
				return null;
			}
			char c = key[0];
			if (c >= '0' && c <= '9')
			{
				return (LdapSchemaElement)this.idTable[schemaType][key];
			}
			return (LdapSchemaElement)this.nameTable[schemaType][key.ToUpper()];
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B28F File Offset: 0x0000948F
		public virtual LdapAttributeSchema getAttributeSchema(string name)
		{
			return (LdapAttributeSchema)this.getSchemaElement(0, name);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B29E File Offset: 0x0000949E
		public virtual LdapDITContentRuleSchema getDITContentRuleSchema(string name)
		{
			return (LdapDITContentRuleSchema)this.getSchemaElement(4, name);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B2AD File Offset: 0x000094AD
		public virtual LdapDITStructureRuleSchema getDITStructureRuleSchema(string name)
		{
			return (LdapDITStructureRuleSchema)this.getSchemaElement(5, name);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B2BC File Offset: 0x000094BC
		public virtual LdapDITStructureRuleSchema getDITStructureRuleSchema(int ID)
		{
			return (LdapDITStructureRuleSchema)this.idTable[5][ID];
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B2E3 File Offset: 0x000094E3
		public virtual LdapMatchingRuleSchema getMatchingRuleSchema(string name)
		{
			return (LdapMatchingRuleSchema)this.getSchemaElement(6, name);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B2F2 File Offset: 0x000094F2
		public virtual LdapMatchingRuleUseSchema getMatchingRuleUseSchema(string name)
		{
			return (LdapMatchingRuleUseSchema)this.getSchemaElement(7, name);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B301 File Offset: 0x00009501
		public virtual LdapNameFormSchema getNameFormSchema(string name)
		{
			return (LdapNameFormSchema)this.getSchemaElement(3, name);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B310 File Offset: 0x00009510
		public virtual LdapObjectClassSchema getObjectClassSchema(string name)
		{
			return (LdapObjectClassSchema)this.getSchemaElement(1, name);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B31F File Offset: 0x0000951F
		public virtual LdapSyntaxSchema getSyntaxSchema(string oid)
		{
			return (LdapSyntaxSchema)this.getSchemaElement(2, oid);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B330 File Offset: 0x00009530
		private int getType(LdapSchemaElement element)
		{
			if (element is LdapAttributeSchema)
			{
				return 0;
			}
			if (element is LdapObjectClassSchema)
			{
				return 1;
			}
			if (element is LdapSyntaxSchema)
			{
				return 2;
			}
			if (element is LdapNameFormSchema)
			{
				return 3;
			}
			if (element is LdapMatchingRuleSchema)
			{
				return 6;
			}
			if (element is LdapMatchingRuleUseSchema)
			{
				return 7;
			}
			if (element is LdapDITContentRuleSchema)
			{
				return 4;
			}
			if (element is LdapDITStructureRuleSchema)
			{
				return 5;
			}
			throw new ArgumentException("The specified schema element type is not recognized");
		}

		// Token: 0x04000157 RID: 343
		private Hashtable[] idTable;

		// Token: 0x04000158 RID: 344
		private Hashtable[] nameTable;

		// Token: 0x04000159 RID: 345
		internal static readonly string[] schemaTypeNames = new string[] { "attributeTypes", "objectClasses", "ldapSyntaxes", "nameForms", "dITContentRules", "dITStructureRules", "matchingRules", "matchingRuleUse" };

		// Token: 0x0400015A RID: 346
		internal const int ATTRIBUTE = 0;

		// Token: 0x0400015B RID: 347
		internal const int OBJECT_CLASS = 1;

		// Token: 0x0400015C RID: 348
		internal const int SYNTAX = 2;

		// Token: 0x0400015D RID: 349
		internal const int NAME_FORM = 3;

		// Token: 0x0400015E RID: 350
		internal const int DITCONTENT = 4;

		// Token: 0x0400015F RID: 351
		internal const int DITSTRUCTURE = 5;

		// Token: 0x04000160 RID: 352
		internal const int MATCHING = 6;

		// Token: 0x04000161 RID: 353
		internal const int MATCHING_USE = 7;
	}
}
