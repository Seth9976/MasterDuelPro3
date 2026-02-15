using System;

namespace System.Xml.Schema
{
	// Token: 0x0200029E RID: 670
	internal sealed class SchemaNotation
	{
		// Token: 0x06001EB3 RID: 7859 RVA: 0x000B4A68 File Offset: 0x000B2C68
		internal SchemaNotation(XmlQualifiedName name)
		{
			this.name = name;
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x000B4A77 File Offset: 0x000B2C77
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000B4A7F File Offset: 0x000B2C7F
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x000B4A87 File Offset: 0x000B2C87
		internal string SystemLiteral
		{
			get
			{
				return this.systemLiteral;
			}
			set
			{
				this.systemLiteral = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000B4A90 File Offset: 0x000B2C90
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x000B4A98 File Offset: 0x000B2C98
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x04000E39 RID: 3641
		private XmlQualifiedName name;

		// Token: 0x04000E3A RID: 3642
		private string systemLiteral;

		// Token: 0x04000E3B RID: 3643
		private string pubid;
	}
}
