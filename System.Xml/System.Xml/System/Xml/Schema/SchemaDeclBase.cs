using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000295 RID: 661
	internal abstract class SchemaDeclBase
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x000B273C File Offset: 0x000B093C
		protected SchemaDeclBase(XmlQualifiedName name, string prefix)
		{
			this.name = name;
			this.prefix = prefix;
			this.maxLength = -1L;
			this.minLength = -1L;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000B276D File Offset: 0x000B096D
		protected SchemaDeclBase()
		{
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x000B2780 File Offset: 0x000B0980
		// (set) Token: 0x06001E29 RID: 7721 RVA: 0x000B2788 File Offset: 0x000B0988
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x000B2791 File Offset: 0x000B0991
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x000B27A7 File Offset: 0x000B09A7
		internal string Prefix
		{
			get
			{
				if (this.prefix != null)
				{
					return this.prefix;
				}
				return string.Empty;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000B27B0 File Offset: 0x000B09B0
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x000B27B8 File Offset: 0x000B09B8
		internal bool IsDeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000B27C1 File Offset: 0x000B09C1
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x000B27C9 File Offset: 0x000B09C9
		internal SchemaDeclBase.Use Presence
		{
			get
			{
				return this.presence;
			}
			set
			{
				this.presence = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x000B27D2 File Offset: 0x000B09D2
		// (set) Token: 0x06001E31 RID: 7729 RVA: 0x000B27DA File Offset: 0x000B09DA
		internal long MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x000B27E3 File Offset: 0x000B09E3
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x000B27EB File Offset: 0x000B09EB
		internal long MinLength
		{
			get
			{
				return this.minLength;
			}
			set
			{
				this.minLength = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000B27F4 File Offset: 0x000B09F4
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x000B27FC File Offset: 0x000B09FC
		internal XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000B2805 File Offset: 0x000B0A05
		// (set) Token: 0x06001E37 RID: 7735 RVA: 0x000B280D File Offset: 0x000B0A0D
		internal XmlSchemaDatatype Datatype
		{
			get
			{
				return this.datatype;
			}
			set
			{
				this.datatype = value;
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000B2816 File Offset: 0x000B0A16
		internal void AddValue(string value)
		{
			if (this.values == null)
			{
				this.values = new List<string>();
			}
			this.values.Add(value);
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x000B2837 File Offset: 0x000B0A37
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x000B283F File Offset: 0x000B0A3F
		internal List<string> Values
		{
			get
			{
				return this.values;
			}
			set
			{
				this.values = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x000B2848 File Offset: 0x000B0A48
		// (set) Token: 0x06001E3C RID: 7740 RVA: 0x000B285E File Offset: 0x000B0A5E
		internal string DefaultValueRaw
		{
			get
			{
				if (this.defaultValueRaw == null)
				{
					return string.Empty;
				}
				return this.defaultValueRaw;
			}
			set
			{
				this.defaultValueRaw = value;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x000B2867 File Offset: 0x000B0A67
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x000B286F File Offset: 0x000B0A6F
		internal object DefaultValueTyped
		{
			get
			{
				return this.defaultValueTyped;
			}
			set
			{
				this.defaultValueTyped = value;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B2878 File Offset: 0x000B0A78
		internal bool CheckEnumeration(object pVal)
		{
			return (this.datatype.TokenizedType != XmlTokenizedType.NOTATION && this.datatype.TokenizedType != XmlTokenizedType.ENUMERATION) || this.values.Contains(pVal.ToString());
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000B28AA File Offset: 0x000B0AAA
		internal bool CheckValue(object pVal)
		{
			return (this.presence != SchemaDeclBase.Use.Fixed && this.presence != SchemaDeclBase.Use.RequiredFixed) || (this.defaultValueTyped != null && this.datatype.IsEqual(pVal, this.defaultValueTyped));
		}

		// Token: 0x04000CE9 RID: 3305
		protected XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000CEA RID: 3306
		protected string prefix;

		// Token: 0x04000CEB RID: 3307
		protected bool isDeclaredInExternal;

		// Token: 0x04000CEC RID: 3308
		protected SchemaDeclBase.Use presence;

		// Token: 0x04000CED RID: 3309
		protected XmlSchemaType schemaType;

		// Token: 0x04000CEE RID: 3310
		protected XmlSchemaDatatype datatype;

		// Token: 0x04000CEF RID: 3311
		protected string defaultValueRaw;

		// Token: 0x04000CF0 RID: 3312
		protected object defaultValueTyped;

		// Token: 0x04000CF1 RID: 3313
		protected long maxLength;

		// Token: 0x04000CF2 RID: 3314
		protected long minLength;

		// Token: 0x04000CF3 RID: 3315
		protected List<string> values;

		// Token: 0x02000296 RID: 662
		internal enum Use
		{
			// Token: 0x04000CF5 RID: 3317
			Default,
			// Token: 0x04000CF6 RID: 3318
			Required,
			// Token: 0x04000CF7 RID: 3319
			Implied,
			// Token: 0x04000CF8 RID: 3320
			Fixed,
			// Token: 0x04000CF9 RID: 3321
			RequiredFixed
		}
	}
}
